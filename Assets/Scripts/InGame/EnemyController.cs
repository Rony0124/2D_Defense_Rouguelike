using Data;
using UnityEngine;
using Util;
  
namespace InGame
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public ObservableVar<bool> IsDead;

        private Transform endPoint;
        private float health;
        private float power;
        private float moveSpeed;
        
        private void Awake()
        {
            IsDead = new();
            IsDead.Value = false;
        }

        public void Initialize(MonsterInfo info, float power, float health, Transform endPoint)
        {
            spriteRenderer.sprite = info.monsterGraphic;
            moveSpeed = info.moveSpeed;
            
            this.endPoint = endPoint;
            this.power = power;
            this.health = health;
        }

        private void FixedUpdate()
        {
            if (CanMove())
            {
                transform.Translate(0, moveSpeed * Time.fixedDeltaTime, 0, Space.World);
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
    }
}
