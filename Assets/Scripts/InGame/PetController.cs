using Cysharp.Threading.Tasks;
using Manager;
using UnityEngine;
using Util;

namespace InGame
{
    public class PetController : MonoBehaviour
    {
        [SerializeField] private GameObject attackPrefab;
        [SerializeField] private float attackInterval;
        [SerializeField] private float attackDuration;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private AnimationCurve damageByLevel;
        
        private float attackTime;
        public int Level { get; set; }
       

        private void Start()
        {
            attackTime = Time.time + attackInterval;
        }

        public void Update()
        {
            if (CanPlay())
            {
                if (attackTime > Time.time)
                    return;
                
                attackTime = Time.time + attackInterval;
                StartAttack().Forget();
            }
        }
        
        public async UniTaskVoid StartAttack()
        {
            var obj = Instantiate(attackPrefab, attackPoint);
            await UniTask.WaitForSeconds(attackDuration);

            Destroy(obj);
        }

        private bool CanPlay()
        {
            if(GameManager.Instance.GameState != Define.GameState.GamePlay)
                return false;

            if (GameManager.Instance.Player.IsDead.Value)
                return false;
        
            return true;
        }

        public float GetDamage()
        {
            return damageByLevel.Evaluate(Level);
        }
    }
}
