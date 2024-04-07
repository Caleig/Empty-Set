using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;

namespace EmptySet.Common.Abstract.NPCs
{
	public enum WormSegmentType
	{
		/// <summary>
		/// 蠕虫的头部，任何蠕虫只有一个头
		/// </summary>
		Head,
		/// <summary>
		/// 身体部分
		/// </summary>
		Body,
		/// <summary>
		/// 尾部分，拥有与身体部分相同的AI，任何蠕虫只有一个尾部
		/// </summary>
		Tail
	}

	/// <summary>
	/// 不分节蠕虫基类
	/// </summary>
	public abstract class Worm : NPCStateMachine
	{
		/// <summary>
		/// 这节NPC类型
		/// </summary>
		public abstract WormSegmentType SegmentType { get; }

		/// <summary>
		/// NPC的最大速度
		/// </summary>
		public float MoveSpeed { get; set; }

		/// <summary>
		/// NPC的加速度
		/// </summary>
		public float Acceleration { get; set; }

		/// <summary>
		/// 蠕虫头部
		/// </summary>
		public NPC HeadSegment => Main.npc[NPC.realLife];
		/// <summary>
		/// 这一节跟随的上一节NPC的索引
		/// </summary>
		public int PreviousNPCId { get; set; }
		/// <summary>
		/// 这一节跟随的上一节NPC，对于头部来说，这个总是返回<see langword="null"/>
		/// </summary>
		public NPC PreviousNPC => SegmentType == WormSegmentType.Head ? null : Main.npc[PreviousNPCId];

		/// <summary>
		/// 跟随这一节的后面一节NPC的索引
		/// </summary>
		public int NextNPCId { get; set; }
		/// <summary>
		/// 跟随这一节的后面一节NPC，对于尾部来说，这个总是返回<see langword="null"/>
		/// </summary>
		public NPC NextNPC => SegmentType == WormSegmentType.Tail ? null : Main.npc[NextNPCId];

		/// <summary>
		/// 当同步更改碰撞检测时使用
		/// </summary>
		public bool IsCollision { get; set; }

		/// <summary>
		/// 检查Init()是否被调用
		/// </summary>
		public bool IsInit { get; set; }

		/// <summary>
		/// 如果体节类型是头，绘制boss血条
		/// </summary>
		/// <param name="hbPosition"></param>
		/// <param name="scale"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return SegmentType == WormSegmentType.Head ? null : false;
		}
		/// <summary>
		/// 是否开始消失
		/// </summary>
		private bool startDespawning;

		public sealed override bool PreAI()
		{
			if (!IsInit)
			{
				IsInit = true;
				Init();
			}

			if (SegmentType == WormSegmentType.Head)
			{
				HeadAI();

				if (!NPC.HasValidTarget)
				{
					NPC.TargetClosest(true);

					// 如果NPC是一个没有目标的boss，那就迫使他迅速掉入地下世界
					if (!NPC.HasValidTarget && NPC.boss)
					{
						NPC.velocity.Y += 8f;

						MoveSpeed = 1000f;

						if (!startDespawning)
						{
							startDespawning = true;

							// 如果NPC足够远，1.5秒后消失
							NPC.timeLeft = 90;
						}
					}
				}
			}
			else
				BodyTailAI();
			return true;
		}

		internal virtual void HeadAI() { }

		internal virtual void BodyTailAI() { }

		public abstract void Init();
	}

	/// <summary>
	/// 蠕虫头部npc的基类
	/// </summary>
	public abstract class WormHead : Worm
	{
		public sealed override WormSegmentType SegmentType => WormSegmentType.Head;

		/// <summary>
		/// 蠕虫Body段对应的NPC类型<br/>
		/// 这个属性只在 <see cref="HasCustomBodySegments"/> 返回 <see langword="false"/> 时使用.
		/// </summary>
		public abstract int BodyType { get; }

		/// <summary>
		/// 蠕虫Tail段对应的NPC类型<br/>
		/// 这个属性只在 <see cref="HasCustomBodySegments"/> 返回 <see langword="false"/> 时使用.
		/// </summary>
		public abstract int TailType { get; }

		/// <summary>
		/// 最小段数，包括头和尾
		/// </summary>
		public int MinSegmentLength { get; set; }

		/// <summary>
		/// 最大段数，包括头和尾
		/// </summary>
		public int MaxSegmentLength { get; set; }

		/// <summary>
		/// NPC是否可以飞行,不依赖方块，就像飞龙一样。
		/// </summary>
		public bool CanFly { get; set; }

		/// <summary>
		/// 在 <see cref="CanFly"/> 返回 <see langword="false"/> 的情况下，NPC可以离开实体方块的最大距离<br/>
		/// 默认为1000像素，相当于62.5个方块。
		/// </summary>
		public virtual int MaxDistanceForUsingTileCollision => 1000;

		/// <summary>
		/// 是否自定义蠕虫的身体部分
		/// </summary>
		public virtual bool HasCustomBodySegments => false;

		/// <summary>
		/// 如果不是 <see langword="null"/>, NPC将针对给定的世界位置而不是它的玩家目标
		/// </summary>
		public Vector2? ForcedTargetPosition { get; set; }

		/// <summary>
		/// 重写此方法以使用自定义<b>Body</b>生成代码<br/>
		/// 这个方法仅在 <see cref="HasCustomBodySegments"/> 返回 <see langword="true"/> 时调用.
		/// </summary>
		/// <param name="segmentCount">身节段数</param>
		/// <returns>生成的NPC的whoAmI,就是调用 <see cref="NPC.NewNPC(Terraria.DataStructures.IEntitySource, int, int, int, int, float, float, float, float, int)"/> 所返回的结果</returns>
		public virtual int SpawnBodySegments(int segmentCount)
		{
			//默认只返回这个NPC的whoAmI
			return NPC.whoAmI;
		}

		/// <summary>
		/// 生成蠕虫的身体和尾部
		/// </summary>
		/// <param name="source">生成源</param>
		/// <param name="type">要生成的体节类型</param>
		/// <param name="latestNPC">上一节的NPC索引</param>
		/// <returns></returns>
		protected int SpawnSegment(IEntitySource source, int type, int latestNPC)
		{
			int oldLatest = latestNPC;
			latestNPC = NPC.NewNPC(source, (int)NPC.Center.X, (int)NPC.Center.Y, type);

			((Worm)Main.npc[oldLatest].ModNPC).NextNPCId = latestNPC;

			NPC latest = Main.npc[latestNPC];

			((Worm)latest.ModNPC).PreviousNPCId = oldLatest;

			latest.realLife = NPC.whoAmI;

			return latestNPC;
		}

		internal sealed override void HeadAI()
		{
			HeadAI_SpawnSegments();

			bool collision = HeadAI_CheckCollisionForDustSpawns();

			HeadAI_CheckTargetDistance(ref collision);

			HeadAI_Movement(collision);
		}

		private void HeadAI_SpawnSegments()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				//NPC是否有下一段，没有的话则是第一次调用，生成身体和尾部
				if (!(NextNPCId > 0))
				{
					NPC.realLife = NPC.whoAmI;
					// latestNPC 将用于 SpawnSegment()
					int latestNPC = NPC.whoAmI;

					// 确定蠕虫的长度
					int randomWormLength = Main.rand.Next(MinSegmentLength, MaxSegmentLength + 1);

					int distance = randomWormLength - 2;

					IEntitySource source = NPC.GetSource_FromAI();

					//如果自定义生成
					if (HasCustomBodySegments)
					{
						// 调用处理 Body 生成的方法
						latestNPC = SpawnBodySegments(distance);
					}
					else
					{
						// 正常的生成 Body 部分
						while (distance > 0)
						{
							latestNPC = SpawnSegment(source, BodyType, latestNPC);
							distance--;
						}
					}

					// 生成尾部
					SpawnSegment(source, TailType, latestNPC);

					NPC.netUpdate = true;

					// 确保所有的体节都可以生成。如果没有全部生成，就彻底消灭蠕虫
					int count = 0;
					for (int i = 0; i < Main.maxNPCs; i++)
					{
						NPC n = Main.npc[i];

						if (n.active && (n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
							count++;
					}

					if (count != randomWormLength)
					{
						// 无法生成所有的片段…杀死蠕虫其他体节
						for (int i = 0; i < Main.maxNPCs; i++)
						{
							NPC n = Main.npc[i];

							if (n.active && (n.type == Type || n.type == BodyType || n.type == TailType) && n.realLife == NPC.whoAmI)
							{
								n.active = false;
								n.netUpdate = true;
							}
						}
					}

					NPC.TargetClosest(true);
				}
			}
		}

		private bool HeadAI_CheckCollisionForDustSpawns()
		{
			int minTilePosX = (int)(NPC.Left.X / 16) - 1;
			int maxTilePosX = (int)(NPC.Right.X / 16) + 2;
			int minTilePosY = (int)(NPC.Top.Y / 16) - 1;
			int maxTilePosY = (int)(NPC.Bottom.Y / 16) + 2;

			// 确保瓦片范围在世界范围内
			if (minTilePosX < 0)
				minTilePosX = 0;
			if (maxTilePosX > Main.maxTilesX)
				maxTilePosX = Main.maxTilesX;
			if (minTilePosY < 0)
				minTilePosY = 0;
			if (maxTilePosY > Main.maxTilesY)
				maxTilePosY = Main.maxTilesY;

			bool collision = false;

			// 这是碰撞瓷砖的初始检查。
			for (int i = minTilePosX; i < maxTilePosX; ++i)
			{
				for (int j = minTilePosY; j < maxTilePosY; ++j)
				{
					Tile tile = Main.tile[i, j];

					// 如果是实体方块或平台，那么就存在有效的碰撞
					if (tile.HasUnactuatedTile && (Main.tileSolid[tile.TileType] || Main.tileSolidTop[tile.TileType] && tile.TileFrameY == 0) || tile.LiquidAmount > 64)
					{
						Vector2 tileWorld = new Point16(i, j).ToWorldCoordinates(0, 0);

						if (NPC.Right.X > tileWorld.X && NPC.Left.X < tileWorld.X + 16 && NPC.Bottom.Y > tileWorld.Y && NPC.Top.Y < tileWorld.Y + 16)
						{
							// 碰撞了
							collision = true;
							//产生灰尘
							if (Main.rand.NextBool(100))
								WorldGen.KillTile(i, j, fail: true, effectOnly: true, noItem: false);
						}
					}
				}
			}

			return collision;
		}

		private void HeadAI_CheckTargetDistance(ref bool collision)
		{
			// 如果没有碰撞，检查这个NPC和它的目标之间的距离是否太大
			if (!collision)
			{
				Rectangle hitbox = NPC.Hitbox;

				int maxDistance = MaxDistanceForUsingTileCollision;

				bool tooFar = true;

				for (int i = 0; i < Main.maxPlayers; i++)
				{
					Rectangle areaCheck;

					Player player = Main.player[i];

					if (ForcedTargetPosition is Vector2 target)
						areaCheck = new Rectangle((int)target.X - maxDistance, (int)target.Y - maxDistance, maxDistance * 2, maxDistance * 2);
					else if (player.active && !player.dead && !player.ghost) 
					{
						areaCheck = new Rectangle((int)player.position.X - maxDistance, (int)player.position.Y - maxDistance, maxDistance * 2, maxDistance * 2);

						Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
						Main.EntitySpriteDraw(EmptySet.FrozenCoreDust, new Vector2((int)player.position.X - maxDistance, (int)player.position.Y - maxDistance)-Main.screenPosition, new Rectangle(0, 0, maxDistance * 2, maxDistance * 2), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
						Main.spriteBatch.End();
					}

					else
						continue;  // 不是一个有效的玩家

					if (hitbox.Intersects(areaCheck))
					{
						tooFar = false;
						break;
					}
				}

				if (tooFar)
					collision = true;
			}
		}

		private void HeadAI_Movement(bool collision)
		{
			// 移动速度
			float speed = MoveSpeed;
			// 加速度
			float acceleration = Acceleration;

			float targetXPos, targetYPos;

			Player playerTarget = Main.player[NPC.target];

			Vector2 forcedTarget = ForcedTargetPosition ?? playerTarget.Center;
			// 同时赋值
			(targetXPos, targetYPos) = (forcedTarget.X, forcedTarget.Y);

			Vector2 npcCenter = NPC.Center;

			float targetRoundedPosX = (float)((int)(targetXPos / 16f) * 16);
			float targetRoundedPosY = (float)((int)(targetYPos / 16f) * 16);
			npcCenter.X = (float)((int)(npcCenter.X / 16f) * 16);
			npcCenter.Y = (float)((int)(npcCenter.Y / 16f) * 16);
			float dirX = targetRoundedPosX - npcCenter.X;
			float dirY = targetRoundedPosY - npcCenter.Y;

			float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

			// 如果没有和实体方块碰撞，并且NPC不会飞，让NPC沿着X轴下落并减速。
			if (!collision && !CanFly)
				HeadAI_Movement_HandleFallingFromNoCollision(dirX, speed, acceleration);
			else
			{
				// 否则播放挖掘音效(soundDelay)并向目标移动。
				HeadAI_Movement_PlayDigSounds(length);

				HeadAI_Movement_HandleMovement(dirX, dirY, length, speed, acceleration);
			}

			HeadAI_Movement_SetRotation(collision);
		}

		private void HeadAI_Movement_HandleFallingFromNoCollision(float dirX, float speed, float acceleration)
		{
			NPC.TargetClosest(true);

			NPC.velocity.Y += 0.11f;

			// 限制NPC下落速度
			if (NPC.velocity.Y > speed)
				NPC.velocity.Y = speed;

			// 下面的行为模仿原版蠕虫的运动
			if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4f)
			{
				// 速度足够快，但也不能太快
				if (NPC.velocity.X < 0.0f)
					NPC.velocity.X -= acceleration * 1.1f;
				else
					NPC.velocity.X += acceleration * 1.1f;
			}
			else if (NPC.velocity.Y == speed)
			{
				// 已达到最快速度
				if (NPC.velocity.X < dirX)
					NPC.velocity.X += acceleration;
				else if (NPC.velocity.X > dirX)
					NPC.velocity.X -= acceleration;
			}
			else if (NPC.velocity.Y > 4)
			{
				if (NPC.velocity.X < 0)
					NPC.velocity.X += acceleration * 0.9f;
				else
					NPC.velocity.X -= acceleration * 0.9f;
			}
		}

		private void HeadAI_Movement_PlayDigSounds(float length)
		{
			if (NPC.soundDelay == 0)
			{
				// NPC离目标位置越近，播放声音的速度越快
				float num1 = length / 40f;

				if (num1 < 10)
					num1 = 10f;

				if (num1 > 20)
					num1 = 20f;

				NPC.soundDelay = (int)num1;

				SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
			}
		}

		private void HeadAI_Movement_HandleMovement(float dirX, float dirY, float length, float speed, float acceleration)
		{
			float absDirX = Math.Abs(dirX);
			float absDirY = Math.Abs(dirY);
			float newSpeed = speed / length;
			dirX *= newSpeed;
			dirY *= newSpeed;

			if ((NPC.velocity.X > 0 && dirX > 0) || (NPC.velocity.X < 0 && dirX < 0) || (NPC.velocity.Y > 0 && dirY > 0) || (NPC.velocity.Y < 0 && dirY < 0))
			{
				// NPC正在向目标位置移动
				if (NPC.velocity.X < dirX)
					NPC.velocity.X += acceleration;
				else if (NPC.velocity.X > dirX)
					NPC.velocity.X -= acceleration;

				if (NPC.velocity.Y < dirY)
					NPC.velocity.Y += acceleration;
				else if (NPC.velocity.Y > dirY)
					NPC.velocity.Y -= acceleration;

				// 
				if (Math.Abs(dirY) < speed * 0.2 && ((NPC.velocity.X > 0 && dirX < 0) || (NPC.velocity.X < 0 && dirX > 0)))
				{
					if (NPC.velocity.Y > 0)
						NPC.velocity.Y += acceleration * 2f;
					else
						NPC.velocity.Y -= acceleration * 2f;
				}

				// 
				if (Math.Abs(dirX) < speed * 0.2 && ((NPC.velocity.Y > 0 && dirY < 0) || (NPC.velocity.Y < 0 && dirY > 0)))
				{
					if (NPC.velocity.X > 0)
						NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
					else
						NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
				}
			}
			else if (absDirX > absDirY)
			{
				// X距离比Y距离大，迫使沿着x轴的运动更强
				if (NPC.velocity.X < dirX)
					NPC.velocity.X += acceleration * 1.1f;
				else if (NPC.velocity.X > dirX)
					NPC.velocity.X -= acceleration * 1.1f;

				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
				{
					if (NPC.velocity.Y > 0)
						NPC.velocity.Y += acceleration;
					else
						NPC.velocity.Y -= acceleration;
				}
			}
			else
			{
				// Y距离比X距离大，迫使沿着y轴的运动更强
				if (NPC.velocity.Y < dirY)
					NPC.velocity.Y += acceleration * 1.1f;
				else if (NPC.velocity.Y > dirY)
					NPC.velocity.Y -= acceleration * 1.1f;

				if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
				{
					if (NPC.velocity.X > 0)
						NPC.velocity.X += acceleration;
					else
						NPC.velocity.X -= acceleration;
				}
			}
		}

		private void HeadAI_Movement_SetRotation(bool collision)
		{
			// 为这个NPC设置正确的旋转
			NPC.rotation = NPC.velocity.ToRotation() + MathHelper.PiOver2;

			//一些更新的东西(多人游戏兼容性)
			if (collision)
			{
				if (!IsCollision)
					NPC.netUpdate = true;

				IsCollision = true;
			}
			else
			{
				if (IsCollision)
					NPC.netUpdate = true;

				IsCollision = false;
			}

			// 如果NPC的速度发生变化，并且不是被玩家击中，那么强制更新
			if (((NPC.velocity.X > 0 && NPC.oldVelocity.X < 0) || (NPC.velocity.X < 0 && NPC.oldVelocity.X > 0) || (NPC.velocity.Y > 0 && NPC.oldVelocity.Y < 0) || (NPC.velocity.Y < 0 && NPC.oldVelocity.Y > 0)) && !NPC.justHit)
				NPC.netUpdate = true;
		}
	}

	public abstract class WormBody : Worm
	{
		public sealed override WormSegmentType SegmentType => WormSegmentType.Body;

		internal override void BodyTailAI()
		{
			CommonAI_BodyTail(this);
		}

		internal static void CommonAI_BodyTail(Worm worm)
		{
			if (!worm.NPC.HasValidTarget)
				worm.NPC.TargetClosest(true);

			if (Main.player[worm.NPC.target].dead && worm.NPC.timeLeft > 30000)
				worm.NPC.timeLeft = 10;

			NPC previous = worm.PreviousNPCId >= Main.maxNPCs ? null : worm.PreviousNPC;
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				// 如果身体/尾巴是单独生成的
				// 或者它跟随的NPC不再有效，则杀死这一段
				if (previous is null || !previous.active || previous.friendly || previous.townNPC || previous.lifeMax <= 5)
				{
					worm.NPC.life = 0;
					worm.NPC.HitEffect(0, 10);
					worm.NPC.active = false;
				}
			}

			if (previous is not null)
			{
				//使用当前的NPC.Center来计算这个NPC的“parent NPC”的方向。
				float dirX = previous.Center.X - worm.NPC.Center.X;
				float dirY = previous.Center.Y - worm.NPC.Center.Y;
				// 使用Atan2去获得朝向父NPC的正确旋转
				// 根据贴图方向修改npc旋转角度
				worm.NPC.rotation = (float)Math.Atan2(dirY, dirX) + MathHelper.PiOver2;
				// 方向向量的长度
				float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
				// 计算出正确的距离
				float dist = (length - worm.NPC.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;

				// 重置这个NPC的速度
				worm.NPC.velocity = Vector2.Zero;
				// 并将该NPC的位置设置为与该NPC的父NPC对应的位置
				worm.NPC.position.X += posX;
				worm.NPC.position.Y += posY;
			}
		}

        public override bool CheckActive()
        {
            return false;
        }
    }

	public abstract class WormTail : Worm
	{
		public sealed override WormSegmentType SegmentType => WormSegmentType.Tail;

		internal override void BodyTailAI()
		{
			WormBody.CommonAI_BodyTail(this);
		}
		public override bool CheckActive()
		{
			return false;
		}
	}
}
