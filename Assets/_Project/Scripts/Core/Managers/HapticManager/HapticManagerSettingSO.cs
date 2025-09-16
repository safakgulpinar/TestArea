using UnityEngine;

namespace _Project.Scripts.Core.Managers.HapticManager
{
    [CreateAssetMenu(menuName = "_Project/Core/Settings/HapticManagerSettingSO")]
    public class HapticManagerSettingSO : ScriptableObject
    {
        [Header("General")]
        public bool EnabledByDefault = true;
    }
}