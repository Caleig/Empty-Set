using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 恒灰镰刀弹幕
/// </summary>
public class EternityAshSickleProj : ModProjectile
{

    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 44;//54
        Projectile.friendly = true;
        Projectile.penetrate = 3 + 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 12 * EmptySet.Frame;
        Projectile.light = 0.1f;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
        dust1.noGravity = true;
    }
    //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    //{
    //    if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
    //        targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
    //    return projHitbox.Intersects(targetHitbox);
    //}
}
