using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Boss;
using EmptySet.Projectiles.Flails;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.NPCs.Boss.腐化水晶
{
    [AutoloadBossHead]
    internal class 腐化水晶 : ModNPC
    {
        Player player;
        int state = ATTACK_STATE_MOVE;
        const int ATTACK_STATE_MOVE = 0;
        const int ATTACK_STATE_TELEPORT = 1;
        const int ATTACK_STATE_LASER = 2;
        const int ATTACK_STATE_SICKLE = 3;
        bool particleLeft = true;
        bool dashLeft = true;
        int laserType = 0;
        int laserIndex;
        private int laserTime=0;
        private int sickleTime = 0;
        Vector2 targetPos = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("腐化水晶");

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers(0){});
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
                new FlavorTextBestiaryInfoElement("腐化水晶")
            });
        }

        public override void SetDefaults()
        {
            NPC.width = 66;
            NPC.height = 120;
            NPC.damage = 50;
            NPC.defense = 30;
            NPC.lifeMax = 16000;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(0, Main.rand.Next(4, 8));
            NPC.npcSlots = 10f;//占据npc槽位，阻止小怪刷出

            NPC.boss = true;
            //NPC.behindTiles = true;
            NPC.trapImmune = true;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TestMusic");
            }
        }

        private int laserDamage = 55;
        private int sickleDamage = 30;//镰刀伤害未指定

        public override bool PreAI()
        {
            NPC.lifeMax = Main.masterMode ? 42000 : Main.expertMode ? 32000 : 16000;
            NPC.defense = Main.masterMode ? 32 : Main.expertMode ? 32 : 32;
            NPC.damage = Main.masterMode ? 120 : Main.expertMode ? 90 : 50;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
            laserDamage = Main.masterMode ? 135 : Main.expertMode ? 95 : 55;
            sickleDamage = Main.masterMode ? 70 : Main.expertMode ? 45 : 30;
            return base.PreAI();
        }

        public override void AI()
        {
            base.AI();
            NPC.localAI[0]++;
            Main.dust[Dust.NewDust(NPC.position, 62, 124, DustID.Shadowflame)].noGravity = true;
            if (!findPlayer()) return;
            switch (state) 
            {
                case ATTACK_STATE_MOVE:
                    move(player.Center + new Vector2(0, -150), 0.03f);
                    if (NPC.localAI[0] >= 42) 
                    {
                        state = Main.rand.Next(1,4);
                        particleLeft = Main.rand.NextBool();
                        laserType = Main.rand.Next(0, 2);
                        NPC.velocity = Vector2.Zero;
                        NPC.localAI[0] = 0;
                    } 
                    break;
                case ATTACK_STATE_TELEPORT:
                    if (NPC.localAI[0] <= 180)
                    {
                        for (int i = 0; i < 5; i++)
                            Main.dust[Dust.NewDust(player.Center + new Vector2(15 * 16 * (particleLeft ? -1 : 1), 0), 100, 100, DustID.Shadowflame)].fadeIn = 0.05f;
                    }
                    else if (NPC.localAI[0] == 181)
                    {
                        NPC.Teleport(player.Center + new Vector2(15 * 16 * (particleLeft ? -1 : 1), 0), TeleportationStyleID.RodOfDiscord);
                    }
                    else if (NPC.localAI[0] == 211)
                    {
                        NPC.velocity = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 20;
                    }
                    else if (NPC.localAI[0] == 211 + 20)
                    {
                        NPC.velocity.X = 0;
                        NPC.velocity.Y = 5;
                    }
                    else if (NPC.localAI[0] == 211 + 20 + 120) 
                    {
                        NPC.velocity = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 20;
                    }
                    else if (NPC.localAI[0] >= 211 + 20 + 121 && NPC.localAI[0] < 211 + 20 + 120 + 50)
                    {
                        if (NPC.localAI[0] % 10 == 0)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 10, ModContent.ProjectileType<DarkCrystal>(), EmptySetUtils.ScaledProjDamage(sickleDamage), 0, player.whoAmI);
                    }
                    else if (NPC.localAI[0] == 211 + 20 + 120 + 50)
                    {
                        state = ATTACK_STATE_MOVE;
                        NPC.localAI[0] = 0;
                    }
                    break;
                case ATTACK_STATE_LASER:
                    if (laserType == 0) 
                    {
                        if (NPC.localAI[0] <= 180)
                        {
                            targetPos = player.Center + new Vector2(0, -160);
                            move(targetPos,0.06f);
                            if (Vector2.Distance(targetPos, NPC.Center) <= 40) NPC.localAI[0] = 180;
                        }
                        else if (NPC.localAI[0] == 181)
                        {
                            NPC.Center = player.Center + new Vector2(0, -160);
                            NPC.velocity = Vector2.Zero;
                        }
                        else if (NPC.localAI[0] == 201)
                        {
                            laserIndex = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitY, ModContent.ProjectileType<腐化水晶激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0, player.whoAmI);
                            Main.projectile[laserIndex].ai[1] = NPC.whoAmI;
                            Main.projectile[laserIndex].timeLeft = 60;
                        }
                        else if (NPC.localAI[0] == 201 + 60)
                        {
                            NPC.localAI[0] = 0;
                            laserTime++;
                            laserType = Main.rand.Next(0, 2);
                            Main.projectile[laserIndex].Kill();
                        }
                    }
                    else 
                    {
                        if (NPC.localAI[0] <= 180) 
                        {
                            if (NPC.localAI[0] == 1) 
                            {
                                dashLeft = Main.rand.NextBool();
                                //targetPos = player.Center + new Vector2(player.velocity.X < 0 ? -320 : 320, -160);
                                targetPos = player.Center + new Vector2(dashLeft ? -320 : 320, -160);
                            }
                            move(targetPos,0.08f);
                            if (Vector2.Distance(targetPos, NPC.Center) <= 40)  NPC.localAI[0] = 180;
                        }
                        else if (NPC.localAI[0] == 181)
                        {
                            //NPC.velocity = ((player.Center.X - NPC.Center.X) > 0 ? 1 : -1) * Vector2.UnitX * 5;
                            NPC.velocity = (dashLeft ? 1 : -1) * Vector2.UnitX * 5;
                            laserIndex = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitY, ModContent.ProjectileType<腐化水晶激光>(), EmptySetUtils.ScaledProjDamage(laserDamage), 0, player.whoAmI);
                            Main.projectile[laserIndex].ai[1] = NPC.whoAmI;
                            Main.projectile[laserIndex].timeLeft = 240;
                        }
                        else if (NPC.localAI[0] == 181 + 240)
                        {
                            NPC.localAI[0] = 0;
                            laserTime++;
                            laserType = Main.rand.Next(0, 2);
                            Main.projectile[laserIndex].Kill();
                        }
                    }
                    if (laserTime >= 7) 
                    {
                        state = ATTACK_STATE_MOVE;
                        laserTime = 0;
                    }
                    break;
                case ATTACK_STATE_SICKLE:
                    if (NPC.localAI[0] == 1)
                    {
                        SoundEngine.PlaySound(SoundID.Item4, NPC.Center);
                        NPC.velocity = Vector2.Zero;
                    }
                    else if (NPC.localAI[0] >= 121) 
                    {
                        if ((NPC.localAI[0] - 120) % 20 == 0) 
                        {
                            for (int i = 0; i < 12; i++) 
                            {
                                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(i * MathHelper.Pi / 6), ProjectileID.DemonSickle, EmptySetUtils.ScaledProjDamage(sickleDamage), 0, player.whoAmI);
                            }
                            sickleTime++;
                        }
                        if (sickleTime >= 7) 
                        {
                            sickleTime = 0;
                            state = ATTACK_STATE_MOVE;
                            NPC.localAI[0] = 0;
                        }
                    }
                    break;
            }
            Visuals();
        }

        public override void OnKill()
        {
            base.OnKill();
            if (laserIndex < Main.maxProjectiles && Main.projectile[laserIndex].type == ModContent.ProjectileType<LaserProjectile>() && Main.projectile[laserIndex].active)
                Main.projectile[laserIndex].Kill();
        }

        private void move(Vector2 pos,float speed)
        {
            NPC.velocity = Vector2.Distance(pos, NPC.Center) <= 5 ?
                Vector2.Zero :
                (pos - NPC.Center).SafeNormalize(Vector2.UnitX) * MathHelper.Max(Vector2.Distance(pos, NPC.Center), 175) * speed;
        }

        private bool findPlayer()
        {
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 2500f || !NPC.HasValidTarget || !player.ZoneCorrupt) NPC.TargetClosest();
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 2500f || !NPC.HasValidTarget || !player.ZoneCorrupt)
            {
                NPC.velocity.Y = 10f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return false;
            }
            return true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            IItemDropRule master = new LeadingConditionRule(new Conditions.IsMasterMode());
            master.OnSuccess(ItemDrop.GetItemDropRule<CorruptShard>(100, 12, 15));
            master.OnSuccess(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 20, 29));

            IItemDropRule noMaster = new LeadingConditionRule(new Conditions.NotMasterMode());
            noMaster.OnSuccess(ItemDrop.GetItemDropRule<CorruptShard>(100, 5, 7));
            noMaster.OnSuccess(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 10, 19));
            
            npcLoot.Add(master);
            npcLoot.Add(noMaster);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return null;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Main.instance.LoadProjectile(NPC.type);
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;

            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, NPC.height * 0.5f);
            for (int k = 0; k < NPC.oldPos.Length; k++)
            {
                Vector2 drawPos = (NPC.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, NPC.gfxOffY);
                Color color = NPC.GetAlpha(drawColor) * ((NPC.oldPos.Length - k) / (float)NPC.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, NPC.rotation, drawOrigin, NPC.scale, SpriteEffects.None, 0);
            }
            return true;
        }
        //视觉效果
        private void Visuals()
        {
            NPC.rotation = NPC.velocity.X * 0.01f;
            Lighting.AddLight(NPC.Center, Color.White.ToVector3() * 0.78f);
        }
    }
}
