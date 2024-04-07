
using EmptySet.Common.ItemDropRules.RoamingUAV;
using EmptySet.Common.ItemDropRules;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy
{
    class RoamingCar : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("巡游小车");

			Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GiantWalkingAntlion];

			NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
			{
				Velocity = 1f 
			};
			NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		}
		public override void SetDefaults()
		{
			NPC.width = 46;
			NPC.height = 26;
			NPC.damage = 17;
			NPC.defense = 4;
			NPC.lifeMax = 25;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.value = 60f;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = 26; // 战斗AI，重要的是选择匹配我们想要模仿的NPCID的aiStyle

			AIType = NPCID.None; // 执行AI代码时使用香草僵尸类型。(这也意味着它会尝试在白天消失)
			AnimationType = NPCID.GiantWalkingAntlion; // 执行动画代码时使用香草类型。重要的是在SetStaticDefaults匹配Main.npcFrameCount[NPC.type]。
			//Banner = Item.NPCtoBanner(NPCID.GiantWalkingAntlion); // 使这个NPC受到普通横幅的影响。
			//BannerItem = Item.BannerToItem(Banner); // 使这个NPC的击杀去掉它所关联的横幅。
			//SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // 将此NPC与图鉴中的ExampleSurface生物群落关联

		}




		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			IItemDropRule CopperDropRule = new LeadingConditionRule(new CopperDropCondition());
			CopperDropRule.OnSuccess(new CommonDrop(ItemID.CopperOre, 1, 4, 4));
			CopperDropRule.OnFailedConditions(new CommonDrop(ItemID.TinOre, 1, 4, 4));

			IItemDropRule IronDropRule = new LeadingConditionRule(new IronDropCondition());
			IronDropRule.OnSuccess(new CommonDrop(ItemID.IronOre, 1, 3, 3));
			IronDropRule.OnFailedConditions(new CommonDrop(ItemID.LeadOre, 1, 3, 3));

			IItemDropRule[] options = { CopperDropRule, IronDropRule };
			npcLoot.Add(new OneFromRulesRule(1, options));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.Player.ZoneForest)
			{
				return SpawnCondition.OverworldDay.Chance * 0.27f;
			}
			return 0;
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
				new FlavorTextBestiaryInfoElement("用于警戒的小车，会以极快的加速度将一脸懵逼的新人创死")
			});
		}
	}
}
