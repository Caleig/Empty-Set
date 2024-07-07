using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 林翠镰弹幕
/// </summary>
public class JungleSickleProj : ModProjectile
{

    public override void SetDefaults()
    {
        Projectile.width = 30;
        Projectile.height = 30;//54
        Projectile.friendly = true;
        Projectile.penetrate = 2 + 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 2 * EmptySet.Frame;
        Projectile.light = 0.1f;
    }

    public override void AI()
    {
        Projectile.velocity *= 0.97f;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, 0, 0, 0, Color.Lime);
        dust1.noGravity = true;
    }
    //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    //{
    //    if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
    //        targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
    //    return projHitbox.Intersects(targetHitbox);
    //}
}
