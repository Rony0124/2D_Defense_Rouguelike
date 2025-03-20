using UnityEngine;
using Util;

namespace Data
{
    [CreateAssetMenu(fileName = "PetInfo", menuName = "Data/Item/PetInfo")]
    public class PetInfo : ItemInfo
    {
        public GameObject petPrefab;
        public Define.SpellItem[] unlockSpells;
    }
}
