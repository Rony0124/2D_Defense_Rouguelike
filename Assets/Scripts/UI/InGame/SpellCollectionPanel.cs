using System.Collections.Generic;
using UnityEngine;

namespace UI.InGame
{
    public class SpellCollectionPanel : MonoBehaviour, ICollectionUpdateHandler
    {
        [SerializeField] private List<SpellEquipmentSlot> slots;
        
        public void UpdateSlots()
        {
            foreach (var slot in slots)
            {
                slot.UpdateIcon();
            }
        }
    }
}
