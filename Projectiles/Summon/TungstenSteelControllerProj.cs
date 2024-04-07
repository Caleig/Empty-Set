using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Buffs;
using EmptySet.Common.Abstract.Projectiles;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace EmptySet.Projectiles.Summon
{
    internal class TungstenSteelControllerProj : ProjectileStateMachine
	{
		public Vector2 vectorToIdlePosition;
		public float distanceToIdlePosition;
		public override void Initialize()
		{
			RegisterState(new Normal());
			RegisterState(new Attack());
			RegisterState(new AttackState1());
			RegisterState(new AttackState2());
		}
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("钨钢无人机");
			// 设置这个召唤物在它的精灵表上的帧数
			//Main.projFrames[Projectile.type] = 4;
			// 这对于右击目标是必要的
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // 表示此射弹是宠物或仆从
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // 这是必要的，这样你的仆从可以正确地生成时，当其他仆从被召唤时替换
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // 让信徒抵抗这一射弹，因为它对所有归航射弹都是抵抗的。

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // 记录模式

		}

		public sealed override void SetDefaults()
		{
			Projectile.width = 38;
			Projectile.height = 34;
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

        public override void AIBefore(ProjectileStateMachine ProjectileStateMachine)
        {
			CheckActive(Main.player[Projectile.owner]);
			GeneralBehavior(ProjectileStateMachine.owner, out vectorToIdlePosition, out distanceToIdlePosition);
			ProjectileStateMachine.target = SearchForTargets(ProjectileStateMachine, ProjectileStateMachine.owner, 1000f);
			if(Main.rand.NextBool(2)) Main.dust[Dust.NewDust(Projectile.position, 34, 34, DustID.SilverFlame)].noGravity = true;
		}

        public override void AIAfter(ProjectileStateMachine ProjectileStateMachine)
        {
			Visuals();
		}
		/// <summary>
		/// 这是一种“主动检查”，确保玩家活着时随从还活着，否则就会被干掉
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active)
			{
				owner.ClearBuff(ModContent.BuffType<TungstenSteelControllerBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<TungstenSteelControllerBuff>()))
			{
				Projectile.timeLeft = 2;
			}

			return true;
		}
		/// <summary>
		/// 一般行为
		/// </summary>
		/// <param name="owner"></param>
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

		/// <summary>
		/// 寻敌
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="foundTarget"></param>
		/// <param name="target"></param>
		private NPC SearchForTargets(ProjectileStateMachine p, Player owner, float distanceFromTarget)
		{
			NPC target = null;
			if (p.foundTarget = true && p.target != null && p.target.active && Vector2.Distance(p.target.Center, Projectile.Center) <= distanceFromTarget && !owner.HasMinionAttackTargetNPC)
			{
				target = p.target;
			}
			else
			{
				Vector2 targetCenter = Vector2.Zero;
				// 如果你的仆从武器有目标功能，这段代码是必需的
				if (owner.HasMinionAttackTargetNPC)
				{
					target = Main.npc[owner.MinionAttackTargetNPC];
					float between = Vector2.Distance(target.Center, Projectile.Center);
					// 合理的距离，这样它就不会瞄准多个屏幕
					if (between < 2000f)
					{
						p.foundTarget = true;
					}
				}

				if (!p.foundTarget)
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

							if (closest && inRange)
							{
								targetCenter = npc.Center;
								target = npc;
							}
						}
					}
				}
			}
			return target;
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
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
		private void Visuals()
		{
			Projectile.spriteDirection = Projectile.direction;
			// 所以它会稍微向它移动的方向倾斜
			//Projectile.rotation = Projectile.velocity.X * 0.05f;

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
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.5f);
		}
    }
	/// <summary>
	/// 空闲状态，左右移动
	/// </summary>
    class Normal : ProjectileState1
    {
		float speed = 8f;
		float inertia = 20f;//惯性
        public override void AI(ProjectileStateMachine p)
        {
			Projectile = p.Projectile;
			
			Movement(p);
			//有敌人，切换状态
			if (p.target != null && p.target.active && p.foundTarget) 
			{
				p.SetState<Attack>();
				p.allowProjectileStates2AI = true;
			}
		}
		/// <summary>
		/// 移动
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
        private void Movement(ProjectileStateMachine p)
        {
			Projectile = p.Projectile;
			Projectile.rotation += 0.05f;
			float distanceToIdlePosition = ((TungstenSteelControllerProj)Projectile.ModProjectile).distanceToIdlePosition;
			Vector2 vectorToIdlePosition = ((TungstenSteelControllerProj)Projectile.ModProjectile).vectorToIdlePosition;
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
	/// <summary>
	/// 攻击状态
	/// </summary>
    class Attack : ProjectileState1
    {
		float distanceFromTarget;

		public override void AI(ProjectileStateMachine p)
        {
			Projectile = p.Projectile;
			if (p.target != null && p.target.active && p.foundTarget)
			{
				distanceFromTarget = Vector2.Distance(Projectile.Center, p.target.Center);
				Projectile.rotation += 0.1f;
				//距离大于100
				if (distanceFromTarget > 150)
				{
					p.Timer1 = 0;
					p.SetState<AttackState1>();
				}
				else 
				{
					p.SetState<AttackState2>();
				}
			}
			else 
			{
				p.foundTarget = false;
				p.SetState<Normal>();
				p.allowProjectileStates2AI = false;
			}
		}
	}
	/// <summary>
	/// 靠近敌人
	/// </summary>
    class AttackState1 : ProjectileState2
    {
		float speed = 8f;
		float inertia = 20f;//惯性
		Vector2 direction;
		public override void AI(ProjectileStateMachine p)
        {
			Projectile = p.Projectile;
			direction = (p.target.Center - Projectile.Center).SafeNormalize(Vector2.UnitX);
			direction *= speed;
			Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
		}
    }
	/// <summary>
	/// 攻击敌人
	/// </summary>
	class AttackState2 : ProjectileState2
	{
		Vector2 direction;
		float distanceFromTarget;
		float speed = 20f;
		public override void AI(ProjectileStateMachine p)
		{
			Projectile = p.Projectile;
			direction = p.target.Center - Projectile.Center;
			distanceFromTarget = Vector2.Distance(Projectile.Center, p.target.Center);
			p.Timer1++;
			if (p.Timer1 == 1)
			{
				Projectile.velocity = direction.SafeNormalize(Vector2.UnitX) * speed;
			}
			else if (p.Timer1 < 15)
			{
				Projectile.rotation += 0.5f;
				if (distanceFromTarget >= 100 && Vector2.Distance(Projectile.Center + Projectile.velocity, p.target.Center) > distanceFromTarget) p.Timer1 = 14;
				if (distanceFromTarget < 100) p.Timer1 = 13;
			}
			else if (p.Timer1 == 15)
			{
				Projectile.velocity *= 0.01f;
			}
			else if (p.Timer1 > 40)
			{
				p.Timer1 = 0;
			}
		}
	}
}
