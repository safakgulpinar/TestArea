using UnityEngine;

namespace _Project.Scripts.Gameplay.Installers.GameplaySetupConfig
{
    [CreateAssetMenu(menuName = "_Project/Game/Settings/GameplaySetupConfigSO")]
    public class GameplaySetupConfigSO : ScriptableObject
    {
        public bool FeatureModeOn;
    }
}