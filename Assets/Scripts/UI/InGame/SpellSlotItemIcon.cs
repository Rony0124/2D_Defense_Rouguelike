using Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class SpellSlotItemIcon : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        private SpellItem item;

        public void SetStoneItem(SpellItem item)
        {
            this.item = item;
            
            iconImage.sprite = item.info.icon;
        }
    }
}
