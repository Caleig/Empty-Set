using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Social.Base;
using EmptySet.Extensions;
using Terraria.Audio;
using Terraria.ID;
using EmptySet.Projectiles.Whip;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Projectiles.Ranged;
namespace EmptySet.Projectiles.Throwing;
public class EnergyBombsProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        //Projectile.penetrate = 1;
        Projectile.timeLeft = 180;
        //Projectile.alpha = 0;
        Projectile.damage = 100;
        Projectile.tileCollide = true;
        Projectile.light = 0.5f;
        //Projectile.ignoreWater = false;
        //Projectile.tileCollide = true;

        //Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }
    public override void AI()
    {
        Vector2 vector = Vector2.Normalize(Projectile.velocity);
        Projectile.rotation = vector.ToRotation();
        Projectile.velocity.Y += 0.15f;
        if(Projectile.timeLeft <= 1)
        {
            Projectile.ExplodeTiles(Projectile.Center, 8, 0, Main.maxTilesX, 0, Main.maxTilesY, false);
            Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity,
                    ModContent.ProjectileType<Explosion2>(), Projectile.damage, Projectile.knockBack,
                    Projectile.owner);
        }
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 30; i++)
        {
            Dust.NewDustDirect(Projectile.position, 40, 40, DustID.Electric).noGravity = true;
        }
        SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
        base.OnKill(timeLeft);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        hit.Damage = 0;
        Projectile.ExplodeTiles(target.Center, 8, 0, Main.maxTilesX, 0, Main.maxTilesY, false);
        Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.Center, Projectile.velocity,
                ModContent.ProjectileType<Explosion2>(), Projectile.damage, Projectile.knockBack,
                Projectile.owner);
        Projectile.Kill();
        base.OnHitNPC(target, hit, damageDone);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity *= 0.9f;
        Projectile.rotation = 0;
        
        return false;
    }
}
