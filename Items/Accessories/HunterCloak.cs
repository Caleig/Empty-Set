using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 猎客披风
/// </summary>
public class HunterCloak : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 32;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Throwing) += 4f;
        player.moveSpeed += 0.15f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.FallenStar, 2)
        .AddIngredient(ItemID.Silk, 32)
        .AddTile(TileID.Loom)
        .Register();
}