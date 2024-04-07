using EmptySet.Common.Effects.Item;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using EmptySet.Items.Materials;
using Terraria.Localization;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 掠食者项链
/// </summary>
public class PredatorNecklace : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 58;
        Item.height = 70;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 5;
        player.GetDamage(DamageClass.Generic) += 0.05f;
        player.GetArmorPenetration(DamageClass.Generic) += 5;
        player.GetModPlayer<PredatorNecklaceEffect>().Enabled(Item);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.StingerNecklace)
        .AddIngredient(ItemID.Shackle)
        .AddIngredient(ModContent.ItemType<FangsNecklace>())
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 10)
        .AddTile(TileID.Anvils)
        .Register();
}