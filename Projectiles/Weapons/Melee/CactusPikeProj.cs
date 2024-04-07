using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 仙人掌长枪弹幕
/// </summary>
public class CactusPikeProj : ModProjectile
{
    public override void SetDefaults()
    { 
        Projectile.CloneDefaults(ProjectileID.Trident);
        Projectile.width = 100; //已精确测量
        Projectile.height = 100;
        Projectile.DamageType = DamageClass.Melee;
        //Projectile.position = -Projectile.position;
        AIType = ProjectileID.TitaniumTrident;
    }
    public override void AI()
    {
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        //Projectile.
    }
}