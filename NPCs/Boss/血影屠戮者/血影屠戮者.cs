using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Boss;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.NPCs.Boss.血影屠戮者
{
    [AutoloadBossHead]
    internal class 血影屠戮者 : ModNPC
    {
        Player player;
        int state = ATTACK_STATE_MOVE;
        const int ATTACK_STATE_MOVE = 0;
        const int ATTACK_STATE1 = 1;
        const int ATTACK_STATE2 = 2;
        const int ATTACK_STATE3 = 3;
        bool dashLeft = true;
        private int bloodTime = 5;
        Vector2 targetPos = Vector2.Zero;
        bool changeState = false;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("血影屠戮者");

            NPCID.Sets.MPAllowedEnemies[Type] = true;

            NPCID.Sets.TrailCacheLength[NPC.type] = 5;
            NPCID.Sets.TrailingMode[NPC.type] = 0;

            NPCID.Sets.BossBestiaryPriority.Add(NPC.type);
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers(0) { });
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
                new FlavorTextBestiaryInfoElement("血影屠戮者")
            });
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.velocity.ToRotation();
        }
        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 66;
            NPC.damage = 65;
            NPC.defense = 27;
            NPC.lifeMax = 15000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1;
            NPC.value = Item.buyPrice(0, Main.rand.Next(4, 8));

            NPC.boss = true;
            NPC.trapImmune = true;

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/RequiemOfScourge");
            }
        }

        private int bloodDamage = 55;

        public override bool PreAI()
        {
            NPC.lifeMax = Main.masterMode ? 32000 : Main.expertMode ? 25700 : 15000;
            NPC.defense = Main.masterMode ? 27 : Main.expertMode ? 27 : 27;
            NPC.damage = Main.masterMode ? 145 : Main.expertMode ? 105 : 65;
            if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
            bloodDamage = Main.masterMode ? 60 : Main.expertMode ? 45 : 30;
            return base.PreAI();
        }

        public override void AI()
        {
            base.AI();
            changeState = false;
            NPC.localAI[0]++;
            Main.dust[Dust.NewDust(NPC.position, 148, 86, DustID.Blood)].noGravity = true;
            if (!findPlayer()) return;
            switch (state) 
            {
                case ATTACK_STATE_MOVE:
                    move(player.Center, 0.03f);
                    if (NPC.localAI[0] > 30) 
                    {
                        NPC.localAI[0] = 0;
                        state = Main.rand.Next(1,4);
                        dashLeft = Main.rand.NextBool();
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
                case ATTACK_STATE1:
                    if (NPC.localAI[0] < 20) 
                    {
                        move(player.Center, 0.01f);
                    }
                    else if (NPC.localAI[0] == 20)
                    {
                        SoundEngine.PlaySound(SoundID.ForceRoar, player.position);
                        NPC.velocity = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 20f;
                    }
                    else if (NPC.localAI[0] == 51+20)
                    {
                        NPC.velocity = Vector2.Zero;
                        NPC.rotation = (player.Center - NPC.Center).ToRotation();
                        for (int i = 0; i < 8; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(i * MathHelper.PiOver4)*8, ModContent.ProjectileType<血弹>(), EmptySetUtils.ScaledProjDamage(bloodDamage), 0, player.whoAmI);
                    }
                    else if (NPC.localAI[0] == 60+20)
                    {
                        bloodTime--;
                        NPC.localAI[0] = 0;
                        if (bloodTime <= 0) 
                        {
                            state = ATTACK_STATE_MOVE;
                            bloodTime = Main.rand.Next(5, 8);
                        }
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
                case ATTACK_STATE2:
                    targetPos = player.Center + new Vector2(dashLeft ? -320 : 320, 0);
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.localAI[0] <= 60)
                    {
                        move(targetPos, 0.1f);
                        if (Vector2.Distance(targetPos, NPC.Center) <= 40) NPC.localAI[0] = 60;
                        changeState = true;
                    }
                    else if (NPC.localAI[0] <= 61 + 270)
                    {
                        move2(targetPos, 8, 5);
                        NPC.rotation = (player.Center - NPC.Center).ToRotation();
                        if ((NPC.localAI[0] - 61) % 30 == 0)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, (dashLeft ? 1 : -1) * Vector2.UnitX * 8, ModContent.ProjectileType<血弹>(), EmptySetUtils.ScaledProjDamage(bloodDamage), 0, player.whoAmI);
                    }
                    else if (NPC.localAI[0] == 61 + 271)
                    {
                        NPC.velocity = (dashLeft ? 1 : -1) * Vector2.UnitX * 20f;
                    }
                    else if (NPC.localAI[0] == 330 + 60)
                    {
                        NPC.velocity = Vector2.Zero;
                    }
                    else if (NPC.localAI[0] == 330 + 70)
                    {
                        NPC.velocity = (player.Center - NPC.Center).SafeNormalize(Vector2.UnitX) * 20f;
                    }
                    else if (NPC.localAI[0] >= 330 + 130) 
                    {
                        for (int i = 0; i < 18; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(i * MathHelper.Pi / 9) * 8, ModContent.ProjectileType<血弹>(), EmptySetUtils.ScaledProjDamage(bloodDamage), 0, player.whoAmI);
                        NPC.localAI[0] = 0;
                        state = ATTACK_STATE_MOVE;
                    }
                    break;
                case ATTACK_STATE3:
                    targetPos = player.Center + new Vector2(0, -25*16);
                    if (NPC.localAI[0] <= 60)
                    {
                        move(targetPos, 0.1f);
                        if (Vector2.Distance(targetPos, NPC.Center) <= 40) NPC.localAI[0] = 60;
                    }
                    else if (NPC.localAI[0] == 61)
                    {
                        NPC.velocity = Vector2.Zero;
                        for (int i = 0; i < 12; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(i * MathHelper.Pi / 6) * 8, ModContent.ProjectileType<血弹>(), EmptySetUtils.ScaledProjDamage(bloodDamage), 0, player.whoAmI);
                    }
                    else if (NPC.localAI[0] == 71)
                    {
                        NPC.velocity = Vector2.UnitY * 20;
                    }
                    else if (NPC.localAI[0] == 71 + 40)
                    {
                        NPC.velocity = Vector2.Zero;
                        for (int i = 0; i < 12; i++)
                            Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.UnitX.RotatedBy(i * MathHelper.Pi / 6) * 8, ModContent.ProjectileType<血弹>(), EmptySetUtils.ScaledProjDamage(bloodDamage), 0, player.whoAmI);
                    }
                    else if (NPC.localAI[0] == 71 + 50) 
                    {
                        NPC.localAI[0] = 0;
                        state = ATTACK_STATE_MOVE;
                    }
                    NPC.rotation = NPC.velocity.ToRotation();
                    break;
            }
        }

        private void move2(Vector2 pos, float speed,float inertia)
        {
            Vector2 direction = pos - NPC.Center;
            direction.Normalize();
            direction *= speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;
        }
        private void move(Vector2 pos, float speed)
        {
            NPC.velocity = Vector2.Distance(pos, NPC.Center) <= 40 ?
                Vector2.Zero :
                (pos - NPC.Center).SafeNormalize(Vector2.UnitX) * MathHelper.Max(Vector2.Distance(pos, NPC.Center), 175) * speed;
        }

        private bool findPlayer()
        {
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 2500f || !NPC.HasValidTarget || !player.ZoneCrimson) NPC.TargetClosest();
            player = Main.player[NPC.target];
            if (!player.active || player.dead || Vector2.Distance(NPC.Center, player.Center) > 2500f || !NPC.HasValidTarget || !player.ZoneCrimson)
            {
                NPC.velocity.Y = -10f;
                if (NPC.timeLeft > 60)
                    NPC.timeLeft = 60;
                return false;
            }
            return true;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            IItemDropRule master = new LeadingConditionRule(new Conditions.IsMasterMode());
            master.OnSuccess(ItemDrop.GetItemDropRule<BloodShadow>(100, 12, 15));
            master.OnSuccess(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 20, 29));

            IItemDropRule noMaster = new LeadingConditionRule(new Conditions.NotMasterMode());
            noMaster.OnSuccess(ItemDrop.GetItemDropRule<BloodShadow>(100, 5, 7));
            noMaster.OnSuccess(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 10, 19));

            npcLoot.Add(master);
            npcLoot.Add(noMaster);
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return changeState ? false : base.CanHitPlayer(target, ref cooldownSlot);
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
    }
}
