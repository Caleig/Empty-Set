using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 撼地者重剑
/// </summary>
public class ShakeEartherEpee : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Shake Earther Epee");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 80;
        Item.height = 80;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = UseSpeedLevel.VerySlowSpeed + 3;
        Item.useTime = UseSpeedLevel.VerySlowSpeed + 3;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 31;
        Item.knockBack = KnockBackLevel.Normal;
        Item.crit = 7-4;
        Item.value = Item.sellPrice(0, 0, 75, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<ShakeEartherEpeeProj>();
        Item.shootSpeed = 4;
    }


    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 18)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 7)
        .AddTile(TileID.Anvils)
        .Register();

    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        if (Main.rand.NextBool(3)) 
        {
            Dust.NewDustDirect(new Vector2(0, -120).RotatedBy(player.itemRotation + MathHelper.ToRadians(20 * player.direction)) + player.Center, 2, 2, DustID.Electric).noGravity = true;
        }
        base.UseItemHitbox(player, ref hitbox, ref noHitbox);
    }
}
