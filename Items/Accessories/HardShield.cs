using EmptySet.Common.Effects.Common;
using EmptySet.Common.Players;
using EmptySet.Extensions;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 坚壁盾
/// </summary>
[AutoloadEquip(EquipType.Shield)]
public class HardShield : ModItem
{
    public override void SetStaticDefaults()
    {   
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 30;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.rare = ItemRarityID.Cyan;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 3;
        player.endurance += 0.03f;
        //充能MAX+10
        var myEnergier = player.GetModPlayer<EnergyPlayer>();
        myEnergier.EnengyMax += 10;
        if (myEnergier.IsMaxEnergy)//充能是满的
        {
            player.moveSpeed += 0.15f;
            player.noKnockback = true;
        }
        else player.noKnockback = false;
        var p = player.GetModPlayer<ImmuneTimeEffect>();
        if (player.GetLifeStatus(0.5))
        {
            player.statDefense += 3;
            player.endurance += 0.03f;
            p.SetImmuneTime(6);
            for (int i = 0; i <2; i++)
            {
                var d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.BlueTorch,default,default,default,default,1.4f);
                d.noGravity = true;
            }

        }
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        //base.ModifyTooltips(tooltips);
        //if (Main.hardMode) tooltips[2].Text = "";
    }
}