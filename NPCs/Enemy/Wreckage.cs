using EmptySet.Utils;
using EmptySet.Items.Materials;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy;

public class Wreckage : ModNPC
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("亡骸");

        Main.npcFrameCount[Type] = 4;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);

    }
    public override void SetDefaults()
    {
        NPC.width = 52;
        NPC.height = 63;
        NPC.damage = 70;
        NPC.defense = 18;
        NPC.lifeMax = 1400;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 60f;
        NPC.knockBackResist = 0f;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.aiStyle = 22;
        AnimationType = 6;
    }
    public override bool PreAI()
    {
        NPC.lifeMax = Main.masterMode ? 4000 : Main.expertMode ? 2800 : 1400;
        NPC.defense = 18;// Main.masterMode ? 18 : Main.expertMode ? 18 : 18;
        NPC.damage = Main.masterMode ? 170 : Main.expertMode ? 120 : 70;
        if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
        NPC.direction = (int)NPC.velocity.X ;
        NPC.spriteDirection = NPC.velocity.X > 0 ? 1 : -1;
        return base.PreAI();
    }
    public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.hardMode && Main.bloodMoon ? SpawnCondition.OverworldNightMonster.Chance * 0.15f : 0;
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        //MechanicalBossesDummyCondition 不论什么情况都掉落
        IItemDropRule dropAll = new LeadingConditionRule(new Conditions.MechanicalBossesDummyCondition());
        dropAll.OnSuccess(ItemDrop.GetItemDropRule<BloodWreckage>(35, 1, 2));
        dropAll.OnSuccess(ItemDrop.GetItemDropRule<DarkBrokenCrystal>(45, 3, 5));
        dropAll.OnSuccess(ItemDrop.GetItemDropRule(ItemID.SilverCoin, 100, 30, 40));
        npcLoot.Add(dropAll);
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Events.BloodMoon,
                new FlavorTextBestiaryInfoElement("这TM的是个啥玩意?!")
            });
    }
}