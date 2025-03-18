using System;
using UnityEngine;

namespace InGame
{
    public partial class PlayerController : MonoBehaviour
    {
        private void Awake()
        {
            equippedStoneItems = new();
        }
    }
}
