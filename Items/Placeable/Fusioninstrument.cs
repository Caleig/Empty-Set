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
/// 聚合魔仪
/// </summary>
public class Fusioninstrument : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 30;
        Item.maxStack = 999;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<FusioninstrumentTile>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.CrystalBall, 1)
        .AddIngredient(ItemID.HallowedBar,4)
        .AddIngredient(ModContent.ItemType<FelShadowBar>(),4)
        .AddIngredient(1508, 2)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
