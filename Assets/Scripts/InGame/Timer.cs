using UnityEngine;

namespace InGame
{
    public class Timer : MonoBehaviour
    {
        private float startTime;
        private float elapsedTime;

        public float StartTime => startTime;
        public float ElapsedTime => elapsedTime;

        public void InitTimer()
        {
            startTime = Time.time;
        }
       
        void Update()
        {
            if (!isActiveAndEnabled)
                return;
            
            elapsedTime = Time.time - startTime;
        }
    }
}
