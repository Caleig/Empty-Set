using Microsoft.Xna.Framework;
using EmptySet.Items.Consumables;
using EmptySet.Items.Materials;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.Items.Weapons.Throwing;
using EmptySet.Projectiles.Boss;
using EmptySet.Utils;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.NPCs.Boss.LavaHunter
{
    [AutoloadBossHead]
    internal class LavaHunter:ModNPC
    {
        private bool spawned = false;
        const int SEGMENT_NUMBER = 37;
        Player player;
        Vector2 targetPos;
        int solarFragmentTimer=0;
        int dashTimer = 0;
        int lavaProjTimer = 0;
        bool isDash = false;
        int status = 0;
        public bool flies = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("熔岩游猎者");
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BoneJavelin] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.BloodButcherer] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Frostburn] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Venom] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 1;

            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);

            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "EmptySet/NPCs/Boss/LavaHunter/LavaHunter_Still",
                PortraitScale = 0.8f,
                //Scale = 1.25f,
                //Position = new Vector2(16 * 10.5f * 1.25f, 0),
                //PortraitScale = 1.25f,
                //PortraitPositionXOverride = 16 * 8 * 1.25f
            });
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,
                new FlavorTextBestiaryInfoElement("丛林游猎者的远亲之一，其身上的鳞片经岩浆洗礼后变得如同黑曜石一般漆黑")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 128;
            NPC.height = 130;
            NPC.damage = 105;
            NPC.defense = 14;
            NPC.lifeMax = 36000;
            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(0, Main.rand.Next(4,8));

            NPC.boss = true;
            NPC.behindTiles = true;
            NPC.trapImmune = true;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/RequiemOfScourge");
            }
        }
        float maxSpeed = 20;
        float turnSpeed=0.08f;
        float acceleration=0.08f;
        int npcDamage = 0;
        int lavaDamage = 0;
        public override bool PreAI()
        {
            NPC.lifeMax = Main.masterMode ? 36000 : Main.expertMode ? 21000 : 18000;
            NPC.defense = Main.masterMode ? 12 : Main.expertMode ? 10 : 10;
            NPC.damage = Main.masterMode ? 140 : Main.expertMode ? 105 : 70;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
            npcDamage = Main.masterMode ? 160 : Main.expertMode ? 120 : 80;
            lavaDamage = Main.masterMode ? 110 : Main.expertMode ? 90 : 60;
            return base.PreAI();
        }
        public override void AI()
        {
            NPC.ai[3] = 0;
            if (!spawned) spawn();
            findPlayer();

            if (NPC.life <= NPC.lifeMax*0.8)
            {
                if (solarFragmentTimer == 1) 
                {
                    for (int i = 0; i < 4; i++)
                    {
                        int npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.SolarGoop);
                        Main.npc[npc].velocity = NPC.velocity.RotatedBy(MathHelper.ToRadians(-45+30*i)) *1.8f;
                        Main.npc[npc].damage = npcDamage;
                    }
                }
                if (solarFragmentTimer >= 60 * 1) solarFragmentTimer = 0;
                solarFragmentTimer++;
            }
            if (NPC.life <= NPC.lifeMax * 0.1)
            {
                isDash = true;
            }
            if (NPC.life <= NPC.lifeMax * 0.5)
            {
                lavaProjTimer++;
                if (lavaProjTimer > 5 * 60)
                {
                    for (int i = -1; i < 10; i++)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(), player.Center + new Vector2(i * 6 * 32 * (player.velocity.X > 0 ? 1 : -1), -40 * 16), Vector2.Zero, ModContent.ProjectileType<LavaProj>(), EmptySetUtils.ScaledProjDamage(lavaDamage), 0, Main.myPlayer);
                    }
                    lavaProjTimer = 0;
                }
            }
            switch (status) 
            {
                case 0:
                    WormMovement2(player, maxSpeed, turnSpeed, acceleration);
                    if (isDash) dashTimer++;
                    if (dashTimer > 5 * 60) 
                    {
                        status = 1;
                        dashTimer = 0;
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
                case 1:
                    dashTimer++;
                    if (dashTimer < 200)
                    {
                        Movement(new Vector2(player.position.X - player.direction * 10 * 16, player.position.Y + 16 * 35), 0.4f, 18);
                    }
                    else 
                    {
                        status =2;
                        dashTimer = 0;
                        /*
                        float rotationDifference = MathHelper.WrapAngle(NPC.velocity.ToRotation() - NPC.DirectionTo(player.Center).ToRotation());
                        bool inFrontOfMe = Math.Abs(rotationDifference) < MathHelper.ToRadians(5);
                        if (dashTimer>300|| inFrontOfMe) 
                        { 
                            status =2;
                            dashTimer = 0;
                        }
                        else
                        {
                            NPC.velocity = Vector2.Normalize(NPC.velocity) * Math.Min(10f, NPC.velocity.Length() + 1f)+ new Vector2(player.Center.X-NPC.Center.X,0)*0.2f;
                            NPC.velocity += NPC.velocity.RotatedBy(MathHelper.PiOver2) * NPC.velocity.Length() / 300;
                        }*/
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
                case 2:
                    if (dashTimer == 0) NPC.velocity = Vector2.Normalize(NPC.DirectionTo(player.Center)) * maxSpeed * 3f;
                    float angle = NPC.DirectionTo(player.Center).ToRotation() - NPC.velocity.ToRotation();
                    angle = MathHelper.WrapAngle(angle);
                    dashTimer++;
                    if (dashTimer > 60 || (Math.Abs(angle) > Math.PI / 2 && NPC.Distance(player.Center) > 300))
                    {
                        status = 0;
                        dashTimer = 0;
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
            }

        }
        private void findPlayer()
        {
            if (!NPC.HasValidTarget || !player.ZoneUnderworldHeight) NPC.TargetClosest();

            player = Main.player[NPC.target];

            if (!NPC.HasValidTarget || !player.ZoneUnderworldHeight)
            {
                NPC.velocity.Y = 10f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return;
            }
        }

        private void spawn()
        {
            spawned = true;
            NPC.TargetClosest(false);
            player = Main.player[NPC.target];
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                NPC.oldPos[i] = NPC.position;

            if (Main.netMode != NetmodeID.MultiplayerClient) 
            {
                int prev = NPC.whoAmI;
                for (int i = 0; i < SEGMENT_NUMBER; i++)
                {
                    int type = i == SEGMENT_NUMBER - 1 ? ModContent.NPCType<LavaHunterTail>() : ModContent.NPCType<LavaHunterBody>();
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
        private void WormMovement2(Player player, float maxSpeed, float turnSpeed, float accel)
        {
            if (!player.active || player.dead || !player.ZoneUnderworldHeight)
            {
                NPC.TargetClosest(false);
                if (NPC.timeLeft > 30)
                    NPC.timeLeft = 30;
                NPC.velocity.Y += 1f;
                return;
            }
            float rotationDifference = MathHelper.WrapAngle(NPC.velocity.ToRotation() - NPC.DirectionTo(player.Center).ToRotation());
            bool inFrontOfMe = Math.Abs(rotationDifference) < MathHelper.ToRadians(90 / 2);

            if (NPC.Distance(player.Center) > 1200f)
            {
                turnSpeed *= 2f;
                accel *= 2f;

                if (inFrontOfMe && maxSpeed < 30f)
                    maxSpeed = 30f;
            }

            if (NPC.velocity.Length() > maxSpeed)
                NPC.velocity *= 0.975f;

            int num180 = (int)(NPC.position.X / 16f) - 1;
            int num181 = (int)((NPC.position.X + (float)NPC.width) / 16f) + 2;
            int num182 = (int)(NPC.position.Y / 16f) - 1;
            int num183 = (int)((NPC.position.Y + (float)NPC.height) / 16f) + 2;
            if (num180 < 0)
            {
                num180 = 0;
            }
            if (num181 > Main.maxTilesX)
            {
                num181 = Main.maxTilesX;
            }
            if (num182 < 0)
            {
                num182 = 0;
            }
            if (num183 > Main.maxTilesY)
            {
                num183 = Main.maxTilesY;
            }
            bool flag18 = flies;

            if (!flag18)
            {
                for (int num184 = num180; num184 < num181; num184++)
                {
                    for (int num185 = num182; num185 < num183; num185++)
                    {
                        if (Main.tile[num184, num185] != null && (Main.tile[num184, num185].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[num184, num185].TileType] || Main.tileSolidTop[(int)Main.tile[num184, num185].TileType] && Main.tile[num184, num185].TileFrameY == 0) || Main.tile[num184, num185].LiquidAmount > 64))
                        {
                            Vector2 vector17;
                            vector17.X = (float)(num184 * 16);
                            vector17.Y = (float)(num185 * 16);
                            if (NPC.position.X + (float)NPC.width > vector17.X && NPC.position.X < vector17.X + 16f && NPC.position.Y + (float)NPC.height > vector17.Y && NPC.position.Y < vector17.Y + 16f)
                            {
                                flag18 = true;
                                if (Main.rand.NextBool(100) && NPC.behindTiles && Main.tile[num184, num185].HasUnactuatedTile)
                                {
                                    WorldGen.KillTile(num184, num185, true, true, false);
                                }
                                if (Main.netMode != NetmodeID.MultiplayerClient && Main.tile[num184, num185].TileType == 2)
                                {
                                    ushort arg_BFCA_0 = Main.tile[num184, num185 - 1].TileType;
                                }
                            }
                        }
                    }
                }
            }
            if (!flag18)
            {
                Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num186 = 1000;
                bool flag19 = true;
                for (int num187 = 0; num187 < 255; num187++)
                {
                    if (Main.player[num187].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[num187].position.X - num186, (int)Main.player[num187].position.Y - num186, num186 * 2, num186 * 2);
                        if (rectangle.Intersects(rectangle2))
                        {
                            flag19 = false;
                            break;
                        }
                    }
                }
                if (flag19)
                {
                    flag18 = true;
                }
            }
            /*if (directional)
            {
                if (NPC.velocity.X < 0f)
                {
                    NPC.spriteDirection = 1;
                }
                else if (NPC.velocity.X > 0f)
                {
                    NPC.spriteDirection = -1;
                }
            }*/
            float num188 = maxSpeed;
            float num189 = turnSpeed;
            Vector2 vector18 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
            float num191 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
            float num192 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
            num191 = (float)((int)(num191 / 16f) * 16);
            num192 = (float)((int)(num192 / 16f) * 16);
            vector18.X = (float)((int)(vector18.X / 16f) * 16);
            vector18.Y = (float)((int)(vector18.Y / 16f) * 16);
            num191 -= vector18.X;
            num192 -= vector18.Y;
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));

    
            if (!flag18)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > num188)
                {
                    NPC.velocity.Y = num188;
                }
                if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                    }
                }
                else if (NPC.velocity.Y == num188)
                {
                    if (NPC.velocity.X < num191)
                    {
                        NPC.velocity.X = NPC.velocity.X + num189;
                    }
                    else if (NPC.velocity.X > num191)
                    {
                        NPC.velocity.X = NPC.velocity.X - num189;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num189 * 0.9f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - num189 * 0.9f;
                    }
                }
            }
            else
            {
                if (!flies && NPC.behindTiles && NPC.soundDelay == 0)
                {
                    float num195 = num193 / 40f;
                    if (num195 < 10f)
                    {
                        num195 = 10f;
                    }
                    if (num195 > 20f)
                    {
                        num195 = 20f;
                    }
                    NPC.soundDelay = (int)num195;
                }
                num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
                float num196 = System.Math.Abs(num191);
                float num197 = System.Math.Abs(num192);
                float num198 = num188 / num193;
                num191 *= num198;
                num192 *= num198;


                bool flag21 = false;

                if (!flag21)
                {
                    if (NPC.velocity.X > 0f && num191 > 0f || NPC.velocity.X < 0f && num191 < 0f || NPC.velocity.Y > 0f && num192 > 0f || NPC.velocity.Y < 0f && num192 < 0f)
                    {
                        if (NPC.velocity.X < num191)
                        {
                            NPC.velocity.X = NPC.velocity.X + num189;
                        }
                        else
                        {
                            if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - num189;
                            }
                        }
                        if (NPC.velocity.Y < num192)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num189;
                        }
                        else
                        {
                            if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189;
                            }
                        }
                        if ((double)System.Math.Abs(num192) < (double)num188 * 0.2 && (NPC.velocity.X > 0f && num191 < 0f || NPC.velocity.X < 0f && num191 > 0f))
                        {
                            if (NPC.velocity.Y > 0f)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189 * 2f;
                            }
                            else
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189 * 2f;
                            }
                        }
                        if ((double)System.Math.Abs(num191) < (double)num188 * 0.2 && (NPC.velocity.Y > 0f && num192 < 0f || NPC.velocity.Y < 0f && num192 > 0f))
                        {
                            if (NPC.velocity.X > 0f)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189 * 2f;
                            }
                            else
                            {
                                NPC.velocity.X = NPC.velocity.X - num189 * 2f;
                            }
                        }
                    }
                    else
                    {
                        if (num196 > num197)
                        {
                            if (NPC.velocity.X < num191)
                            {
                                NPC.velocity.X = NPC.velocity.X + num189 * 1.1f;
                            }
                            else if (NPC.velocity.X > num191)
                            {
                                NPC.velocity.X = NPC.velocity.X - num189 * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                            {
                                if (NPC.velocity.Y > 0f)
                                {
                                    NPC.velocity.Y = NPC.velocity.Y + num189;
                                }
                                else
                                {
                                    NPC.velocity.Y = NPC.velocity.Y - num189;
                                }
                            }
                        }
                        else
                        {
                            if (NPC.velocity.Y < num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y + num189 * 1.1f;
                            }
                            else if (NPC.velocity.Y > num192)
                            {
                                NPC.velocity.Y = NPC.velocity.Y - num189 * 1.1f;
                            }
                            if ((double)(System.Math.Abs(NPC.velocity.X) + System.Math.Abs(NPC.velocity.Y)) < (double)num188 * 0.5)
                            {
                                if (NPC.velocity.X > 0f)
                                {
                                    NPC.velocity.X = NPC.velocity.X + num189;
                                }
                                else
                                {
                                    NPC.velocity.X = NPC.velocity.X - num189;
                                }
                            }
                        }
                    }
                }
            }
            
        }
        private void WormMovement(Player player, float maxSpeed, float turnSpeed, float accel)
        {
            if (!player.active || player.dead || !player.ZoneUnderworldHeight) 
            {
                NPC.TargetClosest(false);
                if (NPC.timeLeft > 30)
                    NPC.timeLeft = 30;
                NPC.velocity.Y += 1f;
                return;
            }
            float rotationDifference = MathHelper.WrapAngle(NPC.velocity.ToRotation() - NPC.DirectionTo(player.Center).ToRotation());
            bool inFrontOfMe = Math.Abs(rotationDifference) < MathHelper.ToRadians(90 / 2);

            if (NPC.Distance(player.Center) > 1200f)
            {
                turnSpeed *= 2f;
                accel *= 2f;

                if (inFrontOfMe && maxSpeed < 30f)
                    maxSpeed = 30f;
            }

            if (NPC.velocity.Length() > maxSpeed) 
                NPC.velocity *= 0.975f;


            //float num21 = player.Center.X - NPC.Center.X;
            //float num22 = player.Center.Y - NPC.Center.Y;
            //float num23 = (float)Math.Sqrt((double)num21 * (double)num21 + (double)num22 * (double)num22);

            //float NPCToPlyerLength = (float)Math.Sqrt(num21 * num21 + num22 * num22);
            float num3 = Math.Abs(player.Center.X - NPC.Center.X);
            float num4 = Math.Abs(player.Center.Y - NPC.Center.Y);
            float num5 = maxSpeed / (player.Center-NPC.Center).Length();
            float num6 = (player.Center.X - NPC.Center.X) * num5;
            float num7 = (player.Center.Y - NPC.Center.Y) * num5;
            if ((NPC.velocity.X > 0f && num6 > 0f || NPC.velocity.X < 0f && num6 < 0f) && (NPC.velocity.Y > 0f && num7 > 0f || NPC.velocity.Y < 0f && num7 < 0f))
            {
                if (NPC.velocity.X < num6)
                    NPC.velocity.X += accel;
                else if (NPC.velocity.X > num6)
                    NPC.velocity.X -= accel;
                if (NPC.velocity.Y < num7)
                    NPC.velocity.Y += accel;
                else if (NPC.velocity.Y > num7)
                    NPC.velocity.Y -= accel;
            }
            if (NPC.velocity.X > 0f && num6 > 0f || NPC.velocity.X < 0f && num6 < 0f || NPC.velocity.Y > 0f && num7 > 0f || NPC.velocity.Y < 0f && num7 < 0f)
            {
                if (NPC.velocity.X < num6)
                    NPC.velocity.X += turnSpeed;
                else if (NPC.velocity.X > num6)
                    NPC.velocity.X -= turnSpeed;
                if (NPC.velocity.Y < num7)
                    NPC.velocity.Y += turnSpeed;
                else if (NPC.velocity.Y > num7)
                    NPC.velocity.Y -= turnSpeed;

                if (Math.Abs(num7) < maxSpeed * 0.2f && (NPC.velocity.X > 0f && num6 < 0f || NPC.velocity.X < 0f && num6 > 0f))
                {
                    if (NPC.velocity.Y > 0f)
                        NPC.velocity.Y += turnSpeed * 2f;
                    else
                        NPC.velocity.Y -= turnSpeed * 2f;
                }
                if (Math.Abs(num6) < maxSpeed * 0.2f && (NPC.velocity.Y > 0f && num7 < 0f || NPC.velocity.Y < 0f && num7 > 0f))
                {
                    if (NPC.velocity.X > 0f)
                        NPC.velocity.X += turnSpeed * 2f;
                    else
                        NPC.velocity.X -= turnSpeed * 2f;
                }
            }
            else if (num3 > num4)
            {
                if (NPC.velocity.X < num6)
                    NPC.velocity.X += turnSpeed * 1.1f;
                else if (NPC.velocity.X > num6)
                    NPC.velocity.X -= turnSpeed * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < maxSpeed * 0.5f)
                {
                    if (NPC.velocity.Y > 0f)
                        NPC.velocity.Y += turnSpeed;
                    else
                        NPC.velocity.Y -= turnSpeed;
                }
            }
            else
            {
                if (NPC.velocity.Y < num7)
                    NPC.velocity.Y += turnSpeed * 1.1f;
                else if (NPC.velocity.Y > num7)
                    NPC.velocity.Y -= turnSpeed * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < maxSpeed * 0.5f)
                {
                    if (NPC.velocity.X > 0f)
                        NPC.velocity.X += turnSpeed;
                    else
                        NPC.velocity.X -= turnSpeed;
                }
            }
        }




        private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += fastY ? speedModifier * 2 : speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= fastY ? speedModifier * 2 : speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > cap)
                NPC.velocity.X = cap * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > cap)
                NPC.velocity.Y = cap * Math.Sign(NPC.velocity.Y);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<LavaHunterChest>()));

            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MoltenDebris>(), 1, 1, Main.rand.Next(7, 10)));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<LavaCutter>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MiniFireballGun>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HellFlameEjector>(), 3));
            notExpertRule.OnSuccess(new CommonDrop(ModContent.ItemType<LoudLavaFlow>(), 100, 1, 1, 7));
            npcLoot.Add(notExpertRule);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return null;
        }

    }
}
