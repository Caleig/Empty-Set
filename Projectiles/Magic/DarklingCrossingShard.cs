using EmptySet.Extensions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

public class DarklingCrossingShard : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.scale = 1f;
        Projectile.alpha = 255;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 300;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
        Projectile.alpha -= 50;
        if (Projectile.alpha < 0)
        {
            Projectile.alpha = 0;
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame).position = Projectile.Center;
        }            
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.ShadowFlame, 120);
        if (hit.Crit)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            Projectile.LetExplode(30, hit.Damage / 2);
            Projectile.Kill();
        }
    }
}