using System;
using System.Collections.Generic;
using System.ComponentModel;
using Data;
using Item;
using Manager;
using UnityEngine;
using Util;

namespace InGame.Player
{
    public partial class PlayerController : MonoBehaviour, IDamageHandler
    {
        [SerializeField] private SpawnController spawnController;
        
        [Header("Test")]
        [SerializeField] private List<SpellInfo> testSpells;
        [SerializeField] private GameObject poolObj;
        
        public ObservableVar<bool> IsDead;
        
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

     

        private void Start()
        {
            CreateSpells();
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
            Debug.Log("{OnGamePlay} player controller");
            spawnController.InitSpawn();
        }

        private void CreateSpells()
        {
            foreach (var spell in testSpells)
            {
                var spellItem = new SpellItem(spell, 1);
                equippedSpellItems.Add(spellItem);
                
              
            }

            
        }

        private void OnGameEnd()
        {
            spawnController.StopSpawn();
        }

        private void OnDestroy()
        {
            EventBus.Unregister(Define.GameState.GamePlay, OnGamePlay);
            EventBus.Unregister(Define.GameState.GameEnd, OnGameEnd);
            
            IsDead.OnValueChanged -= OnIsDeadValueChanged;
        }
    }
}
