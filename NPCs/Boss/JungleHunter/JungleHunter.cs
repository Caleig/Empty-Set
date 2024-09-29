using Microsoft.Xna.Framework;
using EmptySet.Common.Abstract.NPCs;
using EmptySet.Common.Systems;
using EmptySet.Items.Accessories;
using EmptySet.Items.Consumables;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.Projectiles.Boss.JungleHunter;
using EmptySet.Utils;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.NPCs.Boss.LavaHunter;

namespace EmptySet.NPCs.Boss.JungleHunter
{
    [AutoloadBossHead]
    public class JungleHunterHead : WormHead
    {
        private bool spawned = false;

        public static int secondStageHeadSlot = -1;

        public int witherDamage;//尖刺伤害
        public int LeavesBallDamage;//叶球伤害
        public Vector2[] witherPosition = { new(-20, -20), new(0, -20), new(20, -20), new(20, 0), new(20, 20), new(0, 20), new(-20, 20), new(-20, 0), };
        private Vector2 leavesBallVelocity;
        private Vector2 witherVelocity;
        private Vector2 witherPos;
        private Vector2 poisonousGogVelocity;

        //public override int MaxDistanceForUsingTileCollision => base.MaxDistanceForUsingTileCollision;

        public override string Texture => "EmptySet/NPCs/Boss/JungleHunter/JungleHunterHead";

        public override int BodyType => ModContent.NPCType<JungleHunterBody>();

        public override int TailType => ModContent.NPCType<JungleHunterTail>();

        public override void Load()
        {
            secondStageHeadSlot = Mod.AddBossHeadTexture(BossHeadTexture, -1);
        }
        public override void Initialize()
        {
            RegisterState(new MoveState1());
            RegisterState(new MoveState2());
            RegisterState(new AttackState1());
            RegisterState(new AttackState2());
            RegisterState(new AttackState3());
        }

        protected override void AIBefore(NPCStateMachine n)
        {
            float PercentageOfLife = (float)NPC.life / (float)NPC.lifeMax;
            if (!spawned) spawn();
            if (PercentageOfLife < 0.5)
            {
                n.allowNPCStates1AI = true;
                n.SetState<AttackState3>();
            }

            if (PercentageOfLife < 0.3)
            {
                n.SetState<MoveState2>();
            }

            if (PercentageOfLife < 0.4)
            {
                n.SetState<AttackState2>();
                n.allowNPCAttackStatesAI = true;
            }
            else if (PercentageOfLife < 0.9)
            {
                n.SetState<AttackState1>();
                n.allowNPCAttackStatesAI = true;
            }
        }

        public override void Init()
        {
            //设置段数
            MinSegmentLength = 30;
            MaxSegmentLength = 30;
            //设置速度和加速度
            MoveSpeed = 22f;
            Acceleration = 0.2f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("丛林游猎者");
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "EmptySet/NPCs/Boss/JungleHunter/JungleHunter_Bestiary",
                PortraitScale = 0.8f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        }

        public override void SetDefaults()
        {
            InitializeProperties();
            NPC.width = 94;
            NPC.height = 110;

            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.value = Item.buyPrice(0, Main.rand.Next(2, 5));

            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.behindTiles = true;
            NPC.trapImmune = true;

            NPC.dontCountMe = true;
            NPC.netAlways = true;

            if (!Main.dedServ) Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TheForgotten"); 
        }

        public void InitializeProperties()
        {
            NPC.lifeMax = EmptySetUtils.ScaledNPCMaxLife(Main.masterMode ? 18000 : Main.expertMode ? 12000 : 9000);
            NPC.defense = Main.masterMode ? 7 : Main.expertMode ? 5 : 5;
            NPC.knockBackResist = Main.masterMode ? 0f : Main.expertMode ? 0f : 0f;
            NPC.damage = EmptySetUtils.ScaledNPCDamage(Main.masterMode ? 160 : Main.expertMode ? 120 : 80);
            witherDamage = EmptySetUtils.ScaledProjDamage(Main.masterMode ? 250 : Main.expertMode ? 75 : 50);
            LeavesBallDamage = EmptySetUtils.ScaledProjDamage(Main.masterMode ? 300 : Main.expertMode ? 90 : 60);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
            new FlavorTextBestiaryInfoElement("生活在枯萎丛林的巨大生物，曾经是生活在丛林的众多生物之一，然而受到那场灾难的影响，其族群与事件发生的地周围的丛林被一同转化，变成了现在这幅模样.")
        });
        }

        public override void BossHeadSlot(ref int index)
        {
            if (secondStageHeadSlot != -1)
            {
                index = secondStageHeadSlot;
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.velocity.ToRotation() + (float)Math.PI * 90 / 180;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<JungleHunterChest>()));
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, 
                ModContent.ItemType<ForestNecklace>(), 
                ModContent.ItemType<DesertedTomahawk>()));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FangsNecklace>(), 3));

            npcLoot.Add(notExpertRule);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            // 在这里你想要改变当boss被打败时掉落的药剂类型。因为这个boss是早期的，所以我们保持它不变
            // 如果你想改变它，只需写上“potionType = ItemID.HealingPotion;”或任何其他药剂类型
        }

        public void poisonousGog(Player target)
        {
            poisonousGogVelocity = new Vector2(-NPC.velocity.Y, NPC.velocity.X).SafeNormalize(Vector2.UnitX);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, poisonousGogVelocity * 3, ModContent.ProjectileType<PoisonousGogProjectile>(), 5, 0, target.whoAmI);
            poisonousGogVelocity = new Vector2(NPC.velocity.Y, -NPC.velocity.X).SafeNormalize(Vector2.UnitX);
            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, poisonousGogVelocity * 3, ModContent.ProjectileType<PoisonousGogProjectile>(), 5, 0, target.whoAmI);
        }

        public void Wither(Player target)
        {
            int r = Main.rand.Next(1, 60);
            for (int i = 0; i < witherPosition.Length; i++)
            {
                witherPos = target.Center + witherPosition[i].RotatedBy(MathHelper.ToRadians(r)) * 20;
                witherVelocity = (target.Center - witherPos).SafeNormalize(Vector2.UnitX) * 5;
                int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), witherPos, witherVelocity, ModContent.ProjectileType<WitherProjectile>(), EmptySetUtils.ScaledProjDamage(witherDamage), 0, target.whoAmI);
                Main.projectile[projectile].timeLeft = 600;
            }
        }

        public void LeavesBall(Player target)
        {
            leavesBallVelocity = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
            int projectile = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, leavesBallVelocity * 4, ModContent.ProjectileType<LeavesBallProjectile>(), EmptySetUtils.ScaledProjDamage(LeavesBallDamage), 0, target.whoAmI);
            Main.projectile[projectile].timeLeft = 600;
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedJungleHunter, -1);
        }
        private void spawn()
        {
            spawned = true;
            NPC.TargetClosest(false);
            Player player;
            player = Main.player[NPC.target];
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                NPC.oldPos[i] = NPC.position;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int prev = NPC.whoAmI;
                for (int i = 0; i < MaxSegmentLength; i++)
                {
                    int type = i == MaxSegmentLength - 1 ? ModContent.NPCType<LavaHunterTail>() : ModContent.NPCType<LavaHunterBody>();
                    int n = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.whoAmI);
                    if (n != Main.maxNPCs)
                    {
                        Main.npc[n].ai[1] = prev;
                        Main.npc[n].ai[3] = NPC.whoAmI;
                        Main.npc[n].realLife = NPC.whoAmI;

                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);

                        prev = n;
                    }
                    else
                    {
                        NPC.active = false;
                        if (Main.netMode == NetmodeID.Server)
                            NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
                        return;
                    }
                }
            }
        }
    }
    internal class JungleHunterBody : WormBody
    {
        public override string Texture => "EmptySet/NPCs/Boss/JungleHunter/JungleHunterBody";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("丛林游猎者");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        }

        public override void SetDefaults()
        {
            InitializeProperties();
            NPC.width = 42;//60
            NPC.height = 58;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.value = Item.buyPrice(0, Main.rand.Next(2, 5));

            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.behindTiles = true;
            NPC.trapImmune = true;

            NPC.dontCountMe = true;
            NPC.netAlways = true;
        }

        public void InitializeProperties()
        {
            NPC.lifeMax = EmptySetUtils.ScaledNPCMaxLife(Main.masterMode ? 18000 : Main.expertMode ? 12000 : 9000);
            NPC.defense = Main.masterMode ? 10 : Main.expertMode ? 8 : 8;
            NPC.damage = EmptySetUtils.ScaledNPCDamage(Main.masterMode ? 60 : Main.expertMode ? 45 : 30);
        }

        public override void Init() { }

        public override void Initialize() { }
    }

    internal class JungleHunterTail : WormTail
    {
        public override string Texture => "EmptySet/NPCs/Boss/JungleHunter/JungleHunterTail";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("丛林游猎者");
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
        NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
        }

        public override void SetDefaults()
        {
            InitializeProperties();
            NPC.width = 50;//35
            NPC.height = 58;
      
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.value = Item.buyPrice(0, Main.rand.Next(2, 5));

            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.behindTiles = true;
            NPC.trapImmune = true;
            NPC.dontCountMe = true;
            NPC.netAlways = true;
        }

        public void InitializeProperties()
        {
            NPC.lifeMax = EmptySetUtils.ScaledNPCMaxLife(Main.masterMode ? 18000 : Main.expertMode ? 12000 : 9000);
            NPC.defense = Main.masterMode ? 12 : Main.expertMode ? 9 : 9;
            NPC.damage = EmptySetUtils.ScaledNPCDamage(Main.masterMode ? 100 : Main.expertMode ? 75 : 50);
        }

        public override void Init() { }

        public override void Initialize() { }
    }

    #region 整体和头部ai,注册在JungleHunterHead上
    /// <summary>
    /// 移动状态1 血量100%~31%时
    /// </summary>
    class MoveState1 : NPCState
    {
        JungleHunterHead jungleHunterHead;
        bool onfloor = false;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            jungleHunterHead = (JungleHunterHead)NPC.ModNPC;

            int tile_x = (int)(Main.player[NPC.target].Center.X / 16f);
            int tile_y = (int)(Main.player[NPC.target].Center.Y / 16f);
            for (int i = tile_x - 2; i <= tile_x + 2; i++)
            {
                for (int j = tile_y; j <= tile_y + 3; j++)
                {
                    if (WorldGen.SolidTile2(i, j))
                    {
                        onfloor = true;
                        break;
                    }
                    else
                    {
                        onfloor = false;
                    }
                }
                if(onfloor) break;
            }
            if (NPC.HasValidTarget && !onfloor)
            {
                jungleHunterHead.CanFly = true;
            }
            else
            {
                jungleHunterHead.CanFly = false;
            }
        }
    }
    /// <summary>
    /// 移动状态2 血量30%时
    /// </summary>
    class MoveState2 : NPCState
    {
        JungleHunterHead jungleHunterHead;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            jungleHunterHead = (JungleHunterHead)NPC.ModNPC;
            jungleHunterHead.CanFly = true;
            jungleHunterHead.MoveSpeed = 25f;
            jungleHunterHead.Acceleration = 0.2f;
        }
    }
    /// <summary>
    /// 从头部向玩家方向发射一个缓慢飞行的叶球
    /// </summary>
    class AttackState1 : NPCAttackState
    {
        JungleHunterHead jungleHunterHead;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            jungleHunterHead = (JungleHunterHead)NPC.ModNPC;
            n.AttackTimer++;
            if (n.AttackTimer >= 3 * 60)
            {
                jungleHunterHead.LeavesBall(Main.player[NPC.target]);
                n.AttackTimer = 0;
            }
        }
    }
    /// <summary>
    /// 8个尖刺
    /// </summary>
    class AttackState2 : NPCAttackState
    {
        JungleHunterHead jungleHunterHead;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            jungleHunterHead = (JungleHunterHead)NPC.ModNPC;
            n.AttackTimer++;
            if (n.AttackTimer >= 5 * 60)
            {
                jungleHunterHead.Wither(Main.player[NPC.target]);
                n.AttackTimer = 0;
            }
        }
    }
    /// <summary>
    /// 毒雾
    /// </summary>
    class AttackState3 : NPCState1
    {
        JungleHunterHead jungleHunterHead;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            jungleHunterHead = (JungleHunterHead)NPC.ModNPC;
            n.Timer1++;
            if (n.Timer1 <= 3 * 60)
            {
                n.Timer2++;
                if (n.Timer2 >= 10)
                {
                    jungleHunterHead.poisonousGog(Main.player[NPC.target]);
                    n.Timer2 = 0;
                }
            }
            else if (n.Timer1 >= 8 * 60) 
            {
                n.Timer1 = 0;
            }
        }
    }
    #endregion
}
//init();
//判断体节，执行头部或身体ai

//头部
//1、生成体节，执行在非多人客户端，判断ai[0]，后边是否有下一段，没有的时候执行生成
//HeadAI_SpawnSegments();
//2、检测碰撞，并产生灰尘
//HeadAI_CheckCollisionForDustSpawns():
//3、检测与目标的距离，腾空飞行一段时间？
//HeadAI_CheckTargetDistance();
//4、头部移动
//HeadAI_Movement（）；
//4.1、不会飞  并且不在腾空冲向目标  并且没有在方块上
//我们希望NPC沿着X轴下落并减速
//HeadAI_Movement_HandleFallingFromNoCollision（）；
//4.2、否则我们想要播放一些音频(soundDelay)并向目标移动。
//HeadAI_Movement_PlayDigSounds
//HeadAI_Movement_HandleMovement
//4.3、头部移动中最后执行，为这个NPC设置正确的旋转。
//HeadAI_Movement_SetRotation
//5、检测玩家存活。更改速度，消失时间

//身体
//1、通用ai，使当前段跟随上一段移动
//CommonAI_BodyTail

//尾巴
//CommonAI_BodyTail
