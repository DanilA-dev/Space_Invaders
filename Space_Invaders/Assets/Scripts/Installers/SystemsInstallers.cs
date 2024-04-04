using System;
using Systems;
using Core.Behaviours;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SystemsInstallers : MonoInstaller
    {
        [SerializeField] private MobileJoystickInput _joystickInput;
        [SerializeField] private UnitEntitySpawner _unitEntitySpawner;
        [SerializeField] private EnemiesMovementHandler _enemiesMovementHandler;
        
        public override void InstallBindings()
        {
            BindScoreHandler();
            BindInput();
            BindEnemiesMovementHandler();
            BindUnitSpawner();
            BindUniRegisterService();
        }

        private void BindEnemiesMovementHandler() =>  Container.BindInterfacesAndSelfTo<EnemiesMovementHandler>()
            .FromInstance(_enemiesMovementHandler).AsSingle().NonLazy();
           

        private void BindUniRegisterService() => Container.BindInterfacesAndSelfTo<UnitEntityRegisterService>()
            .FromNew().AsSingle().NonLazy();
        

        private void BindUnitSpawner() => Container.BindInterfacesAndSelfTo<UnitEntitySpawner>()
            .FromInstance(_unitEntitySpawner).AsSingle().NonLazy();
       

        private void BindInput() => Container.Bind<IInput>().To<MobileJoystickInput>().FromInstance(_joystickInput)
            .AsSingle().NonLazy();
      

        private void BindScoreHandler() =>
            Container.BindInterfacesAndSelfTo<ScoreHandler>().FromNew().AsSingle().NonLazy();

    }
}