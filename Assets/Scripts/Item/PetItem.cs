using Data;
using Item;

public class PetItem : ItemBase<int>
{
    public PetInfo petInfo => info as PetInfo;
    
    public PetItem(ItemInfo info) : base(info)
    {
    }

    public PetItem(ItemInfo info, int itemValue) : base(info, itemValue)
    {
    }

    public override void SetValue(int value)
    {
        ItemValue.Value = value;
    }
}
