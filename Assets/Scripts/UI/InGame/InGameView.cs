using System;
using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.InGame
{
    public class InGameView : ViewBase
    {
       public enum InGameButton
       {
           BountyButton,
           SpellButton,
           MineButton,
           Mine1Button,
           Mine3Button,
           Mine4Button,
       }

       public enum InGameText
       {
           GoldText,
           DiamondText,
           ProbabilityText
       }
       
       public Button BountyButton { get; private set; }
       public Button SpellButton { get; private set; }
       public Button MineButton { get; private set; }
       public Button Mine1Button { get; private set; }
       public Button Mine3Button { get; private set; }
       public Button Mine4Button { get; private set; }
       public TextMeshProUGUI GoldText { get; private set; }
       public TextMeshProUGUI DiamondText { get; private set; }
       public TextMeshProUGUI ProbabilityText { get; private set; }
       
       public Transform petCollectionPanel;
       public SpellCollectionPanel spellCollectionPanel;
       
       private void Awake()
       {
           Bind<Button>(typeof(InGameButton));
           Bind<TextMeshProUGUI>(typeof(InGameText));
         
           BountyButton = Get<Button>((int)InGameButton.BountyButton);
           SpellButton = Get<Button>((int)InGameButton.SpellButton);
           MineButton = Get<Button>((int)InGameButton.MineButton);
           Mine1Button = Get<Button>((int)InGameButton.Mine1Button);
           Mine3Button = Get<Button>((int)InGameButton.Mine3Button);
           Mine4Button = Get<Button>((int)InGameButton.Mine4Button);
           GoldText = Get<TextMeshProUGUI>((int)InGameText.GoldText);
           DiamondText = Get<TextMeshProUGUI>((int)InGameText.DiamondText);
           ProbabilityText = Get<TextMeshProUGUI>((int)InGameText.ProbabilityText);
       }
    }
}
