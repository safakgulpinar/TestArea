using System.Threading;
using _TestGame.Scripts.Core.UI.Services;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _TestGame.Scripts.Core.Navigation
{
    public class ScreenNavigator : IInitializable, System.IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly AppState _appState;
        private readonly ScreenRegistry _screenRegistry;
        private readonly IFadeService _fadeService;

        private CancellationTokenSource _cts;
        private readonly SemaphoreSlim _gate = new(1, 1);

        public ScreenNavigator(SignalBus signalBus, AppState appState, ScreenRegistry screenRegistry, IFadeService fadeService)
        {
            _signalBus = signalBus; 
            _appState = appState;
            _screenRegistry = screenRegistry;
            _fadeService = fadeService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<RequestMainMenu>(OnRequestMainMenu);
            _signalBus.Subscribe<RequestGameplay>(OnRequestGameplay);
            _signalBus.Subscribe<RequestLevelEndWin>(OnRequestLevelEndWin);
            _signalBus.Subscribe<RequestLevelEndLose>(OnRequestLevelEndLose);

            _signalBus.Fire(new ShowScreen(ScreenId.MainMenu));
            
            Change(ScreenId.MainMenu).Forget();
        }

        public void Dispose()
        {
            _cts?.Cancel(); 
            _cts?.Dispose();
            
            _signalBus.TryUnsubscribe<RequestMainMenu>(OnRequestMainMenu);
            _signalBus.TryUnsubscribe<RequestGameplay>(OnRequestGameplay);
            _signalBus.TryUnsubscribe<RequestLevelEndWin>(OnRequestLevelEndWin);
            _signalBus.TryUnsubscribe<RequestLevelEndLose>(OnRequestLevelEndLose);
        }

        private async UniTask Change(ScreenId nextScreenId)
        {
            await _gate.WaitAsync();
            try
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                var currentView = _screenRegistry.Get(_appState.Current);
                var nextView    = _screenRegistry.Get(nextScreenId);

                await _fadeService.FadeOut(FadeOptions.Default, _cts.Token);

                currentView?.Hide();
                nextView?.Show();
                _appState.Set(nextScreenId);

                await _fadeService.FadeIn(FadeOptions.Default, _cts.Token);
                
                _signalBus.Fire(new ShowScreen(nextScreenId));
            }
            finally { _gate.Release(); }
        }


        private void OnRequestMainMenu()
        {
            Change(ScreenId.MainMenu).Forget();
        }

        private void OnRequestGameplay()
        {
            Change(ScreenId.Gameplay).Forget();
        }
        
        private void OnRequestLevelEndWin()
        {
            Change(ScreenId.LevelEndWin).Forget();
        }
        
        private void OnRequestLevelEndLose()
        {
            Change(ScreenId.LevelEndLose).Forget();
        }
    }
}