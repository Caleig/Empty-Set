using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.NPCs.Boss.FrozenCore;
using System;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy;

public class 极川灵: ModNPC
{
    public override void SetStaticDefaults()
    {
        
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Velocity = 1f
        };
        NPCID.Sets.MPAllowedEnemies[Type] = true;
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.IceElemental);
        init();
        NPC.width = 46;
        NPC.height = 46;
        NPC.value = 60f;
        NPC.rarity = 4;
    }
    public void init()
    {
        NPC.lifeMax = Main.masterMode ? 1100 : Main.expertMode ? 600 : 400;
        NPC.defense = Main.masterMode ? 30 : Main.expertMode ? 30 : 30;
        NPC.damage = Main.masterMode ? 185 : Main.expertMode ? 130 : 70;
        NPC.knockBackResist = Main.masterMode ? 0.05f : Main.expertMode ? 0.05f : 0.05f;
    }
    // 原版冰雪精AI
    public override void AI()
    {
        //NPC.position += netOffset;
        Lighting.AddLight((int)((NPC.position.X + (float)(NPC.width / 2)) / 16f), (int)((NPC.position.Y + (float)(NPC.height / 2)) / 16f), 0f, 0.6f, 0.75f);
        NPC.alpha = 30;
        if (Main.rand.Next(3) == 0)
        {
            int num310 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 92, 0f, 0f, 200);
            Dust dust = Main.dust[num310];
            dust.velocity *= 0.3f;
            Main.dust[num310].noGravity = true;
        }

        //NPC.position -= netOffset;
        if (NPC.justHit)
        {
            NPC.ai[3] = 0f;
            NPC.localAI[1] = 0f;
        }

        float num311 = 5f;
        Vector2 vector31 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
        float num312 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2) - vector31.X;
        float num313 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2) - vector31.Y;
        float num314 = (float)Math.Sqrt(num312 * num312 + num313 * num313);
        float num315 = num314;
        num314 = num311 / num314;
        num312 *= num314;
        num313 *= num314;
        if (num312 > 0f)
            NPC.direction = 1;
        else
            NPC.direction = -1;

        NPC.spriteDirection = NPC.direction;
        if (NPC.direction < 0)
            NPC.rotation = (float)Math.Atan2(0f - num313, 0f - num312);
        else
            NPC.rotation = (float)Math.Atan2(num313, num312);

        if (Main.netMode != 1 && NPC.ai[3] == 16f)
        {
            int num316 = 45;
            int num317 = 128;
            int num318 = Projectile.NewProjectile(NPC.GetSource_FromAI(), vector31.X, vector31.Y, num312, num313, num317, num316, 0f, Main.myPlayer);
        }
        if (NPC.ai[3] > 0f)
        {
            NPC.ai[3] += 1f;
            if (NPC.ai[3] >= 64f)
                NPC.ai[3] = 0f;
        }

        if (Main.netMode != 1 && NPC.ai[3] == 0f)
        {
            NPC.localAI[1] += 1f;
            if (NPC.localAI[1] > 120f && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                NPC.localAI[1] = 0f;
                NPC.ai[3] = 1f;
                NPC.netUpdate = true;
            }
        }
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(new CommonDrop(ItemID.FrostCore, 1));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneSnow && Main.raining && Main.hardMode && !NPC.AnyNPCs(ModContent.NPCType<极川灵>())) 
        {
            return SpawnCondition.Overworld.Chance * 0.11f;
        }
        return 0;
    }

    public override void SetBestiary(BestiaryDatabase dataNPC, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.Rain,
            new FlavorTextBestiaryInfoElement("似乎是冰雪精的变种，但总感觉哪里不对")
        });
    }
    //经测试现版本多人联机时onkill只会在服务器端运行
    public override void OnKill()
    {
        int type = ModContent.NPCType<FrozenCore>();
        if (Main.netMode == NetmodeID.SinglePlayer)
        {
            NPC.SpawnBoss((int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.target);
        }
        else if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            ModPacket packet = Mod.GetPacket();
            packet.Write((int)EmptySetMessageType.SpawnBossOnPoint);
            packet.Write((int)NPC.Center.X);
            packet.Write((int)NPC.Center.Y);
            packet.Write((int)type);
            packet.Write((int)NPC.target);
            packet.Send(-1, -1);
        }
        else if (Main.netMode == NetmodeID.Server) 
        {
            NPC.SpawnBoss((int)NPC.Center.X, (int)NPC.Center.Y, type, NPC.target);
        }
        base.OnKill();
    }
}