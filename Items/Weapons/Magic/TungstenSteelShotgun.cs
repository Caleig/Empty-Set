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
/// 钨钢魔霰
/// </summary>
public class TungstenSteelShotgun:ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Mini Fireball Gun");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.DamageType = DamageClass.Magic;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.damage = 15;
        Item.mana = 12;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 2, 35);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.channel = true;
        Item.shoot = ModContent.ProjectileType<TungstenSteelProj>();
        Item.shootSpeed = 12;
    }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-3)), type, damage, knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(0f)), type, damage, knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(3f)), type, damage, knockback, player.whoAmI);
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 12)
        .AddTile(TileID.HeavyWorkBench)//重型工作台
        .AddTile(TileID.Anvils)
        .Register();

}

