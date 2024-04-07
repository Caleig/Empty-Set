using EmptySet.Common.Effects.Common;
using EmptySet.Extensions;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 防御者护符
/// </summary>
//             "血量低于75%/50%/25%时\n" +
//            "防御增加2/3/5点\n" +
//            "减伤增加2%/3%/4%\n" +
//            "受伤后无敌帧增加5/7/10帧\n" +
//            "每0.5秒回血增加0/0/0.5点\n" +
//            "在击败血肉之墙的世界中：\n" +
//            "血量低于75%/50%/25%时\n" +
//            "防御增加4/6/11点\n" +
//            "减伤增加3%/5%/7%\n" +
//            "受伤后无敌帧增加9/13/19帧\n" +
//            "每0.5秒回血增加0.5/0.5/1"
public class DefenderTalisman : ModItem
{
    public override void SetStaticDefaults()
    {   //Defender amulet特指人佩戴的饰品   Talisman特指神秘力量
        // DisplayName.SetDefault("Defender Talisman");
        /* Tooltip.SetDefault("Defeating the Wall of Flesh can release its real power.\n" +
                           "Defense attributes increase with HP reduction"); */
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 30;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.rare = ItemRarityID.Pink;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var p = player.GetModPlayer<ImmuneTimeEffect>();

        if (Main.hardMode)
        {
            player.statDefense += 3;
            player.endurance += 0.02f;
            player.lifeRegen += 1;

            if (player.GetLifeStatus(0.25))
            {
                player.statDefense += 11;
                player.endurance += 0.07f;
                p.SetImmuneTime(19);
                player.lifeRegen += 2;
            }
            else if (player.GetLifeStatus(0.5))
            {
                player.statDefense += 6;
                player.endurance += 0.05f;
                p.SetImmuneTime(13);
                player.lifeRegen += 1;

            }
            else if (player.GetLifeStatus(0.75))
            {
                player.statDefense += 4;
                player.endurance += 0.03f;
                p.SetImmuneTime(9);
                player.lifeRegen += 1;
            }
        }
        else
        {
            player.statDefense += 2;
            player.endurance += 0.01f;

            if (player.GetLifeStatus(0.25))
            {
                player.statDefense += 5;
                player.endurance += 0.04f;
                p.SetImmuneTime(10);
                player.lifeRegen += 1;
            }
            else if (player.GetLifeStatus(0.5))
            {
                player.statDefense += 3;
                player.endurance += 0.03f;
                p.SetImmuneTime(7);
            }
            else if (player.GetLifeStatus(0.75))
            {
                player.statDefense += 2;
                player.endurance += 0.02f;
                p.SetImmuneTime(5);
            }
        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        base.ModifyTooltips(tooltips);
        if (Main.hardMode) tooltips[2].Text = "";
    }
}