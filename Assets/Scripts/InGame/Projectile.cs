using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.Events;

namespace InGame
{
    public class Projectile : MonoBehaviour
    {
        public Player.PlayerController ownerPlayer { get; set; }
        public SpellInfo spellInfo { get; set; }

        public UnityEvent onFire;

        public void SetProjectile(SpellInfo info)
        {
            spellInfo = info;
        }

        public async UniTaskVoid Fire(ObjectPoolProjectile poolProjectile)
        {
            var interval = spellInfo.spellDuration / Time.fixedDeltaTime * 2;
           
            for (int i = 0; i < interval; i++)
            {
                transform.Translate(0, -spellInfo.projectileSpeed * Time.fixedDeltaTime, 0, Space.World);
                await UniTask.NextFrame();
            }
            
            poolProjectile.ReturnObject(this);
        }
    }
}
