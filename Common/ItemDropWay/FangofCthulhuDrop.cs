using EmptySet.Extensions;
using EmptySet.Items.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class FangofCthulhuDrop : GlobalNPC
{
    private static readonly int[] DropNpcList =
    {
        NPCID.EyeofCthulhu //史莱姆王
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => npcLoot.AddManualLoot(DropNpcList, npc,
        ItemDrop.GetItemDropRule<FangofCthulhu>(100, 20, 40));
}
