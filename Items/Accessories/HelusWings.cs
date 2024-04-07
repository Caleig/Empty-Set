using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 赫琉斯之翼
/// </summary>
[AutoloadEquip(EquipType.Wings)]
public class HelusWings : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Helus Wings");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(300, 1);
    }

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.rare = ItemRarityID.Pink;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (Main.rand.Next(4) == 0 &&
            player.wingTime < player.wingTimeMax &&
            !player.mount.Active)
        {
            Dust.NewDust(player.position - new Vector2(16, 0) * player.direction, player.width, player.height, DustID.UnusedWhiteBluePurple);
        }

    }
    public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
    {
        maxCanAscendMultiplier = 1.95f;
        maxAscentMultiplier = 1.4f;
    }

    public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
    {
        speed = 8f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.GiantHarpyFeather)
        .AddIngredient(ItemID.IceFeather)
        .AddIngredient(ItemID.FireFeather)
        .AddIngredient(ItemID.SoulofFlight, 30)
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 10)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}