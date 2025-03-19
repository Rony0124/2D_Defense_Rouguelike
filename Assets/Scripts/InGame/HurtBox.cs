using UnityEngine;

namespace InGame
{
    public class HurtBox : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        
        public void TakeDamage(HitBox hitBox)
        {
            if (!hitBox)
                return;
            
            player.TakeDamage();
        }
    }
}
