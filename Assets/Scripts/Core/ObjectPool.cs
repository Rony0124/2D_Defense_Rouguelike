using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public abstract class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] private int initialPoolSize = 10;
        
        protected readonly Queue<T> pool = new Queue<T>();

        protected virtual void Awake()
        {
            //InitializePool();
        }

        public void SetPool(T obj, int poolSize = 10)
        {
            prefab = obj;
            initialPoolSize = poolSize;
        }

        public void InitializePool()
        {
            for (int i = 0; i < initialPoolSize; i++)
            {
                T obj = CreateNewObject();
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        protected T CreateNewObject()
        {
            T obj = Instantiate(prefab, transform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        public T GetObject()
        {
            if (pool.Count == 0)
            {
                pool.Enqueue(CreateNewObject());
            }

            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}