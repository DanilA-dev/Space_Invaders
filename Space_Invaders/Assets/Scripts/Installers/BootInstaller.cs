using Systems;
using Zenject;

namespace Installers
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameState();
            BindPersistentDataService();
        }

        private void BindPersistentDataService() => Container.BindInterfacesAndSelfTo<PrefsPersistentDataService>()
            .FromNew().AsSingle().NonLazy();
        

        private void BindGameState() => Container.BindInterfacesAndSelfTo<GameState>().FromNew().AsSingle().NonLazy();

    }

}
