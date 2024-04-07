using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Magic;

/// <summary>
/// 凝胶法杖弹幕
/// </summary>
public class GelStaffProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 16; //已精确测量
        Projectile.height = 14;

        Projectile.friendly = true;
        Projectile.timeLeft = 7 * EmptySet.Frame;
        Projectile.alpha = 128;

        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch);
        dust.noGravity = true;
        Projectile.rotation += Projectile.velocity.ToRotation() * 0.07f;
    }
}