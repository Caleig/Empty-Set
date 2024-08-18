using EmptySet.Common.Effects.Common;
using EmptySet.Common.Effects.Item;
using EmptySet.Common.Systems;
using EmptySet.Tiles;
using EmptySet.Utils;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
///蓝眼石
/// </summary>
public class BlueEyE : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 14;
        Item.value = Item.sellPrice(0, 0, 70, 0);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }
    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<Graypendant>());
        if (hasIt)
        {
            return false;
        }
        else
        return true;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<UlcerPupil>())
        .AddIngredient(ItemID.PurificationPowder,20)
        .AddTile(ModContent.TileType<PurifierTile>())
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Generic) += 5f;
        player.GetArmorPenetration(DamageClass.Generic) += 5;
    }
}
