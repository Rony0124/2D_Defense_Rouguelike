using UnityEngine;
using UnityEngine.Events;

namespace InGame
{
    public class Projectile : MonoBehaviour
    {
        public float damage { get; set; }

        public UnityEvent onFire;
    }
}
