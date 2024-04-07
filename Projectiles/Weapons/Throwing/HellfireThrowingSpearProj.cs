using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 狱炎投矛弹幕
/// </summary>
public class HellfireThrowingSpearProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 9;//18; //已精确测量
        Projectile.height = 9;//48;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        Projectile.aiStyle = -1;
    }
    
    public override void AI()
    {
        Projectile.ai[0] ++;
        if (Projectile.ai[0] == 1) Projectile.ai[1] = Projectile.damage;
        if (Projectile.ai[0] >= 40) 
        {
            Projectile.velocity.Y += 0.5f;
            Projectile.damage = (int)Projectile.ai[1] * 2;
        }
        Projectile.rotation = Projectile.velocity.ToRotation()+MathHelper.PiOver2;
        int dust = Dust.NewDust(Projectile.Hitbox.Location.ToVector2() + new Vector2(4, 0), 3, 3, DustID.Torch);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].fadeIn = 0.8f;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.Next(100) < 70)
            target.AddBuff(BuffID.OnFire, 7 * EmptySet.Frame);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
}
