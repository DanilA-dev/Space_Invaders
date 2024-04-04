using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace Core.Behaviours
{
    public class EnemiesMovementHandler : MonoBehaviour, IEnemiesMovementHandler
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _yEndValue;
        
        private List<EnemyEntity> _enemyEntities = new List<EnemyEntity>();

        //Maybe move BaseUnit Position and then update the entity itself?
        private void LateUpdate()
        {
            if(_enemyEntities.Count <= 0)
                return;

            foreach (var enemyEntity in _enemyEntities)
            {
                // var pos = Vector3.down * (_moveSpeed * Time.deltaTime);
                // enemyEntity.transform.position = pos;
                // enemyEntity.Unit.Position = pos;
            }
        }

        public void AddEnemyEntity(EnemyEntity enemyEntity)
        {
            if(!_enemyEntities.Contains(enemyEntity))
                _enemyEntities.Add(enemyEntity);
        }

        private void Clear()
        {
            _enemyEntities.Clear();
        }
    }
}