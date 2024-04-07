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

namespace EmptySet.NPCs.Enemy
{
    class AbyssGazer:ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("渊视者");

			Main.npcFrameCount[Type] = 4;

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f 
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		public override void SetDefaults()
		{
			NPC.width = 58;
			NPC.height = 94;
			NPC.damage = 25;
			NPC.defense = 7;
			NPC.lifeMax = 350;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 60f;
			NPC.knockBackResist = 0.5f;
			NPC.noGravity = true;
			NPC.aiStyle = 5; // 战斗AI，重要的是选择匹配我们想要模仿的NPCID的aiStyle
			AIType = NPCID.EaterofSouls; // 执行AI代码时使用香草僵尸类型。(这也意味着它会尝试在白天消失)
			AnimationType = NPCID.EaterofSouls; // 执行动画代码时使用香草僵尸类型。重要的是匹配Main.npcFrameCount[NPC.type]在SetStaticDefaults。
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			IItemDropRule ShadowScaleDropRule = new LeadingConditionRule(new ShadowScaleDropCondition());
			ShadowScaleDropRule.OnSuccess(new CommonDrop(ItemID.TissueSample, 20, 7, 9, 19)).OnFailedRoll(new CommonDrop(ItemID.CrimsonSeeds, 1, 1, 2));
			ShadowScaleDropRule.OnFailedConditions(new CommonDrop(ItemID.ShadowScale, 20, 7, 9, 19)).OnFailedRoll(new CommonDrop(ItemID.CorruptSeeds, 1, 1, 2));
			npcLoot.Add(ShadowScaleDropRule);
            npcLoot.Add(ItemDrop.GetItemDropRule<UlcerPupil>(15));//溃烂瞳孔 有15%的几率掉落
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss2 && (spawnInfo.Player.ZoneCrimson || spawnInfo.Player.ZoneCorrupt))
			{
				return SpawnCondition.Corruption.Chance == 0 ? SpawnCondition.Crimson.Chance * 0.15f : SpawnCondition.Corruption.Chance * 0.15f;
			}
			return 0;
		}

		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				new ModBiomeBestiaryInfoElement(Mod,"混沌核心","","",Color.White),
				new FlavorTextBestiaryInfoElement("由于众多遗骸组成的憎恶扭曲之物.")
			}) ;
		}
	}
}
