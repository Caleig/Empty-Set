using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace EmptySet.Extensions;

public static class NpcLootExtensions
{
    public static void AddManualLoot(this NPCLoot npcLoot, int[] npcs, NPC npc, params IItemDropRule[] rules)
    {
        if (npcs.Any(_npc => npc.type == _npc))
            foreach (var rule in rules)
                npcLoot.Add(rule);
    }
}