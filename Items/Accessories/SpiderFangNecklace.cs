using EmptySet.Common.Effects.Common;
using EmptySet.Common.Effects.Item;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 蛛牙项链
/// </summary>
public class SpiderFangNecklace : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 56;
        Item.height = 30;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 0, 31, 0);
        Item.accessory = true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SpiderFang, 4)
        .AddIngredient(ItemID.Silk, 2)
        .AddTile(TileID.TinkerersWorkbench)
        .Register();

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxMinions += 1;
        player.GetDamage(DamageClass.Throwing) -= 0.1f;
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.09f);
        player.GetModPlayer<SpiderFangNecklaceEffect>().Enable();
    }
}