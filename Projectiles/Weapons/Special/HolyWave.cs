using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Special;

/// <summary>
/// 神圣镰气
/// </summary>
public class HolyWave : ModProjectile
{

    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 72;
        Projectile.friendly = true;
        Projectile.penetrate = 10 + 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 12 * EmptySet.Frame;
        Projectile.light = 0.25f;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        
        for (int i = 0; i < 3; i++)
        {
            var pink = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch);
            pink.noGravity = true;
        }
    }
    //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    //{
    //    if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
    //        targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
    //    return projHitbox.Intersects(targetHitbox);
    //}
}
