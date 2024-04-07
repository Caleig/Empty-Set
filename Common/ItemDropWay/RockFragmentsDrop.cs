using EmptySet.Extensions;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class RockFragmentsDrop : GlobalNPC
{
    private static readonly int[] DropNpcList =
    {
        NPCID.GraniteGolem, //花岗岩巨人
        NPCID.GraniteFlyer, //花岗精?? Granite Elemental
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (Main.hardMode)
        {
            npcLoot.AddManualLoot(new int[] {NPCID.GraniteGolem}, npc,
                ItemDrop.GetItemDropRule<RockFragments>(50, 3, 4));
            npcLoot.AddManualLoot(new int[] {NPCID.GraniteFlyer}, npc,
                ItemDrop.GetItemDropRule<RockFragments>(33, 1, 2));
        }
       
    }
}