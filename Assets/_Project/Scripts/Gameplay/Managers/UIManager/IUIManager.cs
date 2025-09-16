// Assets/_Project/Scripts/Gameplay/Managers/UIManager/IUIManager.cs
namespace _Project.Scripts.Gameplay.Managers.UIManager
{
    public enum UIScreen { MainMenu, Gameplay, Pause, LevelEndWin, LevelEndLose }

    public interface IUIManager
    {
        void Show(UIScreen screen);
        void Show(string screenKey);
    }
}