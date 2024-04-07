using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Tiles;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Placeable;

/// <summary>
/// 望远镜
/// </summary>
public class Telescope : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 48;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<TelescopeTile>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Nanites, 30)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(),15)
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 40)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
