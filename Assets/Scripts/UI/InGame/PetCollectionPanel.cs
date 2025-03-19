using System;
using System.Collections.Generic;
using System.ComponentModel;
using Manager;
using UnityEngine;

namespace UI.InGame
{
    public class PetCollectionPanel : MonoBehaviour
    {
       [SerializeField] private GameObject petSlotPrefab;
       
       private List<PetSlot> _petSlots;

       private void Start()
       {
           _petSlots = new List<PetSlot>();
               
           CreatePetSlot();
           
           var player = GameManager.Instance.Player;
           player.equippedSpellItems.ListChanged += EquippedSpellItemsOnListChanged;
       }

       private void EquippedSpellItemsOnListChanged(object sender, ListChangedEventArgs e)
       {
           UpdateSlots();
       }

       private void UpdateSlots()
       {
           foreach (var slot in _petSlots)
           {
               slot.UpdateSlot();
           }
       }

       private void CreatePetSlot()
       {
           var player = GameManager.Instance.Player;
           foreach (var petInfo in  player.petInfos)
           {
               var petSlot = Instantiate(petSlotPrefab, transform).GetComponent<PetSlot>();
               petSlot.SetSlot(petInfo);
               petSlot.UpdateSlot();
               
               _petSlots.Add(petSlot);
           }
       }
       
       
    }
}
