using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.JungleHunter
{
    internal class WitherProjectile : Wither
	{
		private Vector2 speed;
		private int timer;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("枯萎");
		}
		public override void SetDefaults()
		{
			init();
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = true; // Can the projectile deal damage to the player?
			Projectile.tileCollide = false; // 弹丸能与瓦片碰撞吗?
		}


		public override void AI()
		{
			timer++;
			if (timer == 1)
			{
				speed = Projectile.velocity;
			}
			if (timer <= 60)
			{
				Projectile.velocity = new(0, 0);
				Projectile.hostile = false;
				return;
			}
			Projectile.hostile = true;
			Projectile.velocity = speed;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return drawProjectile(ref lightColor);
		}
	}
	class WitherProjectilePlayer : Wither
	{
		public override string Texture => "EmptySet/Projectiles/Boss/JungleHunter/WitherProjectile";
		Rectangle hit;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("枯萎");
		}
		public override void SetDefaults()
		{
			init();
			Projectile.friendly = true; // Can the projectile deal damage to enemies?
			Projectile.hostile = false; // Can the projectile deal damage to the player?
			Projectile.tileCollide = true; // 弹丸能与瓦片碰撞吗?
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return drawProjectile(ref lightColor);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Crit)
			{
				target.AddBuff(BuffID.Poisoned, 60 * 5);
			}
		}
	}
	public class Wither : ModProjectile
	{
		public override string Texture => "EmptySet/Projectiles/Boss/JungleHunter/WitherProjectile";
		public void init()
		{
			Projectile.width = 15;
			Projectile.height = 15;
			DrawOffsetX = -3;
			DrawOriginOffsetY = -5;
			Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
									//Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 2; // 射弹能穿透多少怪物。(下面的OnTileCollide也会减少弹跳穿透)
			Projectile.timeLeft = 6000; // 弹丸的有效时间(60 = 1秒，所以600 = 10秒)
			Projectile.alpha = 255; // 射弹的透明度，255为完全透明。(aiStyle 1快速淡入投射物)如果你没有使用淡入的aiStyle，请确保删除这个。你会奇怪为什么你的投射物是隐形的。
			Projectile.light = 0.5f; // 发射体周围发射出多少光
			Projectile.ignoreWater = false; // 水会影响弹丸的速度吗?
			Projectile.extraUpdates = 1; // 如果你想让投射物在一帧内更新多次，设置为0以上
			AIType = ProjectileID.Bullet; // 就像默认的Bullet一样
		}
		public bool drawProjectile(ref Color lightColor)
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
	}
}
