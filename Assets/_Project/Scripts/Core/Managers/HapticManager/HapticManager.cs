using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.Managers.HapticManager
{
    public class HapticManager : IHapticManager, IInitializable, System.IDisposable
    {
        private readonly HapticManagerSettingSO _settings;
        public bool Enabled { get; private set; }

        public HapticManager(HapticManagerSettingSO settings)
        {
            _settings = settings;
            Enabled = settings.EnabledByDefault;
        }
        
        public void Initialize()
        {
            Debug.Log("HapticManager initialized");
        }

        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void Play(HapticType type)
        {
            if (!Enabled) return;
            
            Handheld.Vibrate(); //Haptic kodun.
        }

        public void Dispose()
        {
            Debug.Log("HapticManager Disposed");
        }
    }
}