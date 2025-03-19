using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Registry<T> : ScriptableObject
        where T : RegistryItem
    {
        [SerializeField] private List<T> assets;
        
        public Dictionary<Guid, T> assetGuidLookup = new(); 
        
#if UNITY_EDITOR
        private void Awake()
        {
            SyncDictionaries();
        }
        
        void OnValidate()
        {
            SyncDictionaries();
        }
         
        private void SyncDictionaries()
        {
            assetGuidLookup.Clear();
            
            if (assets is not null && assets.Count > 0)
            {
                foreach (var data in assets)
                {
                    assetGuidLookup.TryAdd(data.Id, data);
                }
            }
        }
#endif
        
        public T Get(Guid id)
        {
            return assetGuidLookup.TryGetValue(id, out var asset) ? asset : null;
        }
        
        public bool TryGetValue(Guid key, out T data)
        {
            return assetGuidLookup.TryGetValue(key, out data);
        }
    }
}
