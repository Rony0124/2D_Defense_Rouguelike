using InGame.Enemy;
using UnityEngine;

namespace InGame
{
    public class ObjectPoolEnemy : ObjectPool<EnemyController>
    {
        public override EnemyController GetObject()
        {
            if (pool.Count == 0)
            {
                pool.Enqueue(CreateNewObject());
            }

            EnemyController obj = pool.Dequeue();
            obj.gameObject.SetActive(true);

            var children = obj.gameObject.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.GetComponent<EnemyController>())
                    continue;
                
                Destroy(child.gameObject);
            }
            return obj;
        }
        
        public override void ReturnObject(EnemyController obj)
        {
            obj.gameObject.SetActive(false);
            var children = obj.gameObject.GetComponentsInChildren<Transform>();
            foreach (var child in children)
            {
                if (child.GetComponent<EnemyController>())
                    continue;
                
                Destroy(child.gameObject);
            }
            pool.Enqueue(obj);
        }
    }
}
