namespace Item
{
    public class StoneItem : ItemBase<int>
    {
        public StoneItem(ItemInfo info) : base(info) { }

        public StoneItem(ItemInfo info, int itemValue) : base(info, itemValue) { }
        
        public override void SetValue(int value)
        {
            ItemValue.Value = value;
        }
    }
}
