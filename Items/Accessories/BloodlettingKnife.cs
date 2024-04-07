using EmptySet.Common.Effects.Item;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 放血短刀
/// </summary>
public class BloodlettingKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Bloodletting Knife");
        /* Tooltip.SetDefault("10% increased damage\n" +
                           "10% increased critcal strike chance\n" +
                           "\"A dagger used by worshippers of the ancient gods\"\n" +
                           "\"It is rumored that believers will slit their arms with knives\n and sacrifice to the gods with their own blood.\""); */

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 54;
        Item.height = 58;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 45, 0);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.1f;
        player.GetCritChance(DamageClass.Generic) += 10f;
        var myLifeRegen = player.lifeRegen;
        var mylifeRegenTime = player.lifeRegenTime;
        player.lifeRegen = 0;
        player.lifeRegenTime = 0;
        if (player.GetModPlayer<BloodlettingKnifeEffect>().UseTimer != 0)
        {
            player.lifeRegen = myLifeRegen;
            player.lifeRegenTime = mylifeRegenTime;
        }
    }
}