using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    class AttackAimedProjectile:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("准心");
		}

		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 1;
			//Projectile.alpha = 255;
			Projectile.penetrate = -1;

			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.light = 0.5f;
			Projectile.timeLeft = 160;
		}
		public override void AI()
		{
			if (Projectile.ai[1] == 0) 
			{
				Projectile.position = Main.player[(int)Projectile.ai[0]].Center - new Vector2(15,75);
			}
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return false;
		}
	}
}
