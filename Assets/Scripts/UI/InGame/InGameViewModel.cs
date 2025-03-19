using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Data;
using InGame.Player;
using Item;
using Manager;
using Sirenix.Utilities;
using UI.Core;
using UnityEngine;

namespace UI.InGame
{
    public class InGameViewModel : ViewModelBase
    {
       private InGameView inGameView => view as InGameView;
       private InGameModel inGameModel => model as InGameModel;

       [SerializeField] private PlayerController player;

       private void Start()
       {
           inGameView.BountyButton.onClick.AddListener(OnBountyButtonClicked);
           inGameView.PetButton.onClick.AddListener(OnPetButtonClicked);
           inGameView.SpellButton.onClick.AddListener(OnSpellButtonClicked);
           inGameView.EnhanceButton.onClick.AddListener(OnEnhanceButtonClicked);
           inGameView.MineButton.onClick.AddListener(OnMinButtonClicked);
           
           player.equippedSpellItems.ListChanged += EquippedSpellItemsOnListChanged;
       }

       private void EquippedSpellItemsOnListChanged(object sender, ListChangedEventArgs e)
       {
           if(player.equippedSpellItems.IsNullOrEmpty())
               return;
           
           inGameView.spellCollectionPanel.UpdateSlots();
       }

       private void OnBountyButtonClicked()
       {
           
       }

       private void OnPetButtonClicked()
       {
           
       }

       private void OnSpellButtonClicked()
       {
           var rItem = GetRandomItem();
           if (rItem == null)
           {
               return;
           }
           
           Debug.Log(rItem.itemType);
           
           var itemIndex = player.equippedSpellItems.FindIndex(item => item.info.Id == rItem.Id);
           if (itemIndex < 0)
           {
               var newItem = new SpellItem(rItem, 1);
               player.equippedSpellItems.Add(newItem);
               
               Debug.Log(newItem.ItemValue.Value);
           }
           else
           {
               var newItem = player.equippedSpellItems[itemIndex];
               newItem.ItemValue.Value++;

               player.equippedSpellItems.Remove(newItem);
               player.equippedSpellItems.Add(newItem);
               
               Debug.Log(newItem.ItemValue.Value);
           }
           
          
       }

       private void OnEnhanceButtonClicked()
       {
           
       }

       private void OnMinButtonClicked()
       {
           
       }

       private SpellInfo GetRandomItem()
       {
           var probability = GameManager.Instance.GameInfo.itemProbability;
           var itemPools = GameManager.Instance.GameInfo.itemPools;
           var probabilities = probability[player.itemProbabilityLevel].probabilities;
           var randomValue = UnityEngine.Random.Range(0f, 100f);
           var cumulative = 0f;
           var selectedGrade = 0;
           
           for (var grade = 0; grade < probabilities.Length; grade++)
           {
               cumulative += probabilities[grade];
               if (randomValue < cumulative)
               {
                   selectedGrade = grade;
                   break;
               }
           }
           
           var possibleItems = itemPools.Find(p => p.Grade == selectedGrade)?.Items;
           if (possibleItems == null || possibleItems.Count == 0)
           {
               return null;
           }
           
           return possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)];
       }
    }
}
