using Data;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class DataInstallers : MonoInstaller
    {
        [SerializeField] private UserData _userData;
        
        public override void InstallBindings()
        {
            BindUserData();
        }

        private void BindUserData() => Container.Bind<UserData>().FromInstance(_userData).AsSingle().NonLazy();

    }
}