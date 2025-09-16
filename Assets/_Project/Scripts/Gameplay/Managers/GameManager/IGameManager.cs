namespace _Project.Scripts.Gameplay.Managers.GameManager
{
    public interface IGameManager
    {
        void StartGame();
        void EndGameWin();
        void EndGameLose();
        void TogglePause(bool isPaused);
    }
}