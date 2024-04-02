using System;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SystemsInstallers : MonoInstaller
    {
        [SerializeField] private MobileJoystickInput _joystickInput;
        [SerializeField] private UnitEntitySpawner _unitEntitySpawner;
        
        public override void InstallBindings()
        {
            BindScoreHandler();
            BindInput();
            BindUnitSpawner();
        }

        private void BindUnitSpawner() => Container.BindInterfacesAndSelfTo<UnitEntitySpawner>()
            .FromInstance(_unitEntitySpawner).AsSingle().NonLazy();
       

        private void BindInput() => Container.Bind<IInput>().To<MobileJoystickInput>().FromInstance(_joystickInput)
            .AsSingle().NonLazy();
      

        private void BindScoreHandler() =>
            Container.BindInterfacesAndSelfTo<ScoreHandler>().FromNew().AsSingle().NonLazy();

    }
}