using System.Linq;
using Data;
using InGame;
using Manager;
using TMPro;
using UnityEngine;

namespace UI.InGame
{
    public class PetSlot : MonoBehaviour
    {
        [SerializeField] private Transform inactiveIcon;
        [SerializeField] private TextMeshProUGUI inactiveIconText;
        [SerializeField] private Transform activeIcon;
        [SerializeField] private TextMeshProUGUI activeIconText;
        
        private PetInfo _petInfo;
        
        public void SetSlot(PetInfo petInfo)
        {
            _petInfo = petInfo;
        }

        public void UpdateSlot()
        {
            var player = GameManager.Instance.Player;
            var satisfiedCount = 0;
            
            foreach (var spellItem in player.equippedSpellItems)
            {
                if (_petInfo.unlockSpells.Contains(spellItem.spellInfo.itemType))
                {
                    ++satisfiedCount;
                }
            }

            if (satisfiedCount == _petInfo.unlockSpells.Length)
            {
                activeIcon.gameObject.SetActive(true);
                inactiveIcon.gameObject.SetActive(false);
            }
            else
            {
                inactiveIconText.text = satisfiedCount + "/" + _petInfo.unlockSpells.Length;
                activeIcon.gameObject.SetActive(false);
                inactiveIcon.gameObject.SetActive(true);
            }
        }

        public void SpawnPet()
        {
            var player = GameManager.Instance.Player;
            var pet = Instantiate(_petInfo.petPrefab, player.PetSpawnPoint).GetComponent<PetController>();
            
            player.SetPet(pet);

            activeIconText.text = "소환완료";
        }
    }
}
