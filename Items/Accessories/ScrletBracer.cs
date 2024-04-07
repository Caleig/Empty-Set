using EmptySet.Common.Effects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 猩红护腕
/// </summary>
public class ScrletBracer : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 26;
        Item.value = Item.sellPrice(0, 1, 25, 0);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<WoodenBracer>() || x.type == ModContent.ItemType<MetalBracer>() || x.type == ModContent.ItemType<BeingsWoodenBracer>());
        var isThis = player.armor[slot].type == ModContent.ItemType<WoodenBracer>() || player.armor[slot].type == ModContent.ItemType<MetalBracer>() || player.armor[slot].type == ModContent.ItemType<BeingsWoodenBracer>();
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
        player.statDefense += 4;
        player.GetCritChance(DamageClass.Throwing) += 8f;
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.03f);
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<MetalBracer>()
        .AddIngredient(ItemID.CrimtaneBar, 4)
        .AddRecipeGroup(RecipeGroupID.IronBar)
        .AddTile(TileID.Anvils)
        .Register();
}
