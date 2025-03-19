using System.Linq;
using Data;
using Manager;
using TMPro;
using UnityEngine;
using Util;

namespace UI.InGame
{
    public class StoneEquipmentSlot : MonoBehaviour
    {
        [SerializeField] private Define.SpellItem itemType;
        [SerializeField] private TextMeshProUGUI itemCountText;
        [SerializeField] private Transform activeIcon;
        [SerializeField] private Transform inactiveIcon;

        private SpellSlotItemIcon icon;

        public void UpdateIcon()
        {
            var player = GameManager.Instance.Player;
            
            if(player != null)
                return;

            foreach (var spellItem in player.equippedSpellItems)
            {
                if (spellItem == null)
                {
                    continue;
                }

                if (spellItem.info is SpellInfo spellInfo)
                {
                    if (spellInfo.itemType == itemType)
                    {
                        activeIcon.gameObject.SetActive(true);
                        inactiveIcon.gameObject.SetActive(false);
                
                        itemCountText.text = spellItem.ItemValue.Value.ToString();
                        return;
                    }
                }
            }
            
            activeIcon.gameObject.SetActive(false);
            inactiveIcon.gameObject.SetActive(true);
            itemCountText.text = string.Empty;
        }
    }
}
