using System.Collections.Generic;
using Data;
using Manager;
using UnityEngine;
using Util;

namespace InGame
{
    public class SpawnController : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private ObjectPoolEnemy enemyPool;
        [SerializeField] private Transform nextSpawnPoint;
        [SerializeField] private Transform endPoint;

        private List<EnemyController> enemies;
        private bool canSpawn;
        
        void Awake()
        {
            enemies = new();
        }

        public void InitSpawn()
        {
            canSpawn = true;
        }

        public void StopSpawn()
        {
            canSpawn = false;
        }

        private void ClearAllEnemy()
        {
            foreach (var enemy in enemies)
            {
                Destroy(enemy);
            }
            
            enemies.Clear();
        }
        
        void Update()
        {
            if (CanSpawnEnemy())
            {
                SpawnEnemy();
            }
        }
        
        private bool CanSpawnEnemy()
        {
            if (!canSpawn)
                return false;
            
            if (GameManager.Instance.GameState != Define.GameState.GamePlay)
                return false;
           
            if (nextSpawnPoint == null)
                return false;
            
            if (enemies.Count <= 0)
                return true;

            var lastEnemy = GetLastEnemy();
            if (lastEnemy == null)
                return false;

            return lastEnemy.transform.position.y >= nextSpawnPoint.transform.position.y;
        }

        private void SpawnEnemy()
        {
            var currentEnemy = enemyPool.GetObject();
            var difficulty = GameManager.Instance.CurrentGameDifficulty;
            var monsterInfo = GameManager.Instance.GetCurrentRoundInfoByDifficulty(difficulty).normalMonsterInfo;
            var power = GameManager.Instance.GetDifficultyValue(GameInfo.DifficultyType.Power, difficulty);
            var health = GameManager.Instance.GetDifficultyValue(GameInfo.DifficultyType.Health, difficulty);
            
            currentEnemy.Initialize(monsterInfo, power,  health, endPoint);
            
            enemies.Add(currentEnemy);
        }
        
        private bool IsValidEnemy(EnemyController enemyController)
        {
            if (enemyController == null)
                return false;

            if (enemyController.IsDead.Value)
                return false;

            return true;
        }

        private EnemyController GetLastEnemy()
        {
            if (enemies.Count <= 0)
                return null;
            
            for (int i = enemies.Count - 1; i >= 0 ; --i)
            {
                var enemy = enemies[i];
                
                if (IsValidEnemy(enemy))
                    return enemy;
            }

            return null;
        }
    }
}
