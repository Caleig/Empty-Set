using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 獠牙项链
/// </summary>
public class FangsNecklace : ModItem
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("3 defense\n" +
                           "7% increased damage"); */
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 50;
        Item.value = Item.sellPrice(0,0,65,0);
        Item.rare = ItemRarityID.Green;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 3;
        player.GetDamage(DamageClass.Generic) += 0.07f; // 增加所有类型伤害7%
    }
}