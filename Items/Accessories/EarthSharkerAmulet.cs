using EmptySet.Common.Effects.Common;
using EmptySet.Common.Effects.Item;
using EmptySet.Common.Players;
using EmptySet.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 撼地护符
/// </summary>
public class EarthSharkerAmulet : ModItem
{
    public override void SetStaticDefaults()
    {   
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 26;
        Item.value = Item.sellPrice(0, 0, 65, 0);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.moveSpeed *= 1.1f;
        player.maxFallSpeed *= 1.1f;
        //
        var myEnergier = player.GetModPlayer<EnergyPlayer>();
        myEnergier.Enable();
        if (myEnergier.IsMaxEnergy)//充能是满的
        {
            player.jumpBoost = true;
            player.jumpSpeedBoost *= 1.15f;
        }
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        //spriteBatch.Draw() UI?
    }
}