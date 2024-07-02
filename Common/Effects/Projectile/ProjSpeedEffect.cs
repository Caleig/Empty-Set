using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Projectile;

public class ProjSpeedEffect : GlobalProjectile
{
    public override void OnSpawn(Terraria.Projectile projectile, IEntitySource source)
    {
        if (source is EntitySource_ItemUse_WithAmmo)
        {
            Player? player = (source as EntitySource_ItemUse_WithAmmo).Player;
            if (projectile.DamageType == DamageClass.Throwing && player.armor.Any(x => x.type == ModContent.ItemType<Items.Accessories.MetalBracer>()))
            {
                projectile.velocity *= 1.07f;
            }
            base.OnSpawn(projectile, source);
        }
    }
}
