using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Common.Abstract.NPCs;
using EmptySet.Common.Systems;
using EmptySet.Dusts;
using EmptySet.Projectiles.Boss.FrozenCore;
using EmptySet.Utils;
using System;
using EmptySet.Items.Consumables;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.NPCs.Boss.FrozenCore
{
    //1、继承NPCStateMachine类
    [AutoloadBossHead]
    internal class FrozenCore : NPCStateMachine
    {
        public bool canBeHit = false;
        public int frostBoltDamage = 100;
        public int icicleDamage = 100;
        //2、实现初始化方法
        public override void Initialize()
        {
            //3、注册状态机
            //注册npc状态
            RegisterState(new SpawnState());
            RegisterState(new NormalState());
            RegisterState(new AttackState());
            RegisterState(new RunState());
            RegisterState(new DeathState());
            //注册npc攻击状态
            RegisterState(new AttackState1());
            RegisterState(new AttackState2());
            RegisterState(new AttackState3());
            RegisterState(new AttackState4());
            //注册npc特殊状态
            RegisterState(new TeleportState());
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Frozen Core");
            
            NPCID.Sets.MPAllowedEnemies[Type] = true;//允许召唤物召唤
            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "EmptySet/NPCs/Boss/FrozenCore/FrozenCore",
                PortraitScale = 0.5f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
            //NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            //NPCID.Sets.TrailingMode[NPC.type] = 0;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("来自冰川能量精华的集合体。\n原本是生活在雪地的法师们为了凝聚能量而创造出来的工具，但在吸收了大量的能量后彻底失控，在干碎留守的法师们后便逃之夭夭")
            });
        }

        public override void SetDefaults()
        {
            DiffSelector();
            NPC.width = 114;
            NPC.height = 114;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(0, Main.rand.Next(4, 8));
            NPC.npcSlots = 10f;//占据npc槽位，阻止小怪刷出
            NPC.boss = true;
            NPC.trapImmune = true;//免疫陷阱
            NPC.alpha = 255;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/The End Of The Story");
            }
        }

        private void DiffSelector()
        {
            NPC.lifeMax = EmptySetUtils.ScaledNPCMaxLife(Main.masterMode ? 32000 : Main.expertMode ? 24000 : 12000);
            NPC.defense = Main.masterMode ? 12 : Main.expertMode ? 10 : 10;
            NPC.knockBackResist = Main.masterMode ? 0f : Main.expertMode ? 0f : 0f;
            NPC.damage = EmptySetUtils.ScaledNPCDamage(Main.masterMode ? 75 : Main.expertMode ? 50 : 40);
            frostBoltDamage = EmptySetUtils.ScaledProjDamage(Main.masterMode ? 60 : Main.expertMode ? 45 : 35);
            icicleDamage = EmptySetUtils.ScaledProjDamage(Main.masterMode ? 85 : Main.expertMode ? 65 : 50);
        }
        
        protected override void AIBefore(NPCStateMachine n)
        {
            Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.FrostStaff, 0f, 0f, 200)].noGravity = true;
        }
        /// <summary>
        /// npc移动
        /// </summary>
        /// <param name="n"></param>
        public void Move(NPCStateMachine n) 
        {
            NPC.rotation += 0.1f;
            float dis_x = n.target.Center.X - n.NPC.Center.X;
            float dis_y = n.target.Center.Y - n.NPC.Center.Y;
            float dis = (float)Math.Sqrt(dis_x * dis_x + dis_y * dis_y);
            dis = 0f / dis;
            NPC.velocity.X = dis_x * dis;
            NPC.velocity.Y = dis_y * dis;
        }

        public override void OnKill()
        {
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedFrozenCore, -1);
            Main.StartRain();
            Main.windSpeedTarget = 50;
            base.OnKill();
            Main.NewText("一场暴风雪席卷了北寒之地...巴拉巴拉", 255, 100, 100);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<FrozenCoreChest>()));
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return null;
        }

        public override int SpawnNPC(int tileX, int tileY)
        {
            return base.SpawnNPC(tileX, tileY);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return true;
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if(!canBeHit) return false;
            return base.CanBeHitByItem(player, item);
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (!canBeHit) return false;
            return base.CanBeHitByProjectile(projectile);
        }
    }
    /// <summary>
    /// npc状态：生成
    /// </summary>
    class SpawnState : NPCState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.Timer++;
            
            Main.windSpeedTarget = 0;

            //将npc移动到中心
            if (n.Timer == 1)
            {
                NPC.position += new Vector2(0, NPC.height / 2f);
            }
            //聚集特效
            else if (n.Timer < 8 * 60)
            {
                //生成80×80的正方形霜冻粒子特效,持续8秒
                for (int i = 0; i < Math.Floor(n.Timer / 60f) + 1; i++)
                {
                    Main.dust[Dust.NewDust(NPC.Center - new Vector2(n.Timer * 40 / 480f), (int)(n.Timer / 6f), (int)(n.Timer / 6f), DustID.FrostStaff, 0f, 0f, 200)].noGravity = true;
                }
                //生成特效粒子
                if (Main.rand.NextBool(2)) Dust.NewDust(Main.screenPosition, Main.screenWidth, Main.screenHeight, ModContent.DustType<FrozenCoreDust>());
                //把雪聚集到npc身上
                foreach (Dust d in Main.dust)
                {
                    if (d.active && (d.type == DustID.Snow || d.type == ModContent.DustType<FrozenCoreDust>()))
                    {
                        if (Vector2.Distance(d.position, NPC.Center) >= 80) d.velocity = (NPC.Center - d.position).SafeNormalize(Vector2.UnitX) * 8;
                        if (Vector2.Distance(d.position, NPC.Center) <= 10) d.active = false;
                    }
                }

            }
            //爆炸前准备，停雨，发声
            else if (n.Timer == 8 * 60)
            {
                //清除所有粒子
                foreach (Dust d in Main.dust)
                {
                    if (d.active && (d.type == DustID.Snow || d.type == ModContent.DustType<FrozenCoreDust>()))
                    {
                        d.active = false;
                    }
                }
                //生成粒子
                for (int i = 0; i < 200; i++)
                {
                    Dust.NewDust(NPC.Center - new Vector2(40), 80, 80, ModContent.DustType<FrozenCoreDust>(), 0f, 0f, 0);
                }
                //停雨
                Main.StopRain();
                //爆炸声
                SoundEngine.PlaySound(SoundID.Item20, NPC.position);

            }
            //爆炸
            else if (n.Timer == 8 * 60 + 1)
            {
                //爆炸粒子
                foreach (Dust d in Main.dust)
                {
                    if (d.active && d.type == ModContent.DustType<FrozenCoreDust>())
                    {
                        d.velocity = Main.rand.NextVector2Circular(10, 10);
                    }
                }
                /*for (int i = 0; i < 10; i++)
                {
                    float num1 = Main.rand.Next(-27, 28);
                    float num2 = Main.rand.Next(-27, 28);
                    float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
                    num3 = Main.rand.Next(9, 18) / num3;
                    num1 *= num3;
                    num2 *= num3;
                    Dust dust = Dust.NewDustDirect(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.FrostStaff, Scale: Main.rand.NextFloat(1.2f, 1.7f));
                    dust.noGravity = true;
                    dust.position = NPC.Center;
                    dust.position += new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
                    dust.velocity.X = num1;
                    dust.velocity.Y = num2;
                }*/
                //爆炸波
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<ExplodeProjectile>(), 0, 0, Main.myPlayer);
            }
            else if (n.Timer <= 8 * 60) 
            {
                NPC.alpha -= 2;
            }
            //生成完毕
            else
            {
                if (!SkyManager.Instance["FrozenCoreSky"].IsActive()) SkyManager.Instance.Activate("FrozenCoreSky");
                //初始化计时器,更换状态,允许攻击
                n.Timer = 0;
                NPC.alpha = 0;
                n.SetState<NormalState>();
                ((FrozenCore)n.NPC.ModNPC).canBeHit = true;
            }
        }
    }
    /// <summary>
    /// npc状态：空闲
    /// </summary>
    class NormalState : NPCState
    {
        //停留在原地，自身贴图会不断逆时针转动,1.5秒
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.Timer++;
            if (n.Timer > 1.5 * 60)
            {
                n.Timer = 0;
                n.SetState<AttackState>();
            }
            //移动
            ((FrozenCore)NPC.ModNPC).Move(n);
            //当npc血量低于50%时,允许特殊状态
            if (NPC.life <= NPC.lifeMax / 2) 
                n.allowNPCStates1AI = true;
            //无存活玩家，npc逃跑
            if (!n.targetIsActive) n.SetState<RunState>();
        }
    }
    /// <summary>
    /// npc状态：攻击
    /// </summary>
    class AttackState : NPCState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.Timer++;
            if (n.Timer == 1)
            {
                n.allowNPCAttackStatesAI = true;
            }
            //移动
            ((FrozenCore)NPC.ModNPC).Move(n);
        }
    }
    /// <summary>
    /// npc状态：逃跑
    /// </summary>
    class RunState : NPCState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            //禁用所有ai
            n.allowNPCStatesAI = false;
            n.allowNPCAttackStatesAI = false;
            n.allowNPCStates1AI = false;
            n.allowNPCStates2AI = false;
            NPC.velocity.Y = 10f;
            NPC.EncourageDespawn(10);
            NPC.noTileCollide = true;
        }
    }
    /// <summary>
    /// npc状态：死亡
    /// </summary>
    class DeathState : NPCState
    {
        public override void AI(NPCStateMachine n)
        {
            Main.NewText("DeathState");
            
        }
    }
    /// <summary>
    /// 攻击方式1:向玩家方向发射5颗扇形散射的冰弹（原版冰雪精射的那个），每颗之间的夹角为20度，连续发射5次，每次之间的间隔为30帧
    /// </summary>
    class AttackState1 : NPCAttackState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.AttackTimer++;
            if (n.Count < 5)
            {
                if (n.AttackTimer % 30 == 0)
                {
                    n.Count++;
                    for (int i = 0; i < 5; i++) 
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),
                            NPC.Center, (n.target.Center - NPC.Center).SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.ToRadians(40 - 20 * i)) * 5,
                            ProjectileID.FrostBlastHostile, ((FrozenCore)NPC.ModNPC).frostBoltDamage, 0f, Main.myPlayer);
                    }
                }
            }
            else 
            {
                n.Count = 0;
                n.Timer = 0;
                n.AttackTimer = 0;
                n.allowNPCAttackStatesAI = false;
                n.SetState<AttackState2>();
                n.SetState<NormalState>();
            }
        }
    }
    /// <summary>
    /// 攻击方式2:发射3圈12枚的冰弹，第2次的发射角度与1.3次不同
    /// </summary>
    class AttackState2 : NPCAttackState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.AttackTimer++;
            if (n.Count < 3)
            {
                if (n.AttackTimer % 30 == 0)
                {
                    n.Count++;
                    for (int i = 0; i < 12; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),
                            NPC.Center, Vector2.UnitX.RotatedBy(MathHelper.ToRadians(15 * n.Count - 30 * i)) * 5,
                            ProjectileID.FrostBlastHostile, ((FrozenCore)NPC.ModNPC).frostBoltDamage, 0f, Main.myPlayer);
                    }
                }
            }
            else
            {
                n.Count = 0;
                n.Timer = 0;
                n.AttackTimer = 0;
                n.allowNPCAttackStatesAI = false;
                n.SetState<AttackState3>();
                n.SetState<NormalState>();
            }
        }
    }
    /// <summary>
    /// 攻击方式3:在原地召唤4只原版的冰雪精
    /// </summary>
    class AttackState3 : NPCAttackState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.AttackTimer++;
            if (n.Count < 2)
            {
                if (n.AttackTimer % 30 == 0)
                {
                    n.Count++;
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.IceElemental);
                }
            }
            else
            {
                n.Timer = 0;
                n.AttackTimer = 0;
                n.Count = 0;
                n.allowNPCAttackStatesAI = false;
                n.SetState<AttackState4>();
                n.SetState<NormalState>();
            }
        }
    }
    /// <summary>
    /// 攻击方式4:在自身上方40格图格处向两边生成2排会穿墙的冰锥，两边的数量皆为15颗冰锥.每颗之间的间隔皆为7格图格
    /// </summary>
    class AttackState4 : NPCAttackState
    {
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.AttackTimer++;
            if (n.AttackTimer == 30)
            {
                for (int i = 0; i < 30; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),
                        NPC.Center + new Vector2(-14 * 7 * 16 - 15 * 22 - 3.5f + i * (7 * 16 + 22), -40 * 16), Vector2.Zero,
                        ModContent.ProjectileType<IcicleProjectile>(), ((FrozenCore)NPC.ModNPC).icicleDamage, 0f, Main.myPlayer);
                }
            }
            else if(n.AttackTimer > 30)
            {
                n.Timer = 0;
                n.AttackTimer = 0;
                n.allowNPCAttackStatesAI = false;
                n.SetState<AttackState1>();
                n.SetState<NormalState>();
            }
        }
    }
    /// <summary>
    /// （特殊）:每隔9~15秒就有30%的几率在玩家当前位置生成一片大小为5×5的正方形霜冻粒子特效，
    /// 粒子特效不会随玩家移动，生成4秒后boss会瞬移到粒子特效处，并发射一圈18枚的冰弹
    /// （注，这招并不会影响其他攻击方式的释放）
    /// </summary>
    class TeleportState : NPCState1
    {
        int t = 0;
        bool b = false;
        Vector2 vector2;
        public override void AI(NPCStateMachine n)
        {
            NPC = n.NPC;
            n.Timer1++;
            if (n.Timer1 == 1)
            {
                t = Main.rand.Next(180, 360);
            }
            else if (n.Timer1 == t)
            {
                b = Main.rand.NextBool(3, 10);
                vector2 = n.target.Center;
                if (b) SoundEngine.PlaySound(SoundID.Item4, n.target.position);
            }
            else if (n.Timer1 > t && n.Timer1 < t + 2 * 60)
            {
                if (b)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Main.dust[Dust.NewDust(vector2 - new Vector2(40), 80, 80, DustID.FrostStaff, 0f, 0f, 200)].noGravity = true;
                    }
                }
                else
                {
                    n.Timer1 = 0;
                }
            }
            else if (n.Timer1 == t + 4 * 60)
            {
                NPC.Center = vector2;
                SoundEngine.PlaySound(SoundID.Item8, n.target.position);
                for (int i = 0; i < 18; i++)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),
                        NPC.Center, Vector2.UnitX.RotatedBy(MathHelper.ToRadians(20 * i)) * 5,
                        ProjectileID.FrostBlastHostile, ((FrozenCore)NPC.ModNPC).frostBoltDamage, 0f, Main.myPlayer);
                }

                n.Timer1= 0;
            }
        }
    }
}
