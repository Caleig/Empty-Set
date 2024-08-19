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
///利锐佩刀
/// </summary>
public class KeenKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 36;
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
        .AddIngredient(ModContent.ItemType<BloodlettingKnife>())
        .AddIngredient(ItemID.PurificationPowder,20)
        .AddTile(ModContent.TileType<PurifierTile>())
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.07f;
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Melee, 0.07f);
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Ranged, 0.07f);
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Magic, 0.07f);
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Summon, 0.07f);
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.07f);
    }
}
