using Microsoft.Xna.Framework;
using EmptySet.Buffs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Summon
{
    class ChargedUAVProjectile:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("充能无人机");
			// 设置这个召唤物在它的精灵表上的帧数
			Main.projFrames[Projectile.type] = 4;
			// 这对于右击目标是必要的
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // 表示此射弹是宠物或仆从
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // 这是必要的，这样你的仆从可以正确地生成时，当其他仆从被召唤时替换
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // 让信徒抵抗这一射弹，因为它对所有归航射弹都是抵抗的。
		}

		public sealed override void SetDefaults()
		{
			Projectile.width = 54;
			Projectile.height = 30;
			Projectile.tileCollide = false; // 让仆从自由地穿过瓷砖

			// 下面的这些是召唤武器需要的
			Projectile.friendly = true; // 只控制它是否在接触时对敌人造成伤害
			Projectile.minion = true; // 宣布这是一个召唤物(有很多效果)
			Projectile.DamageType = DamageClass.Summon; // 声明伤害类型(造成伤害所需的)
			Projectile.minionSlots = 1f; // 这个仆从从玩家可用的仆从槽总数中占有的槽数(稍后详细介绍)
			Projectile.penetrate = -1; // 这样仆从就不会在与敌人或贴图碰撞时掉落
		}
		// 在这里你可以决定你的仆从是否打碎了像草或花盆之类的东西
		public override bool? CanCutTiles()
		{
			return false;
		}

		// 这是强制性的，如果你的仆从造成接触伤害(进一步相关的移动区域在AI())
		public override bool MinionContactDamage()
		{
			return true;
		}

		
		public override void AI()
		{
			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner))
			{
				return;
			}

			GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
			Visuals();
		}

		// 这是一种“主动检查”，确保玩家活着时随从还活着，否则就会被干掉
		private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active)
			{
				owner.ClearBuff(ModContent.BuffType<ChargedUAV>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<ChargedUAV>()))
			{
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
		{
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			// 如果你的仆从在空闲时没有漫无目的地移动，你需要将其“放入”其他被召唤的仆从的队列中
			// 索引是projectile.minionPos
			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// 这一行下面的所有代码都改编自 Spazmamini code (ID 388, aiStyle 66)

			// 如果距离太大，传送给玩家
			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
			{
				// 无论何时，当你处理会彻底改变行为或位置的非规则事件时，确保只在弹丸的所有者上运行代码，
				// 然后将netuupdate设置为true
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			// 如果你的仆从正在飞行，你想要独立于任何条件进行飞行
			float overlapVelocity = 0.04f;

			// 修复了与其他随从的重叠
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
				{
					if (Projectile.position.X < other.position.X)
					{
						Projectile.velocity.X -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.X += overlapVelocity;
					}

					if (Projectile.position.Y < other.position.Y)
					{
						Projectile.velocity.Y -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.Y += overlapVelocity;
					}
				}
			}
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// 开始搜索距离
			distanceFromTarget = 700f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// 如果你的仆从武器有目标功能，这段代码是必需的
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

				// 合理的距离，这样它就不会瞄准多个屏幕
				if (between < 2000f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}

			if (!foundTarget)
			{
				// 无论哪种方式，这段代码都是必需的，用于寻找目标
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					NPC npc = Main.npc[i];

					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
						//额外检查这一特定的仆从行为，否则它将停止攻击，一旦它冲过一个敌人，并飞过瓷砖之后
						// 这个数字取决于下面移动代码中看到的各种参数。测试不同的版本，直到它能正常工作
						bool closeThroughWall = between < 100f;

						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			//友好设置为true，这样奴才可以处理接触伤害
			//友好设置为false，这样它不会伤害的东西，如目标假人
			//这两个都取决于它是否有一个目标，所以它只是一个赋值
			//如果你的小兵是射击而不是造成接触伤害，你就不需要这个任务
			Projectile.friendly = foundTarget;
		}

		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
			// 默认移动参数(这里用于攻击)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget)
			{
				// 小黄人有一个目标:攻击(这里是飞向敌人)
				if (distanceFromTarget > 40f)
				{

					// 目标周围的直接距离(所以当接近目标时它不会锁定目标)
					Vector2 direction = targetCenter - Projectile.Center;
					direction.Normalize();
					direction *= speed;

					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}

			}
			else
			{
				// 小黄人没有目标:返回玩家和空闲
				if (distanceToIdlePosition > 600f)
				{
					// 如果仆从远离玩家，加速它
					speed = 12f;
					inertia = 60f;
				}
				else
				{
					// 如果仆从离玩家更近，放慢它的速度
					speed = 4f;
					inertia = 80f;
				}

				if (distanceToIdlePosition > 20f)
				{
					// 玩家周围的直接范围(当它被动地漂浮时)

					// 这是一个简单的移动公式，使用两个参数及其所需的方向来创建一个“归航”运动
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero)
				{
					// 如果有一种情况，它完全不移动，给它一点“戳”
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
		}

		private void Visuals()
		{
			Projectile.spriteDirection = Projectile.direction;
			// 所以它会稍微向它移动的方向倾斜
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			// 这是一个简单的“从上到下循环所有帧”的动画
			int frameSpeed = 5;

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

			// 一些视觉效果
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);
		}

	}
}
