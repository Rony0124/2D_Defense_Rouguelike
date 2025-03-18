using Cysharp.Threading.Tasks;
using InGame;
using UnityEngine;
using Util;

namespace Manager
{
    public class GameManager : Core.Singleton<GameManager>
    {
        [Header("Game Time")]
        [SerializeField]
        private Timer timer;
        [SerializeField]
        private float gameReadyDuration;
        
        private ObservableVar<Define.GameState> gameState;
        
        private void Awake()
        {
            gameState = new ObservableVar<Define.GameState>();
            
            gameState.OnValueChanged += OnGameStateChanged;
            
            EventBus.Register(Define.GameState.GameBegin, () => OnGameBegin().Forget());
        }

        private void Start()
        {
            gameState.Value = Define.GameState.GameBegin;
        }
        
        private void OnGameStateChanged(Define.GameState prevState, Define.GameState newState)
        {
            if(prevState == newState)
                return;

            EventBus.Publish(newState);
        }

        private async UniTaskVoid OnGameBegin()
        {
            timer.InitTimer();
            
            await UniTask.WaitForSeconds(gameReadyDuration);
            
            gameState.Value = Define.GameState.GamePlay;
        }
        
        
    }
}
