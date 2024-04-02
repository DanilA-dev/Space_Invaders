using Data;
using Entity;
using UnityEngine;

namespace System
{
    public interface IUnitEntitySpawner
    {
        public PlayerEntity SpawnPlayer(Vector3 pos);
        public EnemyEntity SpawnEnemy(EnemyType type, Vector3 pos);
    }
}