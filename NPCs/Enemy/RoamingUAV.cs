
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using EmptySet.Utils;
using EmptySet.Items.Materials;
using EmptySet.Common.Systems;

namespace EmptySet.NPCs.Enemy;
//npc 碰撞体有点毛病 
public class RoamingUAV : ModNPC
{
    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[Type] = Main.npcFrameCount[4];

        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Velocity = 1f
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        NPC.width = 32;
        NPC.height = 28;
        NPC.lifeMax = EmptySetUtils.GetNPCLifeMax(15, 30, 45);
        NPC.defense = 3;
        NPC.damage = EmptySetUtils.GetNPCDamage(5, 10, 15);
        NPC.knockBackResist = 0.1f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        //NPC.value = 60f;
        NPC.aiStyle = 2; // 战斗AI，重要的是选择匹配我们想要模仿的NPCID的aiStyle
        AIType = NPCID.None; // 执行AI代码时使用香草僵尸类型。(这也意味着它会尝试在白天消失)
                             //AnimationType = NPCID.DemonEye; // 执行动画代码时使用类型。重要的是在SetStaticDefaults匹配Main.npcFrameCount[NPC.type]。
                             //Banner = NPC.type; // 使这个NPC受到横幅的影响。
                             //BannerItem = ModContent.ItemType<RoamingUAVBanner>(); // 使这个NPC的击杀去掉它所关联的横幅。
                             //SpawnModBiomes = new int[1] { ModContent.GetInstance<ExampleSurfaceBiome>().Type }; // 将此NPC与图鉴中的ExampleSurface生物群落关联
    }
    public override bool PreAI()
    {
        //if (NPC.life > NPC.lifeMax) NPC.life = NPC.lifeMax;
        NPC.direction = (int)NPC.velocity.X;
        NPC.spriteDirection = NPC.velocity.X < 0 ? 1 : -1;
        return base.PreAI();
    }
    public override void FindFrame(int frameHeight)
    {
        var padding = 5;//tick
        var h = NPC.height;
        NPC.frame.Height = h;
        NPC.frame.Y = (((int)Main.time / padding) % 4) switch
        {
            0 => h * 0,
            1 => h * 1,
            2 => h * 2,
            3 => h * 3,
        };

    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        //IItemDropRule CopperDropRule = new LeadingConditionRule(new CopperDropCondition());
        //CopperDropRule.OnSuccess(new CommonDrop(ItemID.CopperOre, 1, 4, 4));
        //CopperDropRule.OnFailedConditions(new CommonDrop(ItemID.TinOre, 1, 4, 4));

        //IItemDropRule IronDropRule = new LeadingConditionRule(new IronDropCondition());
        //IronDropRule.OnSuccess(new CommonDrop(ItemID.IronOre, 1, 3, 3));
        //IronDropRule.OnFailedConditions(new CommonDrop(ItemID.LeadOre, 1, 3, 3));

        //IItemDropRule[] options = { CopperDropRule, IronDropRule };
        //npcLoot.Add(new OneFromRulesRule(1,options));


        npcLoot.Add(new OneFromRulesRule(1, ItemDrop.GetItemDropRule(ItemID.CopperCoin, 100, 10, 30), ItemDrop.GetItemDropRule(ModContent.ItemType<碎晶>(), 30)));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneOverworldHeight)
        {
            if (DownedBossSystem.downedEarthShaker) //打败了撼地巨械
                return SpawnCondition.OverworldDay.Chance * 0.03f;
            else
                return SpawnCondition.OverworldDay.Chance * 0.3f;
        }
        return 0;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("以恶魔眼为原型制作的无人机，主要负责侦查工作")
            });
    }
}
