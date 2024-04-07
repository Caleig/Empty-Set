using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing;

/// <summary>
/// 叶绿鉸弹幕
/// </summary>
public class ChlorophytePikeProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Chlorophyte Pike");

    }
    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = 600;
        Projectile.alpha = 0;
        Projectile.light = 0.1f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = 1;
        Projectile.penetrate = 1;
        AIType = ProjectileID.Bullet;
        DrawOffsetX = -7;
    }
    public override void AI()
    {
        base.AI();
        if (Main.rand.NextBool(2))
        {
            int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Chlorophyte);
            Main.dust[dust].noGravity = true;
        }
        if (Main.rand.NextBool(5))
            Dust.NewDustPerfect(Projectile.Center, DustID.Chlorophyte, Projectile.velocity).noGravity = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        for (int i = 0; i < 5; i++)
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0, MathHelper.Pi * 2)) * 5, ProjectileID.SporeCloud, Projectile.damage, Projectile.knockBack, Main.myPlayer);
        return base.OnTileCollide(oldVelocity);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit,damageDone);
        for (int i = 0; i < 5; i++)
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0, MathHelper.Pi * 2)) * 5, ProjectileID.SporeCloud, Projectile.damage, Projectile.knockBack, Main.myPlayer);
    }
}