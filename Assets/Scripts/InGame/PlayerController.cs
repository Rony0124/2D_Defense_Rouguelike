using Manager;
using UnityEngine;
using Util;

namespace InGame
{
    public partial class PlayerController : MonoBehaviour
    {
        public ObservableVar<bool> IsDead;

        [SerializeField] private SpawnController spawnController;
        
        private void Awake()
        {
            IsDead = new();
            IsDead.Value = false;
            IsDead.OnValueChanged += OnIsDeadValueChanged;
            
            equippedStoneItems = new();
            
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

        public void TakeDamage()
        {
            
        }

        private void OnGamePlay()
        {
            Debug.Log("{OnGamePlay} player controller");
            spawnController.InitSpawn();
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
