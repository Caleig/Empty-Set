using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

public class LoudLavaFlowProj : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_0";
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Loud Lava Flow");
    }
    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 100;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 180;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.penetrate = -1;
    }

    public override void AI()
    {
        Dust dust = Main.dust[Dust.NewDust(Projectile.Center, 10, 10, DustID.InfernoFork)];
        dust.noGravity=true;
        dust.velocity.Y = (Main.rand.NextFloat() * 2 - 1) * 6.5f;
        dust.fadeIn = 1.3f;
        dust.noLight = false;
        Lighting.AddLight(dust.position,TorchID.Torch);
    }
}