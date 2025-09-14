using _TestGame.Scripts.Core.Navigation;

namespace _TestGame.Scripts.Core
{
    public class AppState
    {
        public ScreenId Current { get; private set; } = ScreenId.MainMenu;
        public void Set(ScreenId id) => Current = id;
    }
}