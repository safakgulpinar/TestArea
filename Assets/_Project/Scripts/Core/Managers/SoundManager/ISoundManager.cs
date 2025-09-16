namespace _Project.Scripts.Core.Managers.SoundManager
{
    public interface ISoundManager
    {
        void PlaySfx(string key);
        void PlayMusic(string key);
        void StopMusic();
    }
}