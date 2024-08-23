using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 恒灰锭
/// </summary>
public class EternityAshBar : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 55, 0);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshOre>(), 3)
        .AddTile(TileID.Furnaces) //熔炉
        .Register();
}