using Cysharp.Threading.Tasks;
using Data;
using InGame;
using UnityEngine;
using Util;
using PlayerController = InGame.Player.PlayerController;

namespace Manager
{
    public class GameManager : Core.Singleton<GameManager>
    {
        [Header("Game Info")]
        [SerializeField]
        private GameInfo gameInfo;
        
        [Header("Game Time")]
        [SerializeField]
        private Timer timer;
        [SerializeField]
        private float gameReadyDuration;
        
        [Header("Player")]
        [SerializeField]
        private PlayerController player;
        
        private ObservableVar<Define.GameState> gameState;
        private int currentGameDifficulty;
        
        public PlayerController Player => player;
        public Define.GameState GameState => gameState.Value;
        public int CurrentGameDifficulty => currentGameDifficulty;
        
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

        public void SetGameState(Define.GameState gameState)
        {
            this.gameState.Value = gameState;
        }
        
        private void OnGameStateChanged(Define.GameState prevState, Define.GameState newState)
        {
            if(prevState == newState)
                return;
            
            Debug.Log($"Prev GameState - {prevState} : Current GameState - {newState}");
            EventBus.Publish(newState);
        }

        private async UniTaskVoid OnGameBegin()
        {
           // timer.InitTimer();
            
            await UniTask.WaitForSeconds(gameReadyDuration);
            
            gameState.Value = Define.GameState.GamePlay;
        }

        public GameInfo.RoundData GetCurrentRoundInfoByDifficulty(int difficulty)
        {
            var roundIndex = difficulty % gameInfo.roundDataList.Count;

            return gameInfo.roundDataList[roundIndex];
        }

        public float GetDifficultyValue(GameInfo.DifficultyType difficultyType, int difficulty)
        {
            return gameInfo.GetDifficultyValue(difficultyType, difficulty);
        }
    }
}
