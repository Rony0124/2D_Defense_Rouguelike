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
        
        public ObservableVar<bool> IsDead;
        public Transform PetSpawnPoint => petSpawnPoint;
        public PetController PetController => petController;
        
        private void Awake()
        {
            IsDead = new();
            IsDead.Value = false;
            IsDead.OnValueChanged += OnIsDeadValueChanged;
            
            equippedSpellItems = new();
            equippedSpellItems.ListChanged += EquippedSpellItemsOnListChanged;

            projectileHandlers = new();
            
            EventBus.Register(Define.GameState.GamePlay, OnGamePlay);
            EventBus.Register(Define.GameState.GameEnd, OnGameEnd);
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
