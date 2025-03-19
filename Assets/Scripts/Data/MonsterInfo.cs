using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterInfo", menuName = "Data/MonsterInfo", order = int.MaxValue)]
    public class MonsterInfo : ScriptableObject
    {
        public Sprite monsterGraphic;
        public float moveSpeed;
    }
}
