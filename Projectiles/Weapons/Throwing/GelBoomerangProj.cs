using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 凝胶回旋镖弹幕
/// </summary>
public class GelBoomerangProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 26; //已精确测量
        Projectile.height = 38;
        Projectile.aiStyle = 3;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Throwing;
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
            DustID.BlueTorch);
        dust.noGravity = true;
    }
}