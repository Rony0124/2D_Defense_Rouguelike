using System.Linq;
using Data;
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
        
        private PetInfo _petInfo;
        
        public void SetSlot(PetInfo petInfo)
        {
            _petInfo = petInfo;
        }

        public void UpdateSlot()
        {
            Debug.Log("1");
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
                Debug.Log("2");
                activeIcon.gameObject.SetActive(true);
                inactiveIcon.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("3");
                Debug.Log(satisfiedCount);
                inactiveIconText.text = satisfiedCount + "/" + _petInfo.unlockSpells.Length;
                activeIcon.gameObject.SetActive(false);
                inactiveIcon.gameObject.SetActive(true);
            }
        }

        public void SpawnPet()
        {
            
        }
    }
}
