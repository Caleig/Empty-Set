using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 硬化凝胶
/// </summary>
public class HardenedGel : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 0, 25);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Gel, 10)
        .AddTile(TileID.Solidifier)
        .Register();
}