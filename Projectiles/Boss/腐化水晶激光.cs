using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.NPCs.Boss.腐化水晶;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss
{
    internal class 腐化水晶激光 : ModProjectile
	{
		private const float MOVE_DISTANCE = 0f;
		Dust dust;

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
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("腐化水晶激光");
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 8;
			//Projectile.alpha = 255;
			Projectile.penetrate = -1;

			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.light = 1;
			Projectile.hide = true;
		}
		public override void AI()
		{
			if(!NPC.AnyNPCs(ModContent.NPCType<腐化水晶>()))
				{
				Projectile.Kill();
			}
			Projectile.position = Main.npc[ParentIndex].Center + new Vector2(-31,10);
			SetLaserPosition(Projectile.Center);
			SpawnDusts(Projectile.Center);
		}
		private void SetLaserPosition(Vector2 center)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 20f)
			{
				var start = center + Projectile.velocity * Distance;
				if (!Collision.CanHit(center, 1, 1, start, 1, 1))
				{
					Distance -= 40f;
					break;
				}
			}
			Distance = 2200f;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			DrawLaser(spriteBatch, ((Texture2D)TextureAssets.Projectile[Projectile.type]), Projectile.position,
					Projectile.velocity, 8, Projectile.damage, -(float)Math.PI / 2, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
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
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = new Color(188, 117, 255, 80);
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 29, 50, 8), i < transDist ? Color.Transparent : c, r, new Vector2(-6, 0), scale, 0, 0);
			}
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition, new Rectangle(0, 0, 50, 29), new Color(1, 1, 1, 0.1f), r, new Vector2(-6, 0), scale, 0, 0);
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition, new Rectangle(0, 37, 50, 34), new Color(1, 1, 1, 0.3f), r, new Vector2(-6, 0), scale, 0, 0);

		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// 运行AABBLine碰撞
			// 它将使用AABB寻找给定线路上的碰撞
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
				Projectile.Center + unit * Distance, 50, ref point);
		}

		private void SpawnDusts(Vector2 center)
		{
			Vector2 dustPos = center + Projectile.velocity * Distance + new Vector2(0, 20);
			int type = 272;//221
			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				dust = Main.dust[Dust.NewDust(dustPos, 0, 0, type, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;

			}
			if (Main.rand.NextBool(3)) 
			{
				dust = Main.dust[Dust.NewDust(Projectile.position+new Vector2(0,30), 62, 10, type, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
		}

		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Terraria.Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			behindNPCs.Add(index);
		}
    }
}
