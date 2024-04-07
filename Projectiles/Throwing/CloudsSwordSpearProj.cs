using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing
{
    internal class CloudsSwordSpearProj:ModProjectile
    {
		float r;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("阴云投矛");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
		}
		public override void SetDefaults()
		{
			Projectile.width = 15;
			Projectile.height = 15;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 10 * EmptySet.Frame;
			Projectile.alpha = 0;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.aiStyle = 1;
            Projectile.penetrate = 1;
			AIType = ProjectileID.Bullet;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// 用不受光线影响的颜色重新绘制投射体
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return true;
		}
		public override void AI()
		{
			base.AI();
			if (Main.rand.NextBool(3)) 
			{
				int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.NorthPole);
				Main.dust[dust].noGravity = true;
			}
			if (Main.rand.NextBool(3)) Dust.NewDustPerfect(Projectile.Center, 197, Projectile.velocity).noGravity = true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			Player player = Main.player[Projectile.owner];
            for (int i = 0; i < 3; i ++)
            {
				r = Main.rand.NextFloat(1, 6.28f);
                Vector2 velocity = new Vector2((float)Math.Cos(r), (float)Math.Sin(r)) * 10f;
                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, velocity, ModContent.ProjectileType<Projectiles.Ranged.ChargedArrowProj>(), 100, 10f, player.whoAmI);
            }
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			return base.OnTileCollide(oldVelocity);
		}
	}
}
