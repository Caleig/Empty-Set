using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using EmptySet.Utils;
using Terraria.ID;
using EmptySet.Tiles;

namespace EmptySet.Items.Placeable;

public class 缭乱砾岩 : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("缭乱砾岩");

        //Tooltip.SetDefault("缭乱砾岩");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "缭乱砾岩");
    }

    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 16;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 0, 20, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.createTile = ModContent.TileType<缭乱砾岩Tile>();
        Item.autoReuse = true;
    }
}