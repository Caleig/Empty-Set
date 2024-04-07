using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item
{
    public class ProjSpeedEffect : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            Player player = source as Player;
            if(projectile.DamageType == DamageClass.Throwing && player.armor.Any(x => x.type == ModContent.ItemType<Items.Accessories.MetalBracer>()))
            {
                projectile.velocity *= 1.07f;
            }
            base.OnSpawn(projectile, source);
        }
    }
}
