using System.Collections.Generic;
using Data;
using InGame.Player;
using Item;
using UnityEngine;

namespace InGame
{
    public class ProjectileShooter : IProjectileHandler
    {
        public SpellInfo spellInfo;
        public ObjectPoolProjectile poolProjectile;
        public PlayerController ownerPlayer;

        private float shootTime;
        
        private List<Projectile> projectiles = new List<Projectile>();

        public void SetShooter(SpellItem item, ObjectPoolProjectile poolProjectile, PlayerController player)
        {
            spellInfo = item.info as SpellInfo;
            this.poolProjectile = poolProjectile;
            ownerPlayer = player;
        }
        
        public void FireProjectile(float time)
        {
            if(shootTime > time)
                return;
            
            shootTime = time + spellInfo.spellInterval;
            var projectile = poolProjectile.GetObject();
            projectile.transform.position = ownerPlayer.transform.position;
            
            projectile.SetProjectile(spellInfo);
            projectile.ownerPlayer = ownerPlayer;
            
            projectile.Fire(poolProjectile).Forget();
            
            projectiles.Add(projectile);
        }

        public void ClearProjectile()
        {
            poolProjectile.Clear();
        }
    }

    public interface IProjectileHandler
    {
        void FireProjectile(float time);
    }
}
