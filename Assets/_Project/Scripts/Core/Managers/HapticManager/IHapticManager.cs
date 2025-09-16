namespace _Project.Scripts.Core.Managers.HapticManager
{
    public enum HapticType
    {
        Light, Medium, Heavy, Success, Warning, Failure
    }
    public interface IHapticManager
    {
        void Play(HapticType type);
        void SetEnabled(bool enabled);
        bool Enabled { get; }
    }
}