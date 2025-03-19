using System;
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
           PetButton,
           SpellButton,
           EnhanceButton,
           MineButton,
       }
       
       public Button BountyButton { get; private set; }
       public Button PetButton { get; private set; }
       public Button SpellButton { get; private set; }
       public Button EnhanceButton { get; private set; }
       public Button MineButton { get; private set; }

       public SpellCollectionPanel spellCollectionPanel;
       
       private void Awake()
       {
           Bind<Button>(typeof(InGameButton));
           
           BountyButton = Get<Button>((int)InGameButton.BountyButton);
           PetButton = Get<Button>((int)InGameButton.PetButton);
           SpellButton = Get<Button>((int)InGameButton.SpellButton);
           EnhanceButton = Get<Button>((int)InGameButton.EnhanceButton);
           MineButton = Get<Button>((int)InGameButton.MineButton);
       }
    }
}
