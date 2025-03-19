using System;
using UnityEngine;

namespace InGame
{
    public class HitBox : MonoBehaviour
    {
        public LayerMask layerMask;

        private void OnTriggerEnter2D(Collider2D other)
        {
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
