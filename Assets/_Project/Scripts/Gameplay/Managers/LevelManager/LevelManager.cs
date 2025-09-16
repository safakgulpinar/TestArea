using UnityEngine;

namespace _Project.Scripts.Gameplay.Managers.LevelManager
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        public int CurrentIndex { get; private set; }
        
        [SerializeField] private LevelManagerSettingSO levelManagerSettingSO;

        public void LoadCurrent()
        {
            //var currentLevel = CurrentIndex;
            Debug.Log($"CURRENT LEVEL LOADED");
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