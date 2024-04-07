using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    internal class BigShockWaveProjectile:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("大冲击波");
			Main.projFrames[Projectile.type] = 9;
		}

		public override void SetDefaults()
		{
			Projectile.width = 44;
			Projectile.height = 162;
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
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] % 5 == 0) 
			{
				Projectile.localAI[1]++;
				if (Projectile.ai[0] == 1)
					Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + new Vector2(-Projectile.width * Projectile.localAI[1]+22, 80), Vector2.Zero, ModContent.ProjectileType<BigShockWaveProjectile>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
				if (Projectile.ai[0] == 2)
					Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position + new Vector2(Projectile.width * Projectile.localAI[1]+22, 80), Vector2.Zero, ModContent.ProjectileType<BigShockWaveProjectile>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
			}

			int frameSpeed = 3;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.Kill();
				}
			}
		}
	}
}
