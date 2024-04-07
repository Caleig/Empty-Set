using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    internal class 钻地导弹 : ModProjectile
    {
		Vector2 targetPos = Vector2.Zero;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("钻地导弹");
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 38;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = false; 
            Projectile.friendly = false; 
            Projectile.hostile = true; 
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
			Projectile.alpha = 255;
        }

        public override void AI()
        {
			Projectile.localAI[0]++;
            Projectile.rotation = Projectile.velocity.ToRotation()+MathHelper.PiOver2;

			if (Projectile.alpha > 0) Projectile.alpha-=15;

			if (Projectile.localAI[0] == 1) Projectile.velocity = -Vector2.UnitY;

			if (Projectile.velocity.Length() <= 15) Projectile.velocity *= 1.1f;

			if (Projectile.localAI[0] == 120) targetPos = Main.player[(int)Projectile.ai[0]].Center;

			if (Projectile.localAI[0] >= 120) 
			{
				if (Projectile.localAI[1] == 0) Projectile.velocity = (Projectile.velocity + (targetPos - Projectile.Center).SafeNormalize(Vector2.UnitX) * 3).SafeNormalize(Vector2.UnitX) * 12;
				if (Projectile.Center.Distance(targetPos) <= 16) Projectile.localAI[1] = 1;
			}

			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 200, default(Color), 1.5f);

			int frameSpeed = 10;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed)
			{
				Projectile.frameCounter = 0;
				Projectile.frame++;
				if (Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.frame = 0;
				}
			}
		}

        public override void OnKill(int timeLeft)
        {
			Demage();
			//原版爆炸效果
			Projectile.width = (Projectile.height = 176);
			Projectile.Center = Projectile.position;

			SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
			for (int num257 = 0; num257 < 4; num257++)
			{
				int num258 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num258].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
			}

			for (int num259 = 0; num259 < 30; num259++)
			{
				int num260 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 200, default(Color), 3.7f);
				Main.dust[num260].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				Main.dust[num260].noGravity = true;
				Dust dust = Main.dust[num260];
				dust.velocity *= 3f;
				num260 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[num260].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				dust = Main.dust[num260];
				dust.velocity *= 2f;
				Main.dust[num260].noGravity = true;
				Main.dust[num260].fadeIn = 2.5f;
			}

			for (int num261 = 0; num261 < 10; num261++)
			{
				int num262 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 6, 0f, 0f, 0, default(Color), 2.7f);
				Main.dust[num262].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * Projectile.width / 2f;
				Main.dust[num262].noGravity = true;
				Dust dust = Main.dust[num262];
				dust.velocity *= 3f;
			}

			for (int num263 = 0; num263 < 10; num263++)
			{
				int num264 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[num264].position = Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(Projectile.velocity.ToRotation()) * Projectile.width / 2f;
				Main.dust[num264].noGravity = true;
				Dust dust = Main.dust[num264];
				dust.velocity *= 3f;
			}

			for (int num265 = 0; num265 < 2; num265++)
			{
				int num266 = Gore.NewGore(Projectile.GetSource_Death(), Projectile.position + new Vector2((float)(Projectile.width * Main.rand.Next(100)) / 100f, (float)(Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
				Main.gore[num266].position = Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * Projectile.width / 2f;
				Gore gore = Main.gore[num266];
				gore.velocity *= 0.3f;
				Main.gore[num266].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
				Main.gore[num266].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
			}
		}

        private void Demage()
        {
			Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BonmPro>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			Projectile.penetrate--;
			damageDone = 0;
			base.OnHitNPC(target, hit, damageDone);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			Projectile.penetrate--;
			info.Damage = 0;
			base.OnHitPlayer(target, info);
        }
    }
	class BonmPro : ModProjectile 
	{
		public override string Texture => "Terraria/Images/Projectile_0";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("钻地导弹");
		}
		public override void SetDefaults()
		{
			Projectile.width = 170;
			Projectile.height = 170;
			Projectile.light = 0.3f;
			Projectile.ignoreWater = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 2;
		}
	}
}
