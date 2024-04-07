using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

public class 冷彻弹幕 : ModProjectile
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Sunplate Blade");
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CopperShortswordStab);
        Projectile.width = 36;
        Projectile.height = 44;
    }
    public override void AI()
    {
        //Projectile.rotation = Projectile.velocity.Length() + MathHelper.ToRadians(0f);
    }
}