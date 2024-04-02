using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Blaster Data")]
    public class BlasterData : ScriptableObject
    {
        [field: SerializeField] public float RateOfFire { get; private set; }
        [field: SerializeField] public int BulletsPerFire { get; private set; }
    }
}