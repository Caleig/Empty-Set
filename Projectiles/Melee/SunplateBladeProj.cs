using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace EmptySet.Projectiles.Melee
{
    internal class SunplateBladeProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunplate Blade");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.CopperShortswordStab);
            Projectile.width = 30;
            Projectile.height = 30;
        }
    }
}
