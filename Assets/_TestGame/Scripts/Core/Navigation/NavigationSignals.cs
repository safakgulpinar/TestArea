namespace _TestGame.Scripts.Core.Navigation
{
    public readonly struct ShowScreen
    {
        public readonly ScreenId Id; 
        public ShowScreen(ScreenId id)=>Id=id;
    }
    
    public readonly struct RequestMainMenu { }
    public readonly struct RequestGameplay { }
    public readonly struct RequestLevelEndWin { }
    public readonly struct RequestLevelEndLose { }
}