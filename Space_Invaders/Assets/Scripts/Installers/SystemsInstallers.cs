using Systems;
using Zenject;

namespace Installers
{
    public class SystemsInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreHandler();
        }

        private void BindScoreHandler() =>
            Container.BindInterfacesAndSelfTo<ScoreHandler>().FromNew().AsSingle().NonLazy();

    }
}