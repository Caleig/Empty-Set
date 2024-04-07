using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 烟花1
/// </summary>
public class Firework1 : ModItem
{
    public override string Texture => "EmptySet/Items/Consumables/Firework";

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Firework");
    }
    public override void SetDefaults()
    {
        Item.width = 8;
        Item.height = 8;
        Item.maxStack = 999;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useTurn = true;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.consumable = true;
    }

    public override void AddRecipes() => CreateRecipe(10)
        .AddIngredient(ItemID.AdamantiteBar, 10)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}