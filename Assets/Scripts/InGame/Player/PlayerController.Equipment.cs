using System;
using System.Collections.Generic;
using System.ComponentModel;
using Data;
using Item;
using Util;

namespace InGame.Player
{
    public partial class PlayerController
    {
        public ObservableList<SpellItem> equippedSpellItems;
        public List<PetInfo> petInfos;
        public List<ProjectileShooter> projectileHandlers;

        public ObservableVar<int> itemProbabilityLevel = new();
        public ObservableVar<int> gold = new();
        public ObservableVar<int> diamond = new();
        
        private void EquippedSpellItemsOnListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    var item = equippedSpellItems[e.NewIndex];
                    var spellInfo = item.info as SpellInfo;
                    var pool = Instantiate(poolObj, transform).GetComponent<ObjectPoolProjectile>();
                    if (spellInfo != null)
                    {
                        pool.SetPool(spellInfo.projectilePrefab.GetComponent<Projectile>());
                        pool.InitializePool();
                    }
                    
                    ProjectileShooter shooter = new();
                    shooter.SetShooter(item, pool, this);
                    projectileHandlers.Add(shooter);
                    
                    break;
                case ListChangedType.ItemChanged:
                    break;
                case ListChangedType.ItemDeleted:
                    var rItem = equippedSpellItems[e.NewIndex];
                    var rShooter = projectileHandlers.Find(projectileShooter => projectileShooter.spellInfo.itemType == rItem.spellInfo.itemType);

                    var poolProjectile = rShooter.poolProjectile;
                    Destroy(poolProjectile);

                    projectileHandlers.Remove(rShooter);
                    
                    break;
            }
        }

        public void EnhanceProbabilityLevel()
        {
            itemProbabilityLevel.Value++;
        }
    }
}
