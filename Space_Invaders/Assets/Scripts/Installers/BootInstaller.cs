using Zenject;

namespace Installers
{
    public class BootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameState();
        }

        private void BindGameState() => Container.BindInterfacesAndSelfTo<GameState>().FromNew().AsSingle().NonLazy();

    }

}
