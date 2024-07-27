using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
namespace EmptySet.NPCs.Boss.SandStormSlime
{
    [AutoloadBossHead]
    public class SandStormSlime : BTLcNPC
    {
        private bool Rage = false;//被激怒
        private bool SuperRage = false;//狂怒
        private int RageTime = 0;
        private int FrameTimer = 0;
        private int AIstate=0;
        private int LastState = 0;
        private Vector2 PlayerPos;
        private bool HitText = false;
        private bool DeathText = false;
        private int HitCD = 0;
        //private int Timer = 0;
        public override void SetStaticDefaults()
        {
        }
        public static int Life()
        {
            if (Main.masterMode) return 3570;
            else if (Main.expertMode) return 4200;
            else return 6000;
        }
        public override void SetDefaults()
        {
            NPC.lavaImmune = true;
            base.SetDefaults();
            NPC.width = 93;
            NPC.height = 56;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Main.rand.Next(8,14)*10000;
            NPC.rotation = 0;
            NPC.lifeMax = Life();
            NPC.boss= true;
            NPC.npcSlots = 15f;
            Music = MusicID.OtherworldlyBoss1;
            Main.npcFrameCount[NPC.type] = 6;
            NPC.scale = 3;
            NPC.knockBackResist = 0f;
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[] {
                BuffID.Poisoned,
                }
            };
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Drying");
            }
        }
        enum NPCState
        {
            Init,//初始化
            Normal,//正常状态
            Jump,
            Attact1,
            Attack3,
            Attack6,
            Attack7,
            Shoot,
            Move//瞬移
        }
        public int GetState()//根据剩余生命计算状态
        {
            float RATIO = (float)NPC.life/(float)NPC.lifeMax;
            if (RATIO < 0.3f) return 2;
            else if (RATIO < 0.7f) return 1;
            return 0;
        }
        public int TimeOperation(float number)//根据难度和狂怒情况计算攻击间隔,expert是普通难度攻击间隔-33%的额外计算
        {
            if (!Main.expertMode)
            {
                number *= 1.5f;
            }
            if (Rage && !SuperRage)
            {
                number *= 0.83f;//攻速+20%
            }
            if (SuperRage)
            {
                if(Main.getGoodWorld)number *= 0.5f;//攻速+100%
                else number *= 0.67f;//攻速+50%
            }
            return (int)number;
        }
        public override bool CheckDead()//死亡执行
        {
            if (NPC.AnyNPCs(ModContent.NPCType<NPCs.Enemy.StormSlime>()))
            {
                if (!DeathText)
                {
                    DeathText = true;
                    Main.NewText("风暴史莱姆正为它提供最后的力量!", Color.BlueViolet);
                }
                NPC.life = 1;
                return false;
            }
            else
            {
                Sandstorm.StopSandstorm();
                return true;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {/*
            if (HitCD > 60)
            {
                HitCD = 0;
                if (!HitText)
                {
                    Main.NewText("你就这点能耐?", Color.BlueViolet);
                    HitText = true;
                }
            }*/
        }
        public override void AI()
        {
            HitCD++;
            Timer++;
            FrameTimer++;
            Player player = Main.player[NPC.target];
            if((!Main.dayTime && !player.ZoneDesert && !SuperRage)||(Main.getGoodWorld && !SuperRage))
            {   
                if(Main.getGoodWorld) Main.NewText("一股神秘的力量使沙暴史莱姆变得暴躁不安...", new Color(255, 0, 0));
                else Main.NewText("你的失礼行为彻底惹怒了沙暴史莱姆!", new Color(255,0,0));
                if (!Rage)
                {
                    NPC.defense += 5;
                    NPC.damage = (int)((float)NPC.damage*1.2f);
                }
                NPC.damage = (int)((float)NPC.damage * 1.25f);
                NPC.defense += 10;
                Rage = true;
                SuperRage = true;
            }
            if (!Rage&&(!Main.dayTime||!player.ZoneDesert)&&RageTime>600&&!SuperRage)//进入狂暴模式的特别判断
            {
                NPC.defense += 5;
                NPC.damage = (int)((float)NPC.damage * 1.2f);
                Main.NewText("你的失礼行为惹怒了沙暴史莱姆!", Color.BlueViolet);
                Rage = true;
            }
            if(!Main.dayTime || !player.ZoneDesert)
            {
                RageTime++;//在非沙漠或夜晚增加计数 达到300时狂暴
            }
            else
            {
                RageTime = 0;
            }
            switch ((NPCState)State)
            {
                case NPCState.Init://初始化
                    {
                        NPC.TargetClosest();
                        AIstate = 0;
                        SwitchState((int)NPCState.Normal);
                        break;
                    }
                case NPCState.Normal:
                    {
                        NPC.velocity *= 0.9f;//速度修正
                        if(LastState!= GetState())//当阶段发生变化时
                        {
                            AIstate = 0;
                            if (GetState() == 1)
                            {
                                NPC.scale = 2.5f;
                                for (int i = 0; i < (Main.getGoodWorld?2:1); i++) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<NPCs.Enemy.StormSlime>());
                                for(int i=0;i<5;i++)NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X+Main.rand.Next(-5,5), (int)NPC.position.Y + Main.rand.Next(-5, 5), 537);//沙漠史莱姆
                                Sandstorm.StartSandstorm();
                                Main.NewText("风暴史莱姆已苏醒！沙暴席卷了整个战场！", Color.BlueViolet);
                            }
                            if (GetState() == 2)
                            {
                                NPC.scale = 2f;
                                for(int i=0;i<5;i++) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + Main.rand.Next(-5, 5), (int)NPC.position.Y + Main.rand.Next(-5, 5), 537);
                                for (int i = 0; i < (Main.getGoodWorld ? 3 : 1); i++) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<NPCs.Enemy.StormSlime>());
                                Main.NewText("来自沙暴史莱姆的最后一击！", Color.BlueViolet);
                            }
                        }
                        LastState = GetState();
                        if (GetState() == 0)//1阶段
                        {
                            if (AIstate == 2 || AIstate == 3)//坠击
                            {
                                if (Timer > TimeOperation(45))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = -15;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Attact1);
                                }
                            }
                            else if (AIstate == 8)//冲刺
                            {
                                if (Timer > TimeOperation(45))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    Vector2 Velo;
                                    Velo = player.position - NPC.position;
                                    Velo.Normalize();
                                    Velo *= 5;
                                    NPC.velocity += Velo*7;
                                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center,Velo, 871, NPC.damage / 4, 10);
                                }
                            }
                            else if(AIstate==5)//沙尘封路
                            {
                                if (Timer > TimeOperation(45))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.expertMode ? (Main.getGoodWorld ? -240 : -360) : -450, 0), new Vector2(0, -2), 657, NPC.damage / 4, 10);
                                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.expertMode ? (Main.getGoodWorld ? 240 : 360) : 450, 0), new Vector2(0, -2), 657, NPC.damage / 4, 10);
                                }
                            }
                            else if (AIstate == 10)
                            {
                                if (Timer > TimeOperation(45)) 
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.Move);
                                }
                            }
                            else
                            {
                                if (Timer > TimeOperation(60))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    //sMain.NewText($"{AIstate} 2");
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = Main.rand.NextBool() ? -7 : -12;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Jump);
                                }
                            }
                            if (AIstate > 10) AIstate = 0 ;
                        }
                        else if (GetState() == 1)//二阶段
                        {
                            if (AIstate == 1 || AIstate == 2 || AIstate == 3)//坠击
                            {
                                if (Timer > TimeOperation(40))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = -15;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Attack3);
                                }
                            }
                            else if (AIstate == 7)//冲刺
                            {
                                if (Timer > TimeOperation(40))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    Vector2 Velo;
                                    Velo = player.position - NPC.position;
                                    Velo.Normalize();
                                    Velo *= 6;
                                    NPC.velocity += Velo * 8;
                                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velo, 871, NPC.damage / 4, 10);
                                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X + Main.rand.Next(-5, 5), (int)NPC.position.Y + Main.rand.Next(-5, 5), 537);
                                    if (Main.expertMode)
                                    {
                                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velo.RotatedBy(Math.PI / 6), 871, NPC.damage/4, 10);
                                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velo.RotatedBy(Math.PI / -6), 871, NPC.damage/4, 10);

                                    }
                                }
                            }
                            else if (AIstate == 5 && Main.expertMode)//沙尘封路
                            {
                                if (Timer > TimeOperation(40))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    for (int i = 0; i < (Main.expertMode? (Main.getGoodWorld ? 5 : 3) : 2); i++)
                                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + new Vector2(Main.expertMode?520:700, 0).RotatedBy(Math.PI * Main.rand.NextFloat(2f)), new Vector2(0, -2), 657, NPC.damage / 4, 10);
                                }
                            }
                            else if (AIstate == 9)//瞬移
                            {
                                if (Timer > TimeOperation(40))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.Move);
                                }
                            }
                            else
                            {
                                if (Timer > TimeOperation(50))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    //sMain.NewText($"{AIstate} 2");
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = Main.rand.NextBool() ? -6 : -9;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Jump);
                                }
                            }
                            if (AIstate > 9) AIstate = 0;
                        }
                        else if (GetState() == 2)
                        {
                            if (AIstate == 0||AIstate==1||AIstate==2)//瞬移
                            {
                                if (Timer > TimeOperation(30))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.Move);
                                }
                            }
                            else if (AIstate == 5)//坠击
                            {
                                if (Timer > TimeOperation(30))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = -15;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Attack3);
                                }
                            }
                            else if (AIstate == 6)//沙尘封路
                            {
                                if (Timer > TimeOperation(60))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    for (int i = 0; i < (Main.expertMode ? (Main.getGoodWorld ? 6 : 4) : 3); i++)
                                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), player.Center + new Vector2((Main.expertMode ? 420 : 560), 0).RotatedBy(Math.PI * Main.rand.NextFloat(2f)), new Vector2(0, -2), 657, NPC.damage/4, 10);
                                }
                            }
                            else if (AIstate == 9||AIstate==8||AIstate==7)//冲刺
                            {
                                if (Timer > TimeOperation(120))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    Vector2 Velo;
                                    Velo = player.position - NPC.position;
                                    Velo.Normalize();
                                    Velo *= 7;
                                    NPC.velocity += Velo * 9;
                                    Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velo, 871, NPC.damage / 4, 10);
                                }
                            }
                            else if (AIstate == 10)//瞬移
                            {
                                if (Timer > TimeOperation(30))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.Move);
                                }
                            }
                            else if (AIstate == 11|| AIstate == 3 || AIstate == 9)//发射
                            {
                                if (Timer > TimeOperation(30))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Shoot);
                                }
                            }
                            else
                            {
                                if (Timer > TimeOperation(30))
                                {
                                    Timer = 0;
                                    AIstate++;
                                    NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                                    NPC.velocity.X = NPC.ai[2] * 5;
                                    NPC.velocity.Y = Main.rand.NextBool() ? -6 : -9;
                                    NPC.direction = (int)NPC.ai[2];
                                    SwitchState((int)NPCState.Jump);
                                }
                            }
                            if (AIstate > 12) AIstate = 0;
                        }
                        break;
                    }
                case NPCState.Jump:
                    {
                        NPC.velocity.X = NPC.ai[2] * 5;
                        if (NPC.collideX) NPC.velocity.X = 0;
                        if ((NPC.collideY && NPC.position.Y > player.position.Y - 160) || Timer>180)
                        {
                            NPC.velocity.X = 0;
                            Timer = 0;
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            SwitchState((int)NPCState.Normal);
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-5, 5), (int)NPC.Center.Y + Main.rand.Next(-5, 5), 537);
                        }
                        else if (NPC.collideY)
                        {
                            NPC.position.Y += NPC.velocity.Y;
                        }
                        break;
                    }
                case NPCState.Attact1://一阶段坠击
                    {
                        //NPC.velocity.Y += 2f;
                        NPC.velocity.X = NPC.ai[2] * 3;
                        if (NPC.collideX) NPC.velocity.X = 0;
                        if (NPC.collideY && NPC.position.Y > player.position.Y - 160 || Timer > 180)
                        {
                            NPC.velocity.X = 0;
                            Timer = 0;
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            for (int i = 0; i < 4; i++)
                            {
                                Vector2 ShootVelocity2 = new Vector2(0,-8).RotatedBy(Main.rand.NextFloat(-1,1) * Math.PI / 2);
                                Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage / 4, 10);
                                Proj.scale = 1;
                            }
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-5, 5), (int)NPC.Center.Y + Main.rand.Next(-5, 5), 537);
                            SwitchState((int)NPCState.Normal);
                        }
                        else if (NPC.collideY)
                        {
                            NPC.position.Y += NPC.velocity.Y;
                        }
                        break;
                    }
                case NPCState.Attack3://二阶段坠击
                    {
                        NPC.velocity.X = NPC.ai[2] * 3;
                        if (NPC.collideX) NPC.velocity.X = 0;
                        if (NPC.collideY && NPC.position.Y > player.position.Y - 160 || Timer > 180)
                        {
                            NPC.velocity.X = 0;
                            Timer = 0;
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            int Num = 6;
                            if (GetState() == 2) Num = 20;
                            for (int i = 0; i < Num; i++)
                            {
                                Vector2 ShootVelocity2 = new Vector2(0, -10).RotatedBy(Main.rand.NextFloat(-1, 1) * Math.PI / 2);
                                Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage/4, 10);
                                Proj.scale = 1.5f;
                            }
                            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-5, 5), (int)NPC.Center.Y + Main.rand.Next(-5, 5), 537);
                            SwitchState((int)NPCState.Normal);
                        }
                        else if (NPC.collideY)
                        {
                            NPC.position.Y += NPC.velocity.Y;
                        }
                        break;
                    }
                case NPCState.Move://瞬移
                    {
                        NPC.scale = 3-GetState()*0.5f - (float)Timer / 30;
                        if (Timer % 2 == 0 && Timer > 40)
                        {
                            for (int i = 0; i < 16; i++)
                            {
                                Vector2 Pos = PlayerPos + new Vector2(120 - GetState() * 15 - Timer , 0).RotatedBy(Math.PI / 8 * i);
                                Dust.NewDustDirect(Pos, 10, 10,124, 0, 0);
                            }
                        }
                        if (Timer < 60 - GetState() * 7)
                        {
                            if (Main.getGoodWorld) PlayerPos = player.position + player.velocity * 10;
                            else PlayerPos = player.position;
                        }
                        if (Timer >= 120-GetState() *15)
                        {
                            NPC.scale = 3-GetState()*0.5f;
                            NPC.position = PlayerPos -new Vector2(NPC.width/2, NPC.height+20);
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            for (int i = 0; i < 2; i++) NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-5, 5), (int)NPC.Center.Y + Main.rand.Next(-5, 5), 537);
                            SwitchState((int)NPCState.Normal);
                            if (GetState() == 0)
                            {
                                if (Main.expertMode)
                                {
                                    for (int i = 0; i < 6; i++)
                                    {
                                        Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 3 * i);
                                        Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage / 4, 10);
                                        Proj.scale = 1;
                                    }
                                }
                            }
                            else if (GetState() == 1)
                            {
                                for (int i = 0; i < 12; i++)
                                {
                                    Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 6 * i);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage / 4, 10);
                                    Proj.scale = 1.5f;
                                }
                            }
                            else if (GetState() == 2)
                            {
                                for (int i = -3; i < 4; i++)
                                {
                                    Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 9 * i);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage / 4, 10);
                                    Proj.scale = 1.5f;
                                }
                            }

                            Timer = 0;
                        }
                        break;
                    }
                case NPCState.Shoot:
                    {
                        if (Timer % (Main.expertMode?20:25) == 0)
                        {
                            Vector2 ShootVelocity;
                            if (!Main.getGoodWorld)
                            {
                                ShootVelocity = player.position - NPC.Center;
                            }
                            else//ftw预判攻击
                            {
                                ShootVelocity = player.position - NPC.Center;
                                float tm = ShootVelocity.Length() / 12f;
                                Vector2 Pos2 = player.position + player.velocity * tm;
                                ShootVelocity = Pos2 - NPC.position;
                                tm = ShootVelocity.Length() / 12f;
                                Pos2 = player.position + player.velocity * tm;
                                ShootVelocity = Pos2 - NPC.position;
                            }
                            ShootVelocity.Normalize();
                            for (int i = 0; i < (Main.expertMode ? (Main.getGoodWorld?5:3) : 2); i++)
                            {
                                Vector2 ShootVelocity2 = ShootVelocity.RotatedBy((Main.rand.NextFloat(1f) - 0.5) * Math.PI / 6) * (Main.expertMode?6:5);
                                Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.FirmFossil>(), NPC.damage/4, 10);
                                Proj.scale = 2;
                            }
                        }
                        if (Timer >= (Main.expertMode ? 60 : 100))
                        {
                            SwitchState((int)NPCState.Normal);
                            Timer = 0;
                        }
                        break;
                    }
            }
            base.AI();
        }
        public override void FindFrame(int frameHeight)
        {   
            for(int i = 0; i < 5; i++)
            {
                if (FrameTimer % 35 < i * 7)
                {
                    NPC.frame.Y = i*frameHeight;
                    break;
                }
            }              
        }
        public override void OnSpawn(IEntitySource source)
        {
            //Sandstorm.StartSandstorm();
            base.OnSpawn(source);
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
        }
    }
}
