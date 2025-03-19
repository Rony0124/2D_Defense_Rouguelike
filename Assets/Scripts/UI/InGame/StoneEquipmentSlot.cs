using System.Linq;
using Data;
using Manager;
using UnityEngine;
using Util;

namespace UI.InGame
{
    public class StoneEquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Define.SpellItem itemType;
        
        [SerializeField] private Transform activeIcon;
        [SerializeField] private Transform inactiveIcon;

        private SpellSlotItemIcon icon;

        public void UpdateIcon()
        {
            var player = GameManager.Instance.Player;
            
            if(player != null)
                return;

            if (player.equippedSpellItems
                .Select(spell => spell.info as SpellInfo)
                .Any(info => info != null && info.itemType == itemType))
            {
                activeIcon.gameObject.SetActive(true);
                inactiveIcon.gameObject.SetActive(false);
                return;
            }
            
            activeIcon.gameObject.SetActive(false);
            inactiveIcon.gameObject.SetActive(true);
        }
    }
}
