using EmptySet.Common.Effects.Common;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// ²×»Òµõ×¹
/// </summary>
public class Graypendant : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.value = Item.sellPrice(0, 1, 50, 0);
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }
    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<HuntingTalisman>());
        if (hasIt)
        {
            return false;
        }
        else
            return true;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<HuntingTalisman>())
        .AddIngredient(ItemID.JellyfishNecklace)//Ë®Ä¸ÏîÁ´
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 3)//3ºã»Ò¶§
        .AddTile(TileID.Anvils)
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        if (!player.wet)
        {
            Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.225f, 0.05f, 0.15f);
        }
        if (player.wet)
        {
            Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 1.8f, 0.4f, 1.2f);
        }
        player.GetDamage(DamageClass.Throwing) += 0.1f;
        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.15f);
    }
}