using System;
using UI.Core;

namespace UI.InGame
{
    public class InGameViewModel : ViewModelBase
    {
       private InGameView inGameView => view as InGameView;
       private InGameModel inGameModel => model as InGameModel;

       private void Start()
       {
           inGameView.BountyButton.onClick.AddListener(OnBountyButtonClicked);
           inGameView.PetButton.onClick.AddListener(OnPetButtonClicked);
           inGameView.SpellButton.onClick.AddListener(OnSpellButtonClicked);
           inGameView.EnhanceButton.onClick.AddListener(OnEnhanceButtonClicked);
           inGameView.MineButton.onClick.AddListener(OnMinButtonClicked);
       }

       private void OnBountyButtonClicked()
       {
           
       }

       private void OnPetButtonClicked()
       {
           
       }

       private void OnSpellButtonClicked()
       {
           
       }

       private void OnEnhanceButtonClicked()
       {
           
       }

       private void OnMinButtonClicked()
       {
           
       }
    }
}
