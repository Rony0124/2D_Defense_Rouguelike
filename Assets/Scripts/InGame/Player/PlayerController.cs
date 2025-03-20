using System;
using Manager;
using UnityEngine;
using Util;

namespace InGame.Player
{
    public partial class PlayerController : MonoBehaviour, IDamageHandler
    {
        [SerializeField] private SpawnController spawnController;
        
        [SerializeField] private GameObject poolObj;
        [SerializeField] private Transform petSpawnPoint;
        
        private PetController petController;

        public ObservableVar<float> currentHealth;
        public float maxHealth;
        
        public ObservableVar<bool> IsDead;
        public Transform PetSpawnPoint => petSpawnPoint;
        public PetController PetController => petController;
        public SpawnController SpawnController => spawnController;
        
        private void Awake()
        {
            IsDead = new();
            IsDead.Value = false;
            IsDead.OnValueChanged += OnIsDeadValueChanged;
            
            equippedSpellItems = new();
            equippedSpellItems.ListChanged += EquippedSpellItemsOnListChanged;

            projectileHandlers = new();

            currentHealth = new();
            currentHealth.Value = maxHealth;
            currentHealth.OnValueChanged += OnHealthValueChanged;
            
            EventBus.Register(Define.GameState.GamePlay, OnGamePlay);
            EventBus.Register(Define.GameState.GameEnd, OnGameEnd);
        }
        
        private void OnHealthValueChanged(float oldVal, float newVal)
        {
            if (newVal <= 0)
            {
                IsDead.Value = true;
            }
        }

        private void OnIsDeadValueChanged(bool oldVal, bool newVal)
        {
            if (newVal)
            {
                GameManager.Instance.SetGameState(Define.GameState.GameEnd);
            }
        }

        public void Update()
        {
            if (projectileHandlers.Count < 0 && IsDead.Value)
            {
                return;
            }

            foreach (var shooter in projectileHandlers)
            {
                shooter.FireProjectile(Time.time);
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth.Value -= damage;
        }

        private void OnGamePlay()
        {
            spawnController.InitSpawn();
        }

        private void OnGameEnd()
        {
            spawnController.StopSpawn();
        }

        public void SetPet(PetController pet)
        {
            petController = pet;
        }

        private void OnDestroy()
        {
            EventBus.Unregister(Define.GameState.GamePlay, OnGamePlay);
            EventBus.Unregister(Define.GameState.GameEnd, OnGameEnd);
            
            IsDead.OnValueChanged -= OnIsDeadValueChanged;
        }
    }
}
