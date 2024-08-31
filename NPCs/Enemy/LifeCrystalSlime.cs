using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy
{
    public class LifeCrystalSlime : BTLcNPC
    {
        private bool _isFriendly = true;
        private int _JumpTime = 0;
        private int _jumpHigh = 0;
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.width = 32;
            NPC.height = 26;
            NPC.damage = 15;
            NPC.defense = 0;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100;
            NPC.rotation = 0;
            NPC.lifeMax = 120;
            if (Main.expertMode)
            {
                if (Main.hardMode)
                {
                    NPC.lifeMax = 300;
                    NPC.damage = 30;
                    if (NPC.downedPlantBoss)
                    {
                        NPC.lifeMax = 350;
                    }
                }
            }
            Main.npcFrameCount[NPC.type] = 2;//NPC帧图数量为2
        }
        enum NPCState
        {
            Normal,
            Ready,
            ReadytoJump,
            shoot,
            Jump//共有五种状态
        }
        public override void AI()
        {
            base.AI();
            NPC.direction = (int)NPC.ai[2]; 
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                _isFriendly = true;
            }
            if(!NPC.dontTakeDamage && NPC.lastInteraction != -1)
                _isFriendly = false;
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
                    if (NPC.velocity.Y == 0)//当npc落地时做出的反应
                    {
                        NPC.velocity.X *= 0.9f;
                    }
                    Timer++;
                    if (Timer > 100)//如果正常状态持续时间超过6次抽动，那么就进入跳跃状态
                    {
                        Timer = 0;
                        if (_JumpTime==2 && !_isFriendly) SwitchState((int)NPCState.shoot);
                        _JumpTime += 1;
                        if (_JumpTime > 2) _JumpTime = 0;
                        else
                        {
                            _jumpHigh += 1;
                            if (_jumpHigh > 2) _jumpHigh = 0;
                            NPC.velocity.X = NPC.ai[2] * 3;// 提供初始跳跃速度
                            if (_jumpHigh==2) NPC.velocity.Y = -8;
                            else NPC.velocity.Y = -6;
                            SwitchState((int)NPCState.Jump);
                        }
                        break;
                    }
                    if (Timer % 30 < 1)
                    {
                        SwitchState((int)NPCState.ReadytoJump);
                    }
                    if (!_isFriendly)
                    {
                        NPC.TargetClosest();
                        Player player = Main.player[NPC.target];
                        NPC.ai[2] = player.Center.X > NPC.Center.X ? 1 : -1;
                    }
                    if(Math.Abs(NPC.velocity.Y) > 0.1f)
                    {
                        SwitchState((int)NPCState.Jump);
                    }
                    break;
                }
                case NPCState.ReadytoJump:
                {
                    Timer++;
                    if(Timer % 30 == 15)
                    {
                        SwitchState((int)NPCState.Ready);
                    }
                    break;
                }
                case NPCState.Jump:
                {
                    NPC.velocity.X= NPC.ai[2] * 3;
                    if (NPC.collideY)
                    {
                    SwitchState((int)NPCState.Ready);
                    }
                    break;
                }
                case NPCState.shoot:
                {
                    if(!_isFriendly)
                    {
                        Player player = Main.player[NPC.target]; 
                        Vector2 ShootVelocity = player.Center - NPC.Center;
                        ShootVelocity /= ShootVelocity.Length();
                        ShootVelocity *= 10f;
                        float r = (float)Math.Atan2(ShootVelocity.Y, ShootVelocity.X);

                            for (int i = -1; i <= 1; i++)

                            {
                                float r2 = r + i * MathHelper.Pi / (disorder ? 24f : 18f);
                                Vector2 shootVel = r2.ToRotationVector2() * 7.5f;
                                Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.position, shootVel, ModContent.ProjectileType<Projectiles.MonsterProj.LifeCrystalshards>(), 20, 10);
                            }
                            
                    }
                    SwitchState((int)NPCState.Ready);
                    break;
                }
            }
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.frame.Y = frameHeight;
            if ((Timer%14<7 && _jumpHigh!=0 )||( _jumpHigh==0 && Timer%8<4 && Timer>70)||(_jumpHigh==0 && Timer % 14 < 7 && Timer <= 70))
            //高跳前一跳前0.5s抖动速度加快
            {
                // 切换到第二帧
               NPC.frame.Y = 0;
            }   
            else
            {
                // 切换到第一帧
                NPC.frame.Y = frameHeight;
            }              
        }
        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            _isFriendly = false;
            hit.HitDirection = -hit.HitDirection;
            base.OnHitByItem(player, item, hit, damageDone);
        }
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            _isFriendly = false;
             hit.HitDirection = -hit.HitDirection;
            base.OnHitByProjectile(projectile, hit, damageDone);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)//自然刷新
        {
            if (Main.dayTime) 
            {
                if (Main.hardMode)
                {
                    return SpawnCondition.OverworldDaySlime.Chance * 0.04f;
                }
                else
                {
                    return SpawnCondition.OverworldDaySlime.Chance * 0.1f;
                }
            }
            else return 0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.LifeCrystal));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 2, 5));
            npcLoot.Add(ItemDropRule.Common(1309, 1000));
        }
    }
}