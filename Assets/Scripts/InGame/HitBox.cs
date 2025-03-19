using UnityEngine;

namespace InGame
{
    public class HitBox : MonoBehaviour
    {
        private EnemyController enemy;
        private void Awake()
        {
            enemy = GetComponent<EnemyController>();
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
