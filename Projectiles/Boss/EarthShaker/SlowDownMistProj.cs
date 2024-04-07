using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    internal class SlowDownMistProj:ModProjectile
    {
		float rot = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("雾气");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.alpha = 0;
			Projectile.penetrate = -1;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.light = 0;
			Projectile.timeLeft = 50;
		}

		public override void AI()
		{
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] == 1) 
			{
				Projectile.velocity = new Vector2(Main.rand.NextFloat(-0.3f, 0.3f), Main.rand.NextFloat(-0.3f, -0.2f));
				rot = Main.rand.NextFloat(-0.07f, 0.07f);
			}
			float factor = Projectile.localAI[0] / 100f;
			factor = (float)Math.Sqrt(factor);
			Projectile.scale += (1.0f - factor) * 0.02f;
			Projectile.alpha += (int)((1.0f - factor) * 8.5);
			Projectile.rotation += (1.0f - factor) * rot;
			if (Projectile.localAI[0] < 5) 
			{
				Projectile.frame = 0;
			}
			else if(Projectile.localAI[0] < 10)
			{
				Projectile.frame = 1;
			}
			else if (Projectile.localAI[0] < 15)
			{
				Projectile.frame = 2;
			}
			if (Main.rand.NextBool(7)) 
			{
				int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SteampunkSteam, 0.0f, 0.0f, 40, new Color(), 0.5f);
				Main.dust[d].noGravity = true;
				Main.dust[d].fadeIn = 2f;
			}
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			target.AddBuff(BuffID.Slow,30);
            base.OnHitPlayer(target, info);
        }
    }
}
