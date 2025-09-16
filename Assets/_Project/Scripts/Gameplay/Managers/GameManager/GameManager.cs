using UnityEngine;
using Zenject;
using _Project.Scripts.Gameplay.Managers.LevelManager;
using _Project.Scripts.Gameplay.Managers.UIManager;

namespace _Project.Scripts.Gameplay.Managers.GameManager
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject] private ILevelManager _levelManager;
        [Inject] private IUIManager _uiManager;
        
        [SerializeField] private GameManagerSettingSO gameManagerSettingSO;

        private void Start()
        {
            _uiManager.Show(UIScreen.MainMenu);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _levelManager.LoadCurrent();
            }
        }

        public void StartGame()
        {
            _levelManager.LoadCurrent();
            _uiManager.Show(UIScreen.Gameplay);
        }

        public void EndGameWin()
        {
            _uiManager.Show(UIScreen.LevelEndWin);
        }

        public void EndGameLose()
        {
            _uiManager.Show(UIScreen.LevelEndLose);
        }

        public void TogglePause(bool isPaused)
        {
            _uiManager.Show(isPaused ? UIScreen.Pause : UIScreen.Gameplay);
        }
    }
}