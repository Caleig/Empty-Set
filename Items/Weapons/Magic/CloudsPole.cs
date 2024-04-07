using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 阴云极点
/// </summary>
public class CloudsPole : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 74;
        Item.height = 74;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.damage = 17;
        Item.knockBack = KnockBackLevel.TooLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 37, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item8;
        Item.mana = 13;

        Item.shoot = ModContent.ProjectileType<CloudsPoleProj>();
        Item.shootSpeed = 7;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, Main.MouseScreen + Main.screenPosition, new Vector2(0, 0), type, damage,
            knockback, Main.myPlayer);
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<RaySoul>(), 5)
        .AddIngredient(ItemID.RainCloud, 3)
        .AddIngredient(ItemID.Cloud, 7)
        .AddTile(TileID.Anvils)
        .Register();

}
