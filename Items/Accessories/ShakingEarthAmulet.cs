using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 撼地护符
/// </summary>
public class ShakingEarthAmulet : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Shaking Earth Amulet");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 38;
        Item.value = Item.sellPrice(0, 0, 65, 0);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
        Item.expert = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 += 10;
        player.moveSpeed += 0.1f;
        
        player.jumpBoost = true;
        player.jumpSpeedBoost += 0.1f;//效果同蜂蜜气球
    }
}