using UnityEngine;
using Util;

namespace Data
{
    [CreateAssetMenu(fileName = "PetInfo", menuName = "Data/Item/PetInfo")]
    public class PetInfo : ItemInfo
    {
        public GameObject petPrefab;
        public float damage;
        public float attackInterval;
        public float attackDuration;
        public Define.SpellItem[] unlockSpells;
    }
}
