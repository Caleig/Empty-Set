using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 木头护腕
/// </summary>
public class WoodenBracer : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 24;
        Item.value = Item.sellPrice(0, 0, 0, 5);
        Item.rare = ItemRarityID.White;
        Item.accessory = true;
    }

    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<MetalBracer>() || x.type == ModContent.ItemType<MetalBracer>() || x.type == ModContent.ItemType<BeingsWoodenBracer>() || x.type == ModContent.ItemType<ScrletBracer>() || x.type == ModContent.ItemType<DemonBracer>());
        var isThis = player.armor[slot].type == ModContent.ItemType<MetalBracer>() || player.armor[slot].type == ModContent.ItemType<MetalBracer>() || player.armor[slot].type == ModContent.ItemType<BeingsWoodenBracer>() || player.armor[slot].type == ModContent.ItemType<ScrletBracer>() || player.armor[slot].type == ModContent.ItemType<DemonBracer>();
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
        player.GetCritChance(DamageClass.Throwing) += 4f;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Wood)
        .AddTile(TileID.WorkBenches)
        .Register();
}
