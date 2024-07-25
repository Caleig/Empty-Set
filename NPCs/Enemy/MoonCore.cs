using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
namespace EmptySet.NPCs.Enemy
{
    public class MoonCore : BTLcNPC
    {
        private bool _isFriendly = true;
        private Vector2 BaseVector;
        public int Direction = 1;
        public float Speed = 0.5f;
        public int TurnCD = 0;
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 26;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100;
            NPC.rotation = 0;
            NPC.lifeMax = 40;
            Main.npcFrameCount[NPC.type] = 2;//NPC帧图数量为2
        }
        enum NPCState
        {
            Init,//初始化
            Find,//寻敌
            Fly,//旋绕
            Shoot//随机跳跃
        }
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            base.AI();
            NPC.direction = (int)NPC.ai[2]; 
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                _isFriendly = true;
            }
            switch((NPCState)State)
            {
                case NPCState.Init://初始化
                {
                    NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;//随机选择方向
                    SwitchState((int)NPCState.Find);
                    break;
                }
                case NPCState.Find:
                    {
                        NPC.velocity = (player.Center - NPC.Center);//向玩家飞
                        NPC.velocity.Normalize();
                        NPC.velocity *= 3f;
                        if ((player.position - NPC.position).Length()< 200)
                        {
                            Timer = 0;
                            BaseVector = (player.position - NPC.position);
                            BaseVector.Normalize();
                            BaseVector *= 200;
                            SwitchState((int)NPCState.Fly);
                        }
                        break;
                    }
                case NPCState.Fly:
                    {
                        Timer++;
                        TurnCD++;
                        Speed += 0.05f;
                        if (Speed > 2) Speed = 2;
                        if((NPC.collideX|| NPC.collideY) && TurnCD>15)
                        {
                            TurnCD = 0;
                            Speed = 0.5f;
                            Direction *= -1;
                        }
                        if (Timer > 120)
                        {
                            Timer = 0;
                            SwitchState((int)NPCState.Shoot);
                        }
                        BaseVector = BaseVector.RotatedBy(Direction*Speed*Math.PI/180);
                        NPC.position = player.position + BaseVector;
                        break;
                    }
                case NPCState.Shoot:
                    {
                        Vector2 Pos;
                        if (Main.rand.NextBool())
                        {
                            float Tm;
                            Tm= (player.position - NPC.position).Length() / 10f;
                            Pos = player.position + player.velocity * Tm;
                            Tm = (Pos - NPC.position).Length() / 10f;
                            Pos = player.position + player.velocity * Tm;
                            //Main.NewText("预判");
                        }
                        else
                        {
                            Pos = player.position;
                            //Main.NewText("不预判");
                        }
                        Vector2 Velocity = Pos - NPC.position;
                        Velocity.Normalize();
                        Velocity *= 10;
                        Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity, 38, 20, 10);
                        SwitchState((int)NPCState.Fly);
                        break;
                    }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (Timer%14<7)
            {
               NPC.frame.Y = 0;
            }   
            else
            {
                NPC.frame.Y = frameHeight;
            }              
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            Vector2 Velocity = player.position - NPC.position;
            Velocity.Normalize();
            Velocity *= 15;
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity, 38, 20, 10);
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity.RotatedBy(Math.PI / 20), 38, 20, 10);
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity.RotatedBy(Math.PI / -20), 38, 20, 10);
            _isFriendly = false;
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[NPC.target];
            Vector2 Velocity = player.position - NPC.position;
            Velocity.Normalize();
            Velocity *= 15;
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity, 38, 20, 10);
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity.RotatedBy(Math.PI / 20), 38, 20, 10);
            Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, Velocity.RotatedBy(Math.PI / -20), 38, 20, 10);
            _isFriendly = false;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 5));
            npcLoot.Add(ItemDropRule.Common(1309, 1000));
        }
    }
}