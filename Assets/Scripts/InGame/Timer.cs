using System;
using Manager;
using TMPro;
using UnityEngine;
using Util;

namespace InGame
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI gameReadyTimeText;
        
        [SerializeField]
        private TextMeshProUGUI roundTimeText;
        
        [SerializeField]
        private TextMeshProUGUI bountyTimeText;
        
        [SerializeField]
        private int gameReadyDuration;
        
        [SerializeField]
        private int gameRoundDuration;
        
        [SerializeField]
        private int bountyDuration;
        
        private float timerBeginTime;
        
        private bool timerStarted;
        private bool hasBountyTimeReached;

        private int lastGamereadyRemainTime;
        private int lastRoundRemainTime;
        private int lastBountyRemainTime;
        
        private int currentGamereadyGoalTime;
        private int currentRoundGoalTime;
        private int currentBountyGoalTime;
        
        public bool HasBountyTimeReached => hasBountyTimeReached;

        public void InitTimerOnBegin()
        {
            timerBeginTime = Time.time;
            timerStarted = true;
            currentGamereadyGoalTime = gameReadyDuration;
        }

        public void InitTimerOnGamePlay()
        {
            var timeSpan = (int)GetTimeSpan();
            currentRoundGoalTime = timeSpan + gameRoundDuration;
            currentBountyGoalTime = timeSpan+ bountyDuration;
        }

        private void Update()
        {
            if (!timerStarted)
                return;

            if (currentGamereadyGoalTime > 0)
            {
                int gameReadyRemainTime = Math.Max(0, (int)Math.Ceiling(RemainTimeToGoalTime(currentGamereadyGoalTime)));
                if (gameReadyRemainTime > 0)
                {
                    if (lastGamereadyRemainTime != gameReadyRemainTime)
                    {
                        gameReadyTimeText.text = gameReadyRemainTime.ToString();
                        lastGamereadyRemainTime = gameReadyRemainTime;
                    }
                }

                if (gameReadyRemainTime <= 0)
                {
                    currentGamereadyGoalTime = -1;
                    gameReadyTimeText.text = string.Empty;
                    GameManager.Instance.SetGameState(Define.GameState.GamePlay);
                }
            }
            
            if(GameManager.Instance.GameState != Define.GameState.GamePlay)
                return;
            
            int gameRoundRemainTime = Math.Max(0, (int)Math.Ceiling(RemainTimeToGoalTime(currentRoundGoalTime)));
            if (gameRoundRemainTime > 0)
            {
                if (lastRoundRemainTime != gameRoundRemainTime)
                {
                    roundTimeText.text = gameRoundRemainTime.ToString();
                    lastRoundRemainTime = gameRoundRemainTime;
                }
            }

            if (gameRoundRemainTime <= 0)
            {
                currentRoundGoalTime = (int)GetTimeSpan() + gameRoundDuration;
                GameManager.Instance.SetNextGameRound();
            }
            
            if(currentBountyGoalTime < 0)
                return;
            
            int bountyRemainTime = Math.Max(0, (int)Math.Ceiling(RemainTimeToGoalTime(currentBountyGoalTime)));
            if (bountyRemainTime > 0)
            {
                if (lastBountyRemainTime != bountyRemainTime)
                {
                    bountyTimeText.text = bountyRemainTime.ToString();
                    lastBountyRemainTime = bountyRemainTime;
                }
            }
            
            if (bountyRemainTime <= 0)
            {
                bountyTimeText.text = string.Empty;
                hasBountyTimeReached = true;
                currentBountyGoalTime = -1;
            }
        }
        
        private float GetTimeSpan()
        {
            return Time.time - timerBeginTime;
        }
        
        public float RemainTimeToGoalTime(float goalTime)
        {
            return goalTime - GetTimeSpan();
        }

        public void RestartBountyTime()
        {
            hasBountyTimeReached = false;
            currentBountyGoalTime = (int)GetTimeSpan() + bountyDuration;
        }
    }
}