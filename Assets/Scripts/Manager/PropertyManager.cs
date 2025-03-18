using Core;
using Item;
using UnityEngine;

namespace Manager
{
    public class PropertyManager : Singleton<PropertyManager>
    {
        [SerializeField] private CurrencyItemInfo goldInfo;
        [SerializeField] private CurrencyItemInfo diamondInfo;
        
        private CurrencyItem gold;
        private CurrencyItem diamond;

        public CurrencyItem Gold => gold ??= new CurrencyItem(goldInfo, 0);
        public CurrencyItem Diamond => diamond ??= new CurrencyItem(diamondInfo, 0);
    }
}
