using _TestGame.Scripts.Core.UI.Views;

namespace _TestGame.Scripts.Core.Navigation
{
    public class ScreenRegistry
    {
        private readonly BaseScreenView _mainMenu;
        private readonly BaseScreenView _gameplay;
        private readonly BaseScreenView _levelLevelEndWin;
        private readonly BaseScreenView _levelEndLose;

        public ScreenRegistry(BaseScreenView main, BaseScreenView game, BaseScreenView levelEndWin, BaseScreenView levelEndLose)
        {
            _mainMenu = main;
            _gameplay = game;
            _levelLevelEndWin = levelEndWin;
            _levelEndLose = levelEndLose;
        }

        public BaseScreenView Get(ScreenId id) => id switch
        {
            ScreenId.MainMenu => _mainMenu,
            ScreenId.Gameplay => _gameplay,
            ScreenId.LevelEndWin => _levelLevelEndWin,
            ScreenId.LevelEndLose => _levelEndLose,
            _ => _mainMenu
        };
    }
}