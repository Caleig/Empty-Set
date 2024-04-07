using EmptySet.Common.ItemDropRules.DeepGumPolymer;
using EmptySet.Items.Accessories;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using EmptySet.Utils;
using EmptySet.Biomes;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace EmptySet.NPCs.Enemy;

/// <summary>
/// 钨史莱姆
/// </summary>
public class TungstenSlime : ModNPC
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Tungsten Slime");
        
        Main.npcFrameCount[Type] = 4;

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Velocity = 1f 
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        DiffSelector();
        NPC.width = 46;
        NPC.height = 30;

        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.value = 60f;
        //NPC.noGravity = true;
        NPC.aiStyle = 1; // 战斗AI，重要的是选择匹配我们想要模仿的NPCID的aiStyle
        AIType = NPCID.None; // 执行AI代码时使用香草僵尸类型。(这也意味着它会尝试在白天消失)
        AnimationType = NPCID.CorruptSlime; // 执行动画代码时使用香草僵尸类型。重要的是匹配Main.npcFrameCount[NPC.type]在SetStaticDefaults。
    }

    private void DiffSelector()
    {
        NPC.lifeMax = EmptySetUtils.ScaledNPCMaxLife(Main.masterMode ? 500 : Main.expertMode ? 400 : 350);
        NPC.defense = 6; //Main.masterMode ? 6 : Main.expertMode ? 6 : 12;
        NPC.knockBackResist = 0.95f;//Main.masterMode ? 0f : Main.expertMode ? 0f : 0f;
        NPC.damage = EmptySetUtils.ScaledNPCDamage(Main.masterMode ? 47 : Main.expertMode ? 31 : 23);
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDrop.GetItemDropRule(ItemID.TungstenOre, 100, 4, 12));
        npcLoot.Add(ItemDrop.GetItemDropRule(ItemID.SilverCoin, 100, 10, 35));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        //地牢 地狱
        if (!(spawnInfo.Player.ZoneDungeon && spawnInfo.Player.ZoneUnderworldHeight))
        {
            if (spawnInfo.Player.ZoneSkyHeight) return SpawnCondition.Sky.Chance * 0.09f;
            if (spawnInfo.Player.ZoneOverworldHeight) return SpawnCondition.Overworld.Chance * 0.09f;
            if (spawnInfo.Player.ZoneNormalUnderground) return SpawnCondition.Underground.Chance * 0.09f;
            if (spawnInfo.Player.ZoneNormalCaverns) return SpawnCondition.Cavern.Chance * 0.09f;
        }
        return 0;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
            new FlavorTextBestiaryInfoElement("一种以钨矿为食的史莱姆.")
        });
    }
}