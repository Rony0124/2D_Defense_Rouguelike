using Cysharp.Threading.Tasks;
using Data;
using Manager;
using UnityEngine;
using Util;

namespace InGame.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageHandler
    {
        public ObservableVar<bool> IsDead;

        public ObjectPoolEnemy enemyPool { get; set; }
        
        [SerializeField]
        private float attackInterval;

        private Transform endPoint;
        private float health;
        private float maxHealth;
        private float power;
        private float moveSpeed;
        private float attackTime;

        private Animator animator;
        
        private static readonly int CanWalkId = Animator.StringToHash("CanWalk");
        private static readonly int HitId = Animator.StringToHash("Hit");
        private static readonly int DamageId = Animator.StringToHash("Damage");
        private static readonly int DeathId = Animator.StringToHash("Death");
        
        private void Awake()
        {
            IsDead = new();
            IsDead.Value = false;
            IsDead.OnValueChanged += OnDeadValueChanged;
        }

        public void Initialize(MonsterInfo info, float power, float health, Transform endPoint)
        {
            var enemyObj = Instantiate(info.monsterGraphic, transform);
            animator = enemyObj.GetComponent<Animator>();
            moveSpeed = info.moveSpeed;
            
            this.endPoint = endPoint;
            this.power = power;
            this.health = health;
            maxHealth = health;
        }

        private void FixedUpdate()
        {
            if (CanMove())
            {
                transform.Translate(0, moveSpeed * Time.fixedDeltaTime, 0, Space.World);
                SetAnimatorParamBool(CanWalkId, true);
            }
            else
            {
                SetAnimatorParamBool(CanWalkId, false);
            }

            if (CanAttack())
            {
                Attack();
            }
        }

        private bool CanMove()
        {
            if (IsDead.Value)
                return false;

            if (transform.position.y >= endPoint.position.y)
                return false;
            
            return true;
        }

        private bool CanAttack()
        {
            if (IsDead.Value)
                return false;

            if (transform.position.y < endPoint.position.y)
                return false;

            if (attackTime > Time.time)
                return false;

            return true;
        }

        private void Attack()
        {
            var player = GameManager.Instance.Player;
            player.TakeDamage(power);
            
            attackTime = Time.time + attackInterval;
            
            SetAnimatorParamTrigger(HitId);
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
                IsDead.Value = true;
        }

        private void OnDeadValueChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                OnDeadTask().Forget();
            }
        }

        private async UniTaskVoid OnDeadTask()
        {
            SetAnimatorParamTrigger(DeathId);
            await UniTask.WaitForSeconds(2);
            enemyPool.ReturnObject(this);
        }
        
        private void SetAnimatorParamTrigger(int id)
        {
            if(animator)
            {
                animator.SetTrigger(id);
            }
        }
        
        private void SetAnimatorParamBool(int id, bool val)
        {
            if(animator)
            {
                animator.SetBool(id, val);
            }
        }
    }
}
