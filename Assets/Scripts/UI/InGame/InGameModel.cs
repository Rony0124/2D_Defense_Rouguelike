using Manager;
using UI.Core;
using Util;

namespace UI.InGame
{
    public class InGameModel : ModelBase
    {
        private ObservableVar<int> gold;
        private ObservableVar<int> diamond;

        private void Awake()
        {
            gold = new();
            diamond = new();
            
            gold.Value = PropertyManager.Instance.Gold.ItemValue.Value;
            diamond.Value = PropertyManager.Instance.Diamond.ItemValue.Value;
            
            gold.OnValueChanged += OnGoldChanged;
            diamond.OnValueChanged += OnDiamondChanged;
        }

        private void OnGoldChanged(int oldVal, int newVal)
        {
            if (oldVal == newVal)
                return;
            
            PropertyManager.Instance.Gold.ItemValue.Value = newVal;
        }
        
        private void OnDiamondChanged(int oldVal, int newVal)
        {
            if (oldVal == newVal)
                return;
            
            PropertyManager.Instance.Diamond.ItemValue.Value = newVal;
        }
    }
}
