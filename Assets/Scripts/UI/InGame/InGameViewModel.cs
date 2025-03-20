using System.ComponentModel;
using System.Text;
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
           inGameView.SpellButton.onClick.AddListener(OnSpellButtonClicked);
           inGameView.MineButton.onClick.AddListener(OnMinButtonClicked);
           inGameView.Mine1Button.onClick.AddListener(() => MineButtonClicked(1));
           inGameView.Mine3Button.onClick.AddListener(() => MineButtonClicked(3));
           inGameView.Mine4Button.onClick.AddListener(() => MineButtonClicked(4));
           
           player.equippedSpellItems.ListChanged += EquippedSpellItemsOnListChanged;
           player.itemProbabilityLevel.OnValueChanged += OnProbabilityValueChanged;
           player.gold.OnValueChanged += OnGoldValueChanged;
           player.diamond.OnValueChanged += OnDiamondValueChanged;
           
           ShowGoldText(player.gold.Value);
           ShowDiamondText(player.diamond.Value);
           ShowProbabilitiesText(player.itemProbabilityLevel.Value);
       }

       private void OnGoldValueChanged(int oldval, int newval)
       {
           ShowGoldText(newval);
       }
       
       private void OnDiamondValueChanged(int oldval, int newval)
       {
           ShowDiamondText(newval);
       }

       private void OnProbabilityValueChanged(int oldval, int newval)
       {
           ShowProbabilitiesText(newval);
       }

       private void ShowGoldText(int value)
       {
           inGameView.GoldText.text = "gold - " + value;
       }

       private void ShowDiamondText(int value)
       {
           inGameView.DiamondText.text ="diamond - " + value;
       }

       private void ShowProbabilitiesText(int probability)
       {
           var probabilities = GameManager.Instance.GameInfo.itemProbability[probability].probabilities;
           StringBuilder sb = new StringBuilder();

           for (int i = 0; i < probabilities.Length; i++)
           {
               sb.Append($"{i}: {probabilities[0]}% ");
           }
           
           inGameView.ProbabilityText.text = sb.ToString();
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

       private void OnSpellButtonClicked()
       {
           var rItem = GetRandomItem();
           if (rItem == null)
           {
               return;
           }
           
           var itemIndex = player.equippedSpellItems.FindIndex(item => item.info.Id == rItem.Id);
           if (itemIndex < 0)
           {
               var newItem = new SpellItem(rItem, 1);
               player.equippedSpellItems.Add(newItem);
           }
           else
           {
               var newItem = player.equippedSpellItems[itemIndex];
               newItem.ItemValue.Value++;

               player.equippedSpellItems.Remove(newItem);
               player.equippedSpellItems.Add(newItem);
           }
       }

       private void OnMinButtonClicked()
       {
           
       }

       private SpellInfo GetRandomItem()
       {
           var probability = GameManager.Instance.GameInfo.itemProbability;
           var itemPools = GameManager.Instance.GameInfo.itemPools;
           var probabilities = probability[player.itemProbabilityLevel.Value].probabilities;
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

       public void MineButtonClicked(int grade)
       {
           var items = GameManager.Instance.GameInfo.itemPools.Find(p => p.Grade == grade)?.Items;
           if (items == null || items.Count == 0)
           {
               return;
           }
           
           var rItem = items[UnityEngine.Random.Range(0, items.Count)];
           if (rItem == null)
           {
               return;
           }
           
           var itemIndex = player.equippedSpellItems.FindIndex(item => item.info.Id == rItem.Id);
           if (itemIndex < 0)
           {
               var newItem = new SpellItem(rItem, 1);
               player.equippedSpellItems.Add(newItem);
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
}
