using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Common.Systems;
using EmptySet.Tiles;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Placeable;

/// <summary>
/// 金属加工站
/// </summary>
public class Steelprocessingstation : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.width = 48;
        Item.height = 32;
        Item.maxStack = 999;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 10, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<SteelprocessingstationTile>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Hellforge, 1)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.CopperAnvil))
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
