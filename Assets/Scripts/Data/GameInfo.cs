using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "Data/GameInfo", order = int.MaxValue)]
    public class GameInfo : ScriptableObject
    {
        [Serializable]
        public struct RoundData
        {
            public MonsterInfo normalMonsterInfo;
            public MonsterInfo bossMonsterInfo;
        }
        
        [Serializable]
        public struct DifficultyValue
        {
            public DifficultyType valueType;
            public AnimationCurve curve;
        }
        
        public enum DifficultyType
        {
            Power,
            Health
        }

        [Header("Round")]
        public List<RoundData> roundDataList;
        
        [Header("Difficulty")]
        public List<DifficultyValue> difficultyValues;
        
        public float GetDifficultyValue(DifficultyType valueType, float time, float defaultValue = 0.0f)
        {
            var curve = GetDifficultyValueCurve(valueType);

            if (curve != null)
            {
                return curve.Evaluate(time);
            }
        
            return defaultValue;
        }
        
        private AnimationCurve GetDifficultyValueCurve(DifficultyType valueType)
        {
            for (int i = 0; i < difficultyValues.Count; ++i)
            {
                if (difficultyValues[i].valueType == valueType)
                    return difficultyValues[i].curve;
            }
            
            return null;
        }
    }
}
