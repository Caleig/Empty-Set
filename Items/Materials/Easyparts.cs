using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 简易电子元件
/// </summary>
public class Easyparts : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe(2)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 1)
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 4)
        .AddTile(18) //熔炉
        .Register();
}
