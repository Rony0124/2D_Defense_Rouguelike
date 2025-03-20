using System;
using UnityEngine;

namespace InGame
{
    public class HitBox : MonoBehaviour
    {
        public enum HitType
        {
            Instant,
            Continuous
        }
    
        [SerializeField]
        private HitType hitType;
        [SerializeField]
        private LayerMask layerMask;
        
        [SerializeField]
        private float knockBackForce;
        [SerializeField]
        private float attackInterval;

        private float attackTime;

        public PetController pet { get; set; }

        private void Awake()
        {
            pet = GetComponentInParent<PetController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (hitType != HitType.Instant)
                return;
            
            if(layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                HurtBox hurtBox = other.GetComponent<HurtBox>();

                if (hurtBox)
                {
                    hurtBox.TakeDamage(this);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (hitType != HitType.Continuous)
                return;

            if (attackTime > Time.time)
                return;

            attackTime = Time.time + attackInterval;
            
            if(layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                HurtBox hurtBox = other.GetComponent<HurtBox>();

                if (hurtBox)
                {
                    hurtBox.TakeDamage(this);
                }
            }
        }
    }
}
