using EmptySet.Items.Consumables;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class StarterBoxEffect : ModPlayer
{
    public override IEnumerable<Terraria.Item> AddStartingItems(bool mediumCoreDeath)
    {
        var itemList = new List<Terraria.Item>();
        if (!mediumCoreDeath) itemList.Add(ModContent.GetModItem(ModContent.ItemType<StarterBox>()).Item);
        return itemList;
    }
}