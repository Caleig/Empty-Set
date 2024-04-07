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
/// 烟花发射架
/// </summary>
public class FireworkLauncher : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 26;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<FireworkLauncherTile>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 5)
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 20)
        .AddTile(TileID.Anvils)
        .Register();
}
