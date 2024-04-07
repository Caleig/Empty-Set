using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss
{
    internal class 血弹 : ModProjectile
	{
		Dust dust;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("血弹");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = false; 
			Projectile.hostile = true;
			Projectile.penetrate = 1; 
			Projectile.timeLeft = 300; 
			Projectile.alpha = 0;
			Projectile.light = 0.1f; 
			Projectile.ignoreWater = true; 
			Projectile.tileCollide = false;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			for (int i = 0; i < 5; i++) 
			{
				dust = Main.dust[Dust.NewDust(Projectile.position, 14, 14, DustID.Blood)];
				dust.noGravity = true;
			}

			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
		/*public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
		}*/
	}
}
