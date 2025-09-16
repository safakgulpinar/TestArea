using _Project.Scripts.Gameplay.Managers.GameManager;
using _Project.Scripts.Gameplay.Managers.LevelManager;
using _Project.Scripts.Gameplay.Managers.UIManager;
using Zenject;
namespace _Project.Scripts.Gameplay.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        public GameManagerSettingSO GameManagerSettingSO;
        public LevelManagerSettingSO LevelManagerSettingSO;
        public UIManagerSettingSO UIManagerSettingSO;

        public override void InstallBindings()
        {
            // Settings
            Container.BindInstance(GameManagerSettingSO).AsSingle();
            Container.BindInstance(LevelManagerSettingSO).AsSingle();
            Container.BindInstance(UIManagerSettingSO).AsSingle();

            // Sahnede olan MonoBehaviour bileşenlerini arayüzlerine bağla
            Container.Bind<ILevelManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUIManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}