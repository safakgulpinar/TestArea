using Zenject;
using UnityEngine;

namespace _Project.Scripts.Core.Managers.SoundManager
{
    public class SoundManager : ISoundManager, IInitializable, System.IDisposable
    {
        private readonly SoundManagerSettingSO _settings;
        private GameObject _root;
        private AudioSource _music;

        public SoundManager(SoundManagerSettingSO settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            _root = new GameObject("[SoundManager]");
            Object.DontDestroyOnLoad(_root);
            _music = _root.AddComponent<AudioSource>();
            _music.loop = true;
            
            Debug.Log("SoundManager initialized");
        }

        public void PlaySfx(string key)
        {
            var clip = _settings?.GetSfx(key);
            if (clip != null) AudioSource.PlayClipAtPoint(clip, Vector3.zero, _settings.DefaultMasterVolume);
        }

        public void PlayMusic(string key)
        {
            var clip = _settings?.GetMusic(key);
            if (clip == null) return;
            _music.clip = clip;
            _music.Play();
        }

        public void StopMusic() => _music?.Stop();

        public void Dispose()
        {
            if (_root != null) Object.Destroy(_root);
            
            Debug.Log("SoundManager Disposed");
        }
    }
}