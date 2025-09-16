using UnityEngine;

namespace _Project.Scripts.Core.Managers.SoundManager
{
    [CreateAssetMenu(menuName = "_Project/Core/Settings/SoundManagerSettingSO")]
    public class SoundManagerSettingSO : ScriptableObject
    {
        [System.Serializable]
        public class NamedClip
        {
            public string Key;       // Örn: "click", "gameplay", "win"
            public AudioClip Clip;   // Unity AudioClip referansı
        }

        [Header("Volume Defaults")]
        [Range(0f, 1f)] public float DefaultMasterVolume = 0.8f;

        [Header("Clips")]
        public NamedClip[] Music;
        public NamedClip[] Sfx;

        // Lookup helper
        public AudioClip GetMusic(string key)
        {
            foreach (var entry in Music)
            {
                if (entry.Key == key) return entry.Clip;
            }
            
            return null;
        }

        public AudioClip GetSfx(string key)
        {
            foreach (var entry in Sfx)
            {
                if (entry.Key == key) return entry.Clip;
            }
            
            return null;
        }
    }
}