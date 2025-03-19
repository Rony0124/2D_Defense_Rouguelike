using Core;
using UnityEngine;

namespace Data.Registry
{
    [CreateAssetMenu(fileName = "DataRegistry")]
    public class DataRegistry : SingletonScriptableObject<DataRegistry>
    {
        [SerializeField]
        private SpellInfoRegistry spellRegistry;
        
        public SpellInfoRegistry SpellRegistry => spellRegistry;
    }
}
