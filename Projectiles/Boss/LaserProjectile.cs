using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Utils;
using System;
using EmptySet.Extensions;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Projectiles.Boss
{
    class LaserProjectile:ModProjectile
    {
		private const float MOVE_DISTANCE = 0f;

		public float Distance
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		public int ParentIndex
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		Vector2 nextProjectliePosition;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("激光");
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
			Projectile.light = 1;
		}
		public override void AI()
		{
			Projectile.timeLeft = 2;
			Projectile.position = Main.projectile[ParentIndex].Center;
			nextProjectliePosition = (Main.projectile[ParentIndex].Center - Main.npc[(int)Main.projectile[ParentIndex].ai[1]].Center)
                .RotatedBy(MathHelper.ToRadians(45)) + Main.npc[(int)Main.projectile[ParentIndex].ai[1]].Center;
			
			Projectile.velocity = (nextProjectliePosition - Projectile.Center).SafeNormalize(Vector2.UnitX);
			SetLaserPosition(Main.projectile[ParentIndex].position);
			SpawnDusts(Main.projectile[ParentIndex].Center);
		}
		private void SetLaserPosition(Vector2 center)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = center + Projectile.velocity * Distance;
				if (!Collision.CanHit(center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			DrawLaser(spriteBatch, ((Texture2D)TextureAssets.Projectile[Projectile.type]), Main.projectile[ParentIndex].Center,
					Projectile.velocity, 5, Projectile.damage, -(float)Math.PI/2, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			return false;
		}


		// 绘制激光的核心功能
		/**
		 * start 开始位置
		 * unit 方向
		 * step 每次绘图间隔
		 * rotation 素材绘制旋转角度
		 * scale 缩放
		 * maxDist 最远距离
		 * color 颜色
		 * transDist 绘制起点与start的距离
		 */
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 26, 26, 26), i < transDist ? Color.Transparent : c, r, new Vector2(26 * .5f,0), scale, 0, 0);
			}

			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition, new Rectangle(0, 0, 26, 22), Color.White, r, new Vector2(26 * .5f,0), scale, 0, 0);

			// Draws the laser 'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition, new Rectangle(0, 56, 26, 22), Color.White, r, new Vector2(26 * .5f, 0), scale, 0, 0);

		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// 运行AABB和Line检查，寻找碰撞，首先查找AABB碰撞，看看它是如何工作的
			// 它将使用AABB寻找给定线路上的碰撞
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Main.projectile[ParentIndex].Center,
				Main.projectile[ParentIndex].Center + unit * Distance, 22, ref point);
		}

		private void SpawnDusts(Vector2 center)
		{
			Vector2 unit = Projectile.velocity * -1;
			Vector2 dustPos = center + Projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				/*dust = Dust.NewDustDirect(Main.projectile[ParentIndex].Center, 0, 0, 31,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.Cyan;*/
			}

			if (Main.rand.NextBool(5))
			{
				/*Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, 31, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - Main.projectile[ParentIndex].Center;
				unit.Normalize();*/
				Dust dust = Main.dust[Dust.NewDust(Main.projectile[ParentIndex].position, 26, 26, DustID.Electric, 0.0f, -0.5f, 0, new Color(), 1f)];
				dust.noGravity = true;
				dust.fadeIn = 1.5f;
			}
		}

		public override void CutTiles()
		{

			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Terraria.Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}
	}
}
