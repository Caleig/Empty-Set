using Microsoft.Xna.Framework;
using EmptySet.Common.Effects.Common;
using EmptySet.Common.Effects.Item;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Summon;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 焚狱护符
/// </summary>
public class BurnHellTalisman : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("BurnHell Talisman");
        /* Tooltip.SetDefault("Increases your max number of minions by 1\n" +
                           "Increases throwing and minion damage by 8%\n" +
                           "Increases throwing speed by 12%\n"+
                           "Throwing weapons and whip attacks can set enemies on fire"); */

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 52;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 67, 0);
        Item.accessory = true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<ForestNecklace>())
        .AddIngredient(ModContent.ItemType<MoltenDebris>(), 7)
        .AddIngredient(ItemID.HellstoneBar, 2)
        .AddTile(TileID.Anvils)
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        Item item = this.Item;
        player.maxMinions += 1;
        player.GetDamage(DamageClass.Throwing) += 0.07f;
        player.GetDamage(DamageClass.Summon) += 0.07f;

        player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.12f);
        player.GetModPlayer<BurnHellTalismanEffect>().Enable();
        if (player.ownedProjectileCounts[ModContent.ProjectileType<Lavasprite>()] <= 0)
        {
            Projectile.NewProjectile(player.GetSource_Accessory(item), player.position, Vector2.Zero, ModContent.ProjectileType<Lavasprite>(), 0, 0);
        }
        //player.GetModPlayer<BurnHellTalismanEffect>().hasBurnHellTalisman = true;
    }
}
