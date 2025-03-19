using Data;

namespace Item
{
    public class CurrencyItem : ItemBase<int>
    {
        public CurrencyItem(ItemInfo info) : base(info) { }

        public CurrencyItem(ItemInfo info, int itemValue) : base(info, itemValue) { }

        public override void SetValue(int value)
        {
            ItemValue.Value = value;
        }
    }
}
