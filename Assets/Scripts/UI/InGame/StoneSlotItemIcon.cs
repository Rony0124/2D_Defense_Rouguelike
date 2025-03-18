using Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InGame
{
    public class StoneSlotItemIcon : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        
        private StoneItem item;

        public void SetStoneItem(StoneItem item)
        {
            this.item = item;
            
            iconImage.sprite = item.info.icon;
        }
    }
}
