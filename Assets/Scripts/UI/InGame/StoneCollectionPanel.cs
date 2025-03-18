using System.Collections.Generic;
using UnityEngine;

namespace UI.InGame
{
    public class StoneCollectionPanel : MonoBehaviour, ICollectionUpdateHandler
    {
        [SerializeField] private List<StoneEquipmentSlot> slots;
        
        public void UpdateSlots()
        {
            foreach (var slot in slots)
            {
                slot.UpdateIcon();
            }
        }
    }
}
