using UnityEngine;
using Zenject;
using _Project.Scripts.Gameplay.Managers.LevelManager;
using _Project.Scripts.Gameplay.Managers.UIManager;

namespace _Project.Scripts.Gameplay.Managers.GameManager
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        // DI: Sahne yüklenince Installer’dan inject edilir
        [Inject] private ILevelManager _level;
        [Inject] private IUIManager _ui;
        
        [SerializeField] private GameManagerSettingSO gameManagerSettingSO;

        // İstersen Start’ta ilk ekranı aç
        private void Start()
        {
            _ui.Show(UIScreen.MainMenu);
        }

        public void StartGame()
        {
            _level.LoadCurrent();
            _ui.Show(UIScreen.Gameplay);
        }

        public void EndGameWin()
        {
            _ui.Show(UIScreen.LevelEndWin);
        }

        public void EndGameLose()
        {
            _ui.Show(UIScreen.LevelEndLose);
        }

        public void TogglePause(bool isPaused)
        {
            _ui.Show(isPaused ? UIScreen.Pause : UIScreen.Gameplay);
        }
    }
}