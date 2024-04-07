using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 活木花环
/// </summary>
public class LivinglogWreath : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 28;
        Item.rare = ItemRarityID.Lime;
        
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statLifeMax2 += (int)(player.statLifeMax2 * 0.2f);
        player.moveSpeed += 2.3f;
        player.jumpBoost = true;
        player.jumpSpeedBoost += 0.25f;
    }
}