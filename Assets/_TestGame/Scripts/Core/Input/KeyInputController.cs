using _TestGame.Scripts.Core.Navigation;
using UnityEngine;
using Zenject;

namespace _TestGame.Scripts.Core.Input
{
    public class KeyInputController : ITickable
    {
        private readonly SignalBus _bus;
        public KeyInputController(SignalBus bus) => _bus = bus;

        public void Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.S)) _bus.Fire(new RequestGameplay());
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.D)) _bus.Fire(new RequestLevelEndWin());
            if (UnityEngine.Input.GetKeyDown(KeyCode.F)) _bus.Fire(new RequestLevelEndLose());
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.A)) _bus.Fire(new RequestMainMenu());
        }
    }
}