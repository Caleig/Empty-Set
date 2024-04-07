using EmptySet.Common.Effects.Common;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 溃林项链
/// </summary>
public class ForestNecklace : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.value = Item.sellPrice(0, 0, 3, 0);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    //public override bool CanEquipAccessory(Player player, int slot, bool modded)
    //{
    //    return true;
    //}
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        //if (!player.GetModPlayer<BurnHellTalismanEffect>().hasBurnHellTalisman)
        if(!(player.armor.Any((x) => x.type == ModContent.ItemType<BurnHellTalisman>())))
        {
            player.GetDamage(DamageClass.Summon) += 0.07f; // 增加12 % 的投掷和召唤伤害
            player.GetDamage(DamageClass.Throwing) += 0.07f;
            player.maxMinions += 1; //+1仆从位
            player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.10f);
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        //if (Main.player[Item.playerIndexTheItemIsReservedFor].GetModPlayer<BurnHellTalismanEffect>()
        //   .hasBurnHellTalisman)
        if (Main.player[Item.playerIndexTheItemIsReservedFor].armor
            .Any((x) => x.type == ModContent.ItemType<BurnHellTalisman>()))
        {
            var t = Language.ActiveCulture.Name switch
            {
                "zh-Hans" => "装备焚狱护符时溃林项链效果不生效",
                _ => "It does nothing when equipped with BurnHell Talisman."
            };

            tooltips.Add(new TooltipLine(Mod, "Tooltip#", t));
        }

    }
}