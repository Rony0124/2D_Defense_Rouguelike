using Manager;
using UnityEngine;
using Util;

namespace UI.InGame
{
    public class StoneEquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Define.StoneItem itemType;
        
        [SerializeField] private Transform activeIcon;
        [SerializeField] private Transform inactiveIcon;

        private StoneSlotItemIcon icon;

        public void UpdateIcon()
        {
            var player = GameManager.Instance.Player;
            
            if(player != null)
                return;

            if (player.equippedStoneItems.TryGetValue(itemType, out var stoneItem))
            {
                activeIcon.gameObject.SetActive(true);
                inactiveIcon.gameObject.SetActive(false);
            }
            else
            {
                activeIcon.gameObject.SetActive(false);
                inactiveIcon.gameObject.SetActive(true);
            }
        }
    }
}
