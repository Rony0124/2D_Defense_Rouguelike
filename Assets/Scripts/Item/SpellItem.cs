using Data;

namespace Item
{
    public class SpellItem : ItemBase<int>
    {
        public SpellInfo spellInfo => info as SpellInfo;
        public SpellItem(ItemInfo info) : base(info) { }

        public SpellItem(ItemInfo info, int itemValue) : base(info, itemValue) { }
        
        public override void SetValue(int value)
        {
            ItemValue.Value = value;
        }
    }
}
