using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace InGame
{
    public class ObjectPoolText : ObjectPool<TextMeshPro>
    {
        [SerializeField] 
        private float duration;
        
        protected override void Awake()
        {
            base.Awake();
            InitializePool();
        }
        
        public TextMeshPro GetObjectAsync(int dmg)
        {
            var returnObj = GetObject();
            returnObj.text = dmg.ToString();
            ReturnObjAsync(returnObj).Forget();
            
            return returnObj;
        }

        private async UniTaskVoid ReturnObjAsync(TextMeshPro obj)
        {
            await UniTask.WaitForSeconds(duration);
            ReturnObject(obj);
        }
    }
}
