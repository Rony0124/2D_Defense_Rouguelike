using System;
using UnityEngine;

namespace InGame
{
    public class HurtBox : MonoBehaviour
    {
        private IDamageHandler damageHandler;

        private void Awake()
        {
            damageHandler = GetComponentInParent<IDamageHandler>();
        }

        public void TakeDamage(HitBox hitBox)
        {
            if (!hitBox)
                return;

            var projectile = hitBox.GetComponent<Projectile>();
            if (projectile)
            {
                damageHandler.TakeDamage(projectile.spellInfo.spellDamage);
                return;
            }

            if (hitBox.pet)
            {
                damageHandler.TakeDamage(hitBox.pet.GetDamage());
            }
        }
    }


}
