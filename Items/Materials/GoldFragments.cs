using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 黄金碎块
/// </summary>
public class GoldFragments : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 0, 60);
        Item.maxStack = 999;
    }
}