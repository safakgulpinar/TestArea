using _TestGame.Scripts.Core;
using _TestGame.Scripts.Core.Input;
using _TestGame.Scripts.Core.Navigation;
using Zenject;

namespace _TestGame.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ShowScreen>().OptionalSubscriber(); //Optional ekleyerek kimse abone olmasada var olmasnıı ayrıca log atmamasını sağlıyoruz.
            Container.DeclareSignal<RequestMainMenu>();
            Container.DeclareSignal<RequestGameplay>();
            Container.DeclareSignal<RequestLevelEndWin>();
            Container.DeclareSignal<RequestLevelEndLose>(); 

            Container.Bind<AppState>().AsSingle();

            // Services & Controllers
            Container.BindInterfacesTo<KeyInputController>().AsSingle(); // ITickable, tickable demek her frame kontrol ediliyor demek.
        }
    }
}