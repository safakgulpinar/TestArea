using _TestGame.Scripts.Core.Navigation;
using _TestGame.Scripts.Core.UI.Services;
using _TestGame.Scripts.Core.UI.Views;
using UnityEngine;
using Zenject;

namespace _TestGame.Scripts.Installers
{
    public class UiInstaller : MonoInstaller
    {
        public BaseScreenView mainMenuView;
        public BaseScreenView gameplayView;
        public BaseScreenView levelEndViewWin;
        public BaseScreenView levelEndViewLose;
        
        //Bu componenti ui installerdan 'FadeService' istiyor.
        public CanvasGroup fadeCanvasGroup; 

        public override void InstallBindings()
        {
            var registry = new ScreenRegistry(mainMenuView, gameplayView, levelEndViewWin, levelEndViewLose);
            Container.BindInstance(registry).AsSingle();
            
            Container.Bind<CanvasGroup>().FromInstance(fadeCanvasGroup).AsSingle();

            Container.BindInterfacesAndSelfTo<FadeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScreenNavigator>().AsSingle();

        }
    }
}