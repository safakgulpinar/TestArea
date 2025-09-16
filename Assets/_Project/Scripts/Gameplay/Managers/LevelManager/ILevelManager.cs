namespace _Project.Scripts.Gameplay.Managers.LevelManager
{
    public interface ILevelManager
    {
        int CurrentIndex { get; }
        void LoadCurrent();
        void LoadNext();
        void Reload();
    }
}