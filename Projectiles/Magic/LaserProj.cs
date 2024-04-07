using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

public class LaserProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Laser");
    }
        
    public override void SetDefaults()
    {
        Projectile.width = 2;
        Projectile.height = 2;
        Projectile.friendly = true;
        Projectile.penetrate = 5;
        Projectile.tileCollide = true;
        Projectile.light = 0.5f;
        Projectile.aiStyle = 1;
        AIType = 440;
    }
}