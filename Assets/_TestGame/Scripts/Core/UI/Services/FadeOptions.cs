namespace _TestGame.Scripts.Core.UI.Services
{
    public readonly struct FadeOptions
    {
        public readonly float Duration;
        public FadeOptions(float duration) { Duration = duration; }
        public static FadeOptions Default => new FadeOptions(0.25f);
    }
}