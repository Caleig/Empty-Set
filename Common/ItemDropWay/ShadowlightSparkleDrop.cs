using EmptySet.Extensions;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class ShadowlightSparkleDrop : GlobalNPC
{
    private static readonly int[] DropNpcList =
    {
        NPCID.GoblinSummoner //哥布林召唤师
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => npcLoot.AddManualLoot(DropNpcList, npc,
        ItemDrop.GetItemDropRule<ShadowlightSparkle>(100, 7, 9));
}