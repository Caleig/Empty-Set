using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 叶绿弓
/// </summary>
public class ChlorophyteBow : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Chlorophyte Bow");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 80;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 31;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 5, 12, 0);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item5;

        Item.shoot = ProjectileID.ChlorophyteArrow;
        Item.shootSpeed = 7;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Projectile.NewProjectile(source, position, velocity, ProjectileID.ChlorophyteArrow, damage, knockback, player.whoAmI);
        Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(15)), ProjectileID.ChlorophyteArrow, damage, knockback, player.whoAmI);
        Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.ToRadians(-15)), ProjectileID.ChlorophyteArrow, damage, knockback, player.whoAmI);
        return false;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.ChlorophyteBar, 15)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}