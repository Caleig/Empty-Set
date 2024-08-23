using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 暗血碎晶
/// </summary>
public class DarkBrokenCrystal : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Dark Broken Crystal");


        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 14;
        Item.height = 18;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0,0,15,0);
    }
}
