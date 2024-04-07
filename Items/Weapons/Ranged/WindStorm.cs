using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 风暴
/// </summary>
public class WindStorm : ModItem
{
    public override void SetStaticDefaults()
    {CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 68;
        Item.height = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.autoReuse = false;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 32;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 5, 0, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item11;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 9f;
        Item.useAmmo = AmmoID.Bullet;
    }
    //public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.FlintlockPistol) //燧发枪
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilGun)) //火枪/夺命枪
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 20)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilBar),4)
        .AddTile(TileID.Anvils)
        .Register();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (type == ProjectileID.Bullet)//火枪子弹
        {
            SoundEngine.PlaySound(SoundID.Item75, player.position);
            Projectile.NewProjectileDirect(source,position,velocity*7/4, ModContent.ProjectileType<ChargedBullet>(), damage, knockback,Item.whoAmI);
            return false;
        }

        return true;
    }
}