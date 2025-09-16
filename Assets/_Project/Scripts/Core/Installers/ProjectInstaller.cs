using _Project.Scripts.Core.Managers.HapticManager;
using _Project.Scripts.Core.Managers.SoundManager;
using Zenject;

namespace _Project.Scripts.Core.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        // Inspector’dan verilecek ScriptableObject ayarları
        public SoundManagerSettingSO SoundManagerSettingSO;
        public HapticManagerSettingSO HapticManagerSettingSO;

        public override void InstallBindings()
        {
            // (İstersen) SignalBus kullanacaksan aç:
            // SignalBusInstaller.Install(Container);

            // Settings SO’ları tekil olarak bağla
            Container.BindInstance(SoundManagerSettingSO).AsSingle();
            Container.BindInstance(HapticManagerSettingSO).AsSingle();

            // Proje-genel servisler (DontDestroyOnLoad yaşamı) — AsSingle
            Container.BindInterfacesTo<SoundManager>().AsSingle().NonLazy();   // ISoundManager, IInitializable, IDisposable
            Container.BindInterfacesTo<HapticManager>().AsSingle().NonLazy();  // IHapticManager, (opsiyonel) IInitializable
        }
    }
}