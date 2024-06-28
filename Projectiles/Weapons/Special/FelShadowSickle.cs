using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Special;

/// <summary>
/// 邪恶镰气
/// </summary>
public class FelShadowSickle : ModProjectile
{
    private Vector2 v2;
    private bool is_first;
    
    public override void SetDefaults()
    {
        Projectile.width = 68;
        Projectile.height = 70;
        Projectile.friendly = true;
        Projectile.penetrate = 15 + 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 12 * EmptySet.Frame;
        //Projectile.light = 0.1f;
    }

    public override void AI()
    {
        int to_stop = 5 * EmptySet.Frame;
        int to_kill = 7 * EmptySet.Frame;
        if (!is_first)
        {
            is_first = true;
            v2 = Projectile.velocity;
        }

        if (Projectile.timeLeft > to_kill)
        {
            Projectile.velocity = v2 * (float) ((Projectile.timeLeft - (float)to_kill) / (float)to_stop);
        }
        Projectile.rotation += v2.Length() * 0.05f;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        for (int i = 0; i < 2; i++)
        {
            var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
            dust1.noGravity = true;
        }
    }
    //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    //{
    //    if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
    //        targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
    //    return projHitbox.Intersects(targetHitbox);
    //}
}
