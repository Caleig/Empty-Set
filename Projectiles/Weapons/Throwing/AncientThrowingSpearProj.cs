using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 古遗投矛弹幕
/// </summary>
public class AncientThrowingSpearProj : ModProjectile
{
    Dust dust;
    public override void SetDefaults()
    {
        Projectile.width = 8;//22; //已精确测量
        Projectile.height = 8;//70;
        Projectile.friendly = true;
        Projectile.penetrate = 3 + 1;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        Projectile.DamageType = DamageClass.Throwing;
        DrawOffsetX = -4;
    }
    public override void AI() 
    {
        Projectile.localAI[0]++;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        if (Projectile.localAI[0] == 45) Projectile.velocity *= 2.7f;
        if (Main.rand.NextBool(3)) 
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(0, 60).RotatedBy(Projectile.rotation), 1, 1, DustID.Sandnado);
            dust.noGravity = true;
            dust.fadeIn = 0.8f;
        }
        if (Main.rand.NextBool(3))
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(0, 60).RotatedBy(Projectile.rotation), 1, 1, DustID.Flare_Blue);
            dust.noGravity = true;
            dust.fadeIn = 0.8f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
}