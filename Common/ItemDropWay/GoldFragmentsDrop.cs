using EmptySet.Extensions;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class GoldFragmentsDrop : GlobalNPC
{
    private static readonly int[] DropNpcList =
    {
        NPCID.KingSlime //史莱姆王
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => npcLoot.AddManualLoot(DropNpcList, npc,
        ItemDrop.GetItemDropRule<GoldFragments>(100, 10, 17));
}