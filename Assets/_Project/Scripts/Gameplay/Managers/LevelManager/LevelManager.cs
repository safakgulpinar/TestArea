using UnityEngine;

namespace _Project.Scripts.Gameplay.Managers.LevelManager
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        private readonly LevelManagerSettingSO _settings;
        public int CurrentIndex { get; private set; }

        public LevelManager(LevelManagerSettingSO settings)
        {
            _settings = settings;
            CurrentIndex = 0;
        }

        public void LoadCurrent()
        {
            //var currentLevel = CurrentIndex;
        }

        public void LoadNext()
        {
            var nextLevel = CurrentIndex + 1;
            LoadCurrent();
        }

        public void Reload()
        { 
            LoadCurrent();
        }
    }
}