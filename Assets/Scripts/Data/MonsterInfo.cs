using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "MonsterInfo", menuName = "Data/MonsterInfo", order = int.MaxValue)]
    public class MonsterInfo : ScriptableObject
    {
        public GameObject monsterGraphic;
        public float moveSpeed;
    }
}
