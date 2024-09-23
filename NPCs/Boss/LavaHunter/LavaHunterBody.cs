using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.NPCs.Boss.LavaHunter;

public class LavaHunterBody : ModNPC
{
    NPC segment;
    NPC head;
    int timer=0;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("熔岩游猎者");

        NPCID.Sets.TrailCacheLength[NPC.type] = 5;
        NPCID.Sets.TrailingMode[NPC.type] = 1;
        NPCID.Sets.CantTakeLunchMoney[Type] = true;

        NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Hide = true
        });
    }

    public override void SetDefaults()
    {
        NPC.width = 106;
        NPC.height = 98;
        NPC.damage = 95;
        NPC.defense = 30;
        NPC.lifeMax = 36000;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.knockBackResist = 0f;
        NPC.lavaImmune = true;
        NPC.aiStyle = -1;

        NPC.behindTiles = true;

        //NPC.scale *= 1.25f;
        NPC.trapImmune = true;
        NPC.dontCountMe = true;
    }
    int npcDamage = 0;
    public override bool PreAI()
    {
        NPC.defense = Main.masterMode ? 30 : Main.expertMode ? 25 : 20;
        NPC.damage = Main.masterMode ? 95 : Main.expertMode ? 75 : 55;
        npcDamage = Main.masterMode ? 80 : Main.expertMode ? 65 : 50;
        return base.PreAI();
    }
    public override void AI()
    {
        segment = null;
        head = null;
        if (NPC.ai[1] > -1 && NPC.ai[1] < Main.maxNPCs && Main.npc[(int)NPC.ai[1]].active &&(ModContent.NPCType<LavaHunter>() == Main.npc[(int)NPC.ai[1]].type|| ModContent.NPCType<LavaHunterBody>() == Main.npc[(int)NPC.ai[1]].type)) 
        {
            segment = Main.npc[(int)NPC.ai[1]];
        }
        if (NPC.ai[3] > -1 && NPC.ai[3] < Main.maxNPCs && Main.npc[(int)NPC.ai[3]].active && ModContent.NPCType<LavaHunter>() == Main.npc[(int)NPC.ai[3]].type)
        {
            head = Main.npc[(int)NPC.ai[3]];
        }
        if (head == null) 
        {
            NPC.life = 0;
            return;
        }
        NPC.velocity = Vector2.Zero;

        int pastPos = NPCID.Sets.TrailCacheLength[NPC.type] - (int)head.ai[3] - 1; 

        if (NPC.localAI[0] == 0)
        {
            NPC.localAI[0] = 1;
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[NPC.type]; i++)
                NPC.oldPos[i] = NPC.position;
        }

        /*NPC.Center = segment.oldPos[pastPos] + segment.Size / 2;
        NPC.rotation = NPC.DirectionTo(segment.Center).ToRotation();
        if (NPC.Distance(NPC.oldPos[pastPos - 1] + NPC.Size / 2) > 45 * NPC.scale)
        {
            NPC.oldPos[pastPos - 1] = NPC.position + Vector2.Normalize(NPC.oldPos[pastPos - 1] - NPC.position) * 45 * NPC.scale;
        }*/

        var npcToPlayerX = segment.Center.X - NPC.Center.X;
        var npcToPlayerY = segment.Center.Y - NPC.Center.Y;
        NPC.rotation = (float)Math.Atan2(npcToPlayerY, npcToPlayerX);
        var normOfNpcToPlayer = (float)Math.Sqrt(npcToPlayerX * npcToPlayerX + npcToPlayerY * npcToPlayerY);
        normOfNpcToPlayer = NPC.type == ModContent.NPCType<LavaHunterTail>()
            ? (normOfNpcToPlayer - NPC.width + 15) / normOfNpcToPlayer
            : (normOfNpcToPlayer - NPC.width + 15) / normOfNpcToPlayer;
        npcToPlayerX *= normOfNpcToPlayer;
        npcToPlayerY *= normOfNpcToPlayer;
        NPC.velocity = Vector2.Zero;
        NPC.position.X += npcToPlayerX;
        NPC.position.Y += npcToPlayerY;

        NPC.timeLeft = segment.timeLeft;

        if (head.life <= head.lifeMax * 0.90)
        {
            if (timer == 1) 
            {
                int npc = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.BurningSphere);
                Main.npc[npc].velocity= Main.player[head.target].position - NPC.position;
                Main.npc[npc].damage = npcDamage;
            }
            if (timer >= 60*5)timer = 0;
            timer++;
        }
    }

    public override bool CheckActive()
    {
        return false;
    }
    public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
    {
        return false;
    }
}
