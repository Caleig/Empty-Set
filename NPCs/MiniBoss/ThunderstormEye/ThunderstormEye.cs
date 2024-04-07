using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Boss;
using EmptySet.Utils;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.MiniBoss.ThunderstormEye
{
    class ThunderstormEye:ModNPC
    {
        Player player;
        private int secondStageHeadSlot =-1;
        int laserTimer=0;
        int stateTimer = 0;
        int state = 0;
        Dust dust;

        const int ATTACK1 = 0;
        const int ATTACK2 = 1;
        const int STOP = 2;
        const int DASH = 3;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("雷暴之眼");
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.MPAllowedEnemies[Type] = true;

            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "EmptySet/NPCs/MiniBoss/ThunderstormEye/ThunderstormEye",
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 800;
            NPC.damage = 45;
            NPC.defense = 4;
            NPC.knockBackResist = -1f;
            NPC.width = 98;
            NPC.height = 98;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.noGravity = true;
            NPC.value = 5;
            NPC.noTileCollide = true;
            NPC.aiStyle = -1;
        }
        int laserDamage;
        float speed;
        public override bool PreAI()
        {
            NPC.lifeMax = Main.masterMode ? 2600 : Main.expertMode ? 2200 : 1800;
            NPC.defense = Main.masterMode ? 4 : Main.expertMode ? 4 : 4;
            NPC.damage = Main.masterMode ? 60 : Main.expertMode ? 45 : 30;
            laserDamage = Main.masterMode ? 45 : Main.expertMode ? 35 : 25;
            speed = Main.masterMode ? 0.03f : Main.expertMode ? 0.03f : 0.02f;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
            return base.PreAI();
        }
        public override void AI()
        {
            stateTimer++;
            findPlayer();
            switch (state) 
            {
                case ATTACK1:
                    if (stateTimer > 120) 
                    {
                        laser();
                    }
                    move();
                    break;
                case ATTACK2:
                    if (stateTimer > 120) 
                    {
                        laser2();
                        stateTimer = 0;

                        state = Main.expertMode ? STOP : ATTACK1;
                    } 
                    move();
                    break;
                case STOP:
                    if (stateTimer > 180)
                    {
                        stateTimer = 0;
                        state = DASH;
                    }
                    else if (stateTimer > 120)
                    {
                        NPC.velocity = Vector2.Zero;
                        NPC.rotation += 0.1f;
                    }
                    else 
                    {
                        move();
                    }
                    break;
                case DASH:
                    if (stateTimer == 1) 
                    {
                        NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * 50 * 16 / 50;
                    }
                    else if (stateTimer > 15)
                    {
                        stateTimer = 0;
                        state = ATTACK1;
                    }
                    break;
            }
        }

        private void laser2()
        {
            for (int i = 0; i < 12; i++) 
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(MathHelper.ToRadians(360 / 12) * i) * 10, ModContent.ProjectileType<雷暴之眼激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0, player.whoAmI);
            }
            SoundEngine.PlaySound(SoundID.Item33, NPC.position);
        }

        private void laser()
        {
            laserTimer++;
            if (laserTimer == 1) 
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center,(player.Center-NPC.Center).SafeNormalize(Vector2.UnitX)*10,ModContent.ProjectileType<雷暴之眼激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0,player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item33, NPC.position); //激光声
            }
            if (laserTimer == 11) 
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 10, ModContent.ProjectileType<雷暴之眼激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item33, NPC.position); //激光声
            }
            if (laserTimer == 21) 
            {
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 10, ModContent.ProjectileType<雷暴之眼激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item33, NPC.position); //激光声
                laserTimer = 0;
                ///
                stateTimer = 0;
                state = ATTACK2;
            }
        }

        private void move()
        {
            NPC.rotation += 0.05f;
            NPC.spriteDirection = NPC.direction;
            NPC.velocity = Vector2.Distance(player.Center, NPC.Center) <= 3 ? Vector2.Zero : Vector2.Normalize(player.Center - NPC.Center) * MathHelper.Min(MathHelper.Max(Vector2.Distance(player.Center, NPC.Center), 175), 500) * speed;
            if (Main.rand.NextBool(5)) 
            {
                dust = Main.dust[Dust.NewDust(NPC.position, 98, 98, DustID.BlueTorch)];
                dust.scale = 1.5f;
                dust.noGravity = true;
            }
        }

        private void findPlayer()
        {
            if (!NPC.HasValidTarget) NPC.TargetClosest();

            player = Main.player[NPC.target];

            if (!NPC.HasValidTarget)
            {
                NPC.velocity.Y = -10f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return;
            }
        }

        public override void Load()
        {
            string texture = BossHeadTexture;
            secondStageHeadSlot = Mod.AddBossHeadTexture(texture, -1);
        }
        public override void BossHeadSlot(ref int index)
        {
            if (secondStageHeadSlot != -1)
            {
                index = secondStageHeadSlot;
            }
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneOverworldHeight&&NPC.downedBoss1&& !NPC.AnyNPCs(ModContent.NPCType<ThunderstormEye>()))
            {
                return SpawnCondition.OverworldDayRain.Chance * 0.09f;
            }
            return 0;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
                new FlavorTextBestiaryInfoElement("众多来着域外的访客之一，通过吸收少量雷暴的能量，拥有了发射雷电的能力.")
            });
        }
        public override void OnKill()
        {
            
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            if (NPC.life <= 0)
            {

            }
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            Random ramdom = new Random();
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RaySoul>(), 1, 1, ramdom.Next(10, 16)));
        }

        //public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        //{
        //    spriteBatch.Draw((Texture2D)TextureAssets.Npc[NPC.type], screenPos, new Rectangle(0, 0, 86, 86), drawColor, rotation, new Vector2(0, 0), 1, 0, 0);
        //    return false;
        //}
    }
}
