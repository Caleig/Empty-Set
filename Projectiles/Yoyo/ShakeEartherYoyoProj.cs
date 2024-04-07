using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Yoyo
{
    internal class ShakeEartherYoyoProj:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("震撼悠悠球");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.WoodYoyo);
			Projectile.width = 20;
			Projectile.height = 20;
			//Projectile.extraUpdates = 1;
			//AIType = ProjectileID.WoodYoyo;
		}
	}
}
