using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Data
{
    public class RegistryItem : ScriptableObject
    {
        private Guid _id;
        
        public Guid Id => _id;
        
        private Guid _lastGeneratedAssetGuid;
#if UNITY_EDITOR
        public void Awake()
        {
            UpdateIdentifier();
        }
        
        public void OnValidate()
        {
            UpdateIdentifier();
        }
        
        public void GenerateGUID()
        {
            if (Application.isPlaying)
                return;
            
            string assetPath = AssetDatabase.GetAssetPath(this);
            
            _id = new Guid(AssetDatabase.AssetPathToGUID(assetPath));
            _lastGeneratedAssetGuid = _id;
            
            EditorUtility.SetDirty(this);
        }
        
        public void UpdateIdentifier()
        {
            if (_id == Guid.Empty || _id != _lastGeneratedAssetGuid)
            {
                GenerateGUID();
            }
        }
#endif
    }
}
