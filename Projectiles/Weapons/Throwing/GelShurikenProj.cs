using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 凝胶手里剑弹幕
/// </summary>
public class GelShurikenProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 32; //已精确测量
        Projectile.height = 32;
        Projectile.friendly = true;
        Projectile.penetrate = 1 + 1;
        Projectile.timeLeft = (int)(5.5 * EmptySet.Frame);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        //SoundEngine.PlaySound(SoundID.Item63, Projectile.position);
        for (int i = 0; i < 5; i++)
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Crimslime);
            dust.noGravity = true;
        }
    }
    public override void AI()
    {
        Projectile.rotation += 0.05f * Projectile.velocity.Length();
        var red = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,DustID.RedTorch,0,0,0,default,0.6f);
        red.noGravity = true;
        var yellow = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.YellowTorch);
        yellow.noGravity = true;
        var blue = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch);
        blue.noGravity = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
}