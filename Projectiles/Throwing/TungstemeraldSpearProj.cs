using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing
{
    internal class TungstemeraldSpearProj:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("钨翠投矛");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
		}
		public override void SetDefaults()
		{
			Projectile.width = 25;
			Projectile.height = 25;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 10 * EmptySet.Frame;
			Projectile.alpha = 0;
			Projectile.light = 2.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.aiStyle = 1;
            Projectile.penetrate = 5 + 1;
			AIType = ProjectileID.Bullet;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// 用不受光线影响的颜色重新绘制投射体
			Vector2 drawOrigin = new Vector2(texture.Width * 2.5f, Projectile.height * 2.5f);
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
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			return base.OnTileCollide(oldVelocity);
		}
	}
}
