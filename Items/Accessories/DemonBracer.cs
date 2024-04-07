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
/// 魔矿护腕
/// </summary>
public class DemonBracer : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 26;
        Item.value = Item.sellPrice(0, 1, 10, 0);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<WoodenBracer>() || x.type == ModContent.ItemType<MetalBracer>() || x.type == ModContent.ItemType<BeingsWoodenBracer>() || x.type == ModContent.ItemType<ScrletBracer>());
        var isThis = player.armor[slot].type == ModContent.ItemType<WoodenBracer>() || player.armor[slot].type == ModContent.ItemType<MetalBracer>() || player.armor[slot].type == ModContent.ItemType<BeingsWoodenBracer>() || player.armor[slot].type == ModContent.ItemType<ScrletBracer>();
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
        player.statDefense += 3;
        player.GetCritChance(DamageClass.Throwing) += 4f;
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.03f);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<MetalBracer>()
        .AddIngredient(ItemID.DemoniteBar, 4)
        .AddRecipeGroup(RecipeGroupID.IronBar)
        .AddTile(TileID.Anvils)
        .Register();
}
