using Data;
using Data.Registry;
using Item;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace UI.InGame
{
    public class SpellSlotItemIcon : MonoBehaviour, IPointerClickHandler
    {
        private SpellItem item;

        float interval = 0.25f;
        float doubleClickedTime = -1.0f;

        public void SetIcon(SpellItem data)
        {
            item = data;
        }

        public void OnPointerClick(PointerEventData eData)
        {
            if (Time.time - doubleClickedTime < interval)
            {
                doubleClickedTime = -1.0f;
               
                var player = GameManager.Instance.Player;
                
                var index = player.equippedSpellItems
                    .FindIndex(equippedItem => equippedItem.spellInfo.itemType == item.spellInfo.itemType);
                
                if (index < 0)
                {
                    return;
                }

                var selectedSpellItem = player.equippedSpellItems[index];
                
                if (selectedSpellItem.ItemValue.Value >= 3)
                {
                    if (selectedSpellItem.spellInfo.itemType
                        is Define.SpellItem.Fire_5 or Define.SpellItem.Ground_5 or Define.SpellItem.Ice_5)
                    {
                        return;
                    }
                    
                    selectedSpellItem.ItemValue.Value -= 3;

                    if (selectedSpellItem.ItemValue.Value <= 0)
                    {
                        player.equippedSpellItems.RemoveAt(index);
                    }
                    else
                    {
                        player.equippedSpellItems.RemoveAt(index);
                        player.equippedSpellItems.Add(selectedSpellItem);
                    }
                    
                    int newItemType = (int)selectedSpellItem.spellInfo.itemType + 1;
                    
                    var itemIndex = player.equippedSpellItems
                        .FindIndex(spellItem => (int)spellItem.spellInfo.itemType == newItemType);
                    
                    if (itemIndex < 0)
                    {
                        foreach (var info in DataRegistry.Instance.SpellRegistry.assetGuidLookup.Values)
                        {
                            if (newItemType == (int)info.itemType)
                            {
                                SpellItem spellItem = new(info, 1);
                                player.equippedSpellItems.Add(spellItem);
                            }
                        }
                    }
                    else
                    {
                        var newItem = player.equippedSpellItems[itemIndex];
                        newItem.ItemValue.Value++;

                        player.equippedSpellItems.Remove(newItem);
                        player.equippedSpellItems.Add(newItem);
                    }
                }
            }
            else
            {
                doubleClickedTime = Time.time;
            }
        }
        
    }
}
