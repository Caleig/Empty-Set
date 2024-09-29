using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 硬化凝胶
/// </summary>
public class Flamepollen : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 0, 15);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe(2)
        .AddIngredient(ItemID.Fireblossom)
        .AddTile(13)
        .Register();
}
