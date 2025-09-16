using UnityEngine;
using Zenject;

namespace _Project.Scripts.Gameplay.Managers.UIManager
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [Header("Assign later in scene")]
        public GameObject MainMenu;
        public GameObject Gameplay;
        public GameObject Pause;
        public GameObject LevelEndWin;
        public GameObject LevelEndLose;

        [Inject(Optional = true)] private UIManagerSettingSO _settings;

        public void Show(UIScreen screen)
        {
            if (MainMenu)     MainMenu.SetActive(screen == UIScreen.MainMenu);
            if (Gameplay)     Gameplay.SetActive(screen == UIScreen.Gameplay);
            if (Pause)        Pause.SetActive(screen == UIScreen.Pause);
            if (LevelEndWin)  LevelEndWin.SetActive(screen == UIScreen.LevelEndWin);
            if (LevelEndLose) LevelEndLose.SetActive(screen == UIScreen.LevelEndLose);

            // (Opsiyonel) _settings.TransitionDuration ile fade/anim ekleyebilirsin
        }

        public void Show(string screenKey)
        {
            var k = (screenKey ?? "").ToLowerInvariant();
            if      (k == "mainmenu")      Show(UIScreen.MainMenu);
            else if (k == "gameplay")      Show(UIScreen.Gameplay);
            else if (k == "pause")         Show(UIScreen.Pause);
            else if (k == "win" || k == "levelendwin")   Show(UIScreen.LevelEndWin);
            else if (k == "lose"|| k == "levelendlose")  Show(UIScreen.LevelEndLose);
        }
    }
}