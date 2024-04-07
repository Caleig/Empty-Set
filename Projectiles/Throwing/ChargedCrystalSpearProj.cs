using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing
{
    internal class ChargedCrystalSpearProj:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("能晶投矛");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
		}
		public override void SetDefaults()
		{
			Projectile.width = 25;
			Projectile.height = 25;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Throwing;
			Projectile.timeLeft = 600;
			Projectile.alpha = 0;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.aiStyle = 1;
			AIType = ProjectileID.Bullet;
		}

		public override bool PreDraw(ref Color lightColor)
		{
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

        public override void AI()
        {
            base.AI();
			if (Main.rand.NextBool(5)) 
			{
				int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Frost);
				Main.dust[dust].noGravity = true;
			}
			if (Main.rand.NextBool(5)) Dust.NewDustPerfect(Projectile.Center, DustID.Frost, Projectile.velocity).noGravity = true;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			return base.OnTileCollide(oldVelocity);
        }
    }
}
