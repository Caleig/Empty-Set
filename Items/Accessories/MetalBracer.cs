using EmptySet.Common.Effects.Common;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.Common.Effects.Item;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 金属护腕
/// </summary>
public class MetalBracer : ModItem
{
    float i;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 26;
        Item.value = Item.sellPrice(0, 0, 0, 60);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<WoodenBracer>() || x.type == ModContent.ItemType<ScrletBracer>() || x.type == ModContent.ItemType<BeingsWoodenBracer>());
        var isThis = player.armor[ slot].type == ModContent.ItemType<WoodenBracer>() || player.armor[slot].type == ModContent.ItemType<ScrletBracer>() || player.armor[slot].type == ModContent.ItemType<BeingsWoodenBracer>();
        if (hasIt)
        {
            if (isThis)
                return true;
            return false;
        }
        return true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 2;
        player.GetCritChance(DamageClass.Throwing) += 5f;

    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<WoodenBracer>()
        .AddRecipeGroup(RecipeGroupID.IronBar)
        .AddTile(TileID.Anvils)
        .Register();
}
