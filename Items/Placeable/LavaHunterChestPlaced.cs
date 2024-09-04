using Microsoft.Xna.Framework;
using EmptySet.Items.Consumables;
using EmptySet.Tiles;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Placeable;

/// <summary>
/// 匣子
/// </summary>
public class LavaHunterChestPlaced : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.width = 48;
        Item.height = 48;
        Item.maxStack = 999;
        Item.rare = ItemRarityID.Purple;
        Item.value = Item.sellPrice(0, 0, 0, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<LavaHunterChestTile>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<LavaHunterChest>(),1)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
