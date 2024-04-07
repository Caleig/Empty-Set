using EmptySet.Common.Effects.Item;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 溃烂瞳孔
/// </summary>
public class UlcerPupil : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 28;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 45, 0);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<UlcerPupilEffect>().Enable();
        //但是盔甲穿透增加10
        player.GetArmorPenetration(DamageClass.Generic) += 10;
    }
}