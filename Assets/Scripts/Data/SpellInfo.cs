using Item;
using UnityEngine;
using Util;

namespace Data
{
    [CreateAssetMenu(fileName = "SpellInfo", menuName = "Data/Item/SpellInfo")]
    public class SpellInfo : ItemInfo
    {
        public Define.SpellItem itemType;
        public GameObject projectilePrefab;
        
        public float projectileSpeed;
        public float projectileRange;
        public float spellDamage;
        public float spellInterval;
        public float spellDuration;
    }
}
