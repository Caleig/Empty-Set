using EmptySet.Biomes;
using EmptySet.Common.ItemDropRules.DeepGumPolymer;
using EmptySet.Items.Accessories;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy
{
    class DeepGumPolymer: ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("渊胶聚合体");

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BlueSlime];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f 
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		public override void SetDefaults()
		{
			NPC.width = 70;
			NPC.height = 50;
			NPC.damage = 30;
			NPC.defense = 5;
			NPC.lifeMax = 600;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 60f;
			NPC.knockBackResist = 0.9f;
			NPC.aiStyle = 1; // 战斗AI，重要的是选择匹配我们想要模仿的NPCID的aiStyle
			AIType = NPCID.None; // 执行AI代码时使用香草僵尸类型。(这也意味着它会尝试在白天消失)
			AnimationType = NPCID.CorruptSlime; // 执行动画代码时使用香草僵尸类型。重要的是匹配Main.npcFrameCount[NPC.type]在SetStaticDefaults。
			
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			IItemDropRule ShadowScaleDropRule = new LeadingConditionRule(new ShadowScaleDropCondition());
			ShadowScaleDropRule.OnSuccess(new CommonDrop(ItemID.TissueSample, 20, 3, 5,19)).OnFailedRoll(new CommonDrop(ItemID.CrimsonSeeds, 1, 1, 2 ));
			ShadowScaleDropRule.OnFailedConditions(new CommonDrop(ItemID.ShadowScale, 20, 3, 5,19)).OnFailedRoll(new CommonDrop(ItemID.CorruptSeeds, 1, 1, 2));
			npcLoot.Add(ShadowScaleDropRule);
            npcLoot.Add(ItemDrop.GetItemDropRule<BloodlettingKnife>(15));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedBoss2&&(spawnInfo.Player.ZoneCrimson|| spawnInfo.Player.ZoneCorrupt)) 
			{
				return SpawnCondition.Corruption.Chance==0? SpawnCondition.Crimson.Chance*0.15f: SpawnCondition.Corruption.Chance * 0.15f;
			}
			return 0;
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
				new FlavorTextBestiaryInfoElement("一只吞噬了众多带着憎恨而死的遗骸的史莱姆")
			});
		}
	}
}
