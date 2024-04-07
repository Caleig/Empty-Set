using Microsoft.CodeAnalysis;
using EmptySet.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 祸难手环
/// </summary>
public class DisasterBracelet : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Disaster Bracelet");
        /* Tooltip.SetDefault("Increases your max number of minions by 1\n" +
                           "Increases minion damage by 4%\n" +
                           "Reduces player health by 20\n" +
                           "Reduces the player's defense by 5\n" +
                           "\"Woe betide him who wears it.\""); */
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

    }
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 24;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 17, 0);
        Item.accessory = true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<MoltenDebris>(4)
        .AddIngredient(ItemID.Leather,3)
        .AddTile(TileID.Anvils)
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.maxMinions += 1;
        player.GetDamage(DamageClass.Summon) += 0.2f;
        player.statLifeMax2 -= (int)(player.slotsMinions * player.statLifeMax * 0.05f);
    }
}