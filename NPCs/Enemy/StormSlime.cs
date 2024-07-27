using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using System.Net.Mail;

namespace EmptySet.NPCs.Enemy
{
    public class StormSlime : BTLcNPC
    {
        private int FrameTimer = 0;
        private int _JumpTime = 0;
        private int _jumpHigh = 0;
        private Vector2 PlayerPos;
        //private int Timer = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("StormSlime");
        }
        public static int Life()
        {
            if (Main.masterMode) return 1190;
            else if (Main.expertMode) return 1400;
            else return 2000;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 86;
            NPC.height = 64;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0;
            NPC.rotation = 0;
            NPC.lifeMax = Life();
            Main.npcFrameCount[NPC.type] = 2;
            NPC.scale = 2;
            NPC.knockBackResist = 0f;
        }
        enum NPCState
        {
            Normal,
            Ready,
            ReadytoJump,
            shoot,
            Jump,move
        }
        public override void AI()
        {
            base.AI();
            FrameTimer++;
            NPC.direction = (int)NPC.ai[2];
            switch ((NPCState)State)
            {
                case NPCState.Normal://初始化
                    {
                        NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;//随机选择方向
                        SwitchState((int)NPCState.Ready);
                        break;
                    }
                case NPCState.Ready:
                    {
                        Player player = Main.player[NPC.target];
                        if (NPC.velocity.Y == 0)//当npc落地时做出的反应
                        {
                            NPC.velocity.X *= 0.9f;
                        }
                        Timer++;
                        if (Timer > (Main.expertMode?30:45))
                        {
                            Timer = 0;
                            if (_JumpTime == 1)
                            {
                                if(Main.rand.NextBool())SwitchState((int)NPCState.shoot);
                                else
                                {
                                    PlayerPos = player.position;
                                    SwitchState((int)NPCState.move);
                                }
                            }
                            _JumpTime += 1;
                            if (_JumpTime > 1) _JumpTime = 0;
                            else
                            {
                                _jumpHigh += 1;
                                NPC.velocity.X = NPC.ai[2] * 3;// 提供初始跳跃速度
                                if (_jumpHigh%3== 2) NPC.velocity.Y = -8;
                                else NPC.velocity.Y = -6;
                                SwitchState((int)NPCState.Jump);
                            }
                            break;
                        }
                        if (Timer % 30 < 1)
                        {
                            SwitchState((int)NPCState.ReadytoJump);
                        }
                            NPC.TargetClosest();
                            NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                        if (Math.Abs(NPC.velocity.Y) > 0.1f)
                        {
                            SwitchState((int)NPCState.Jump);
                        }
                        break;
                    }
                case NPCState.ReadytoJump:
                    {
                        Timer++;
                        if (Timer % 30 == 15)
                        {
                            SwitchState((int)NPCState.Ready);
                        }
                        break;
                    }
                case NPCState.Jump:
                    {
                        Timer++;
                        NPC.velocity.X = NPC.ai[2] * 5;
                        Player player = Main.player[NPC.target];
                        if ((NPC.collideY && NPC.position.Y > player.position.Y - 160) || Timer > 180)
                        {
                            SwitchState((int)NPCState.Ready);

                            if (Main.rand.NextBool())
                            {
                                for (int i = 0; i < 9; i++)
                                {
                                    Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 4.5f * i);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage, 10);
                                    Proj.scale = 1;
                                }
                            }
                            else
                            {
                                Vector2 ShootVelocity;
                                ShootVelocity = player.position - NPC.Center;
                                ShootVelocity.Normalize();
                                for (int i = 0; i < (Main.expertMode ? (Main.getGoodWorld ? 5 : 3) : 2); i++)
                                {
                                    Vector2 ShootVelocity2 = ShootVelocity.RotatedBy((Main.rand.NextFloat(1f) - 0.5) * Math.PI / 6) * (Main.expertMode ? 6 : 5);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.FirmFossil>(), NPC.damage / 4, 10);
                                    Proj.scale = 2;
                                }
                            
                            }
                        }
                        break;
                    }
                case NPCState.shoot:
                    {
                        Timer++;
                        if (Timer % 25 == 0)
                        {
                            Player player = Main.player[NPC.target];
                            Vector2 ShootVelocity = player.Center - NPC.Center;
                            ShootVelocity /= ShootVelocity.Length();
                            ShootVelocity *= 10f;
                            float r = (float)Math.Atan2(ShootVelocity.Y, ShootVelocity.X);
                            int Count = 2;

                            for (int i = -Count; i <= 1; i++)

                            {
                                float r2 = r + i * MathHelper.Pi / 15f;
                                Vector2 shootVel = r2.ToRotationVector2() * 5.5f;
                                Projectile proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, shootVel, ModContent.ProjectileType<Projectiles.MonsterProj.FirmFossil>(), NPC.damage / 4, 10);
                                proj.scale = 2;
                            }
                        }
                        if(Timer>45)SwitchState((int)NPCState.Ready);
                        break;
                    }
                case NPCState.move:
                    {
                        Timer++;
                        Player player = Main.player[NPC.target];
                        NPC.scale = 2 - (float)Timer / 50;
                        if (Timer % 2 == 0)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                Vector2 Pos = PlayerPos + new Vector2(90-Timer, 0).RotatedBy(Math.PI / 4 * i);
                                Dust.NewDustDirect(Pos, 10, 10, 124, 0, 0);
                            }
                        }
                        if (Timer >= 90)
                        {
                            Timer = 0;
                            NPC.scale = 2;
                            NPC.position = PlayerPos - new Vector2(NPC.width / 2, NPC.height+20);
                            NPC.ai[2] = Main.rand.NextBool() ? -1 : 1;
                            NPC.direction = (int)NPC.ai[2];
                            SwitchState((int)NPCState.Normal);
                            if (Main.expertMode)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    Vector2 ShootVelocity2 = new Vector2(0, -8).RotatedBy(Math.PI / 2 * i);
                                    Projectile Proj = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, ShootVelocity2, ModContent.ProjectileType<Projectiles.MonsterProj.SandRock>(), NPC.damage / 4, 10);
                                    Proj.scale = 1.5f;
                                }
                            }
                            Timer = 0;
                        }
                        break;
                    }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            if (FrameTimer % 14 < 7)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frame.Y = 0;
            }
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 5));
        }
    }
}
