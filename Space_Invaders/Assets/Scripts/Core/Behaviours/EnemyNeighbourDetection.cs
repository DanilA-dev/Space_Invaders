using Core.Model;
using Entity;
using UnityEngine;

namespace Core.Behaviours
{
    [System.Serializable]
    public class EnemyNeighbourDetection
    {
        [SerializeField] private Transform _rayOrigin;
        [SerializeField] private Vector2 _rayDir;
        [SerializeField] private Collider2D _col;
        
        public bool TryDetectBelowNeighbour(out EnemyUnit belowEnemyUnit)
        {
            var hit = Physics2D.Raycast(_rayOrigin.position, _rayDir);
            if (hit.collider != null)
                _col = hit.collider;
            
            if (hit.collider.TryGetComponent(out EnemyEntity enemyEntity))
            { 
                belowEnemyUnit = enemyEntity.Unit;
                return true;
            }
            
            belowEnemyUnit = null;
            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(_rayOrigin.position, _rayDir);
        }
    }
}