using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 恒灰短弓
/// </summary>
public class EternityAshShortBow : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Eternity Ash Short Bow");
         CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2, 15, 0);

        Item.width = 36; //已精确测量
        Item.height = 86;

        Item.damage = 29;
        Item.crit = 3;
        Item.scale = 0.6f;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.UseSound = SoundID.Item5; //弓发射

        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.autoReuse = true;

        Item.useAmmo = AmmoID.Arrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 6f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var age = (float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X);
        var offset = //3倍为默认
            ((float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X) +
             0 * MathHelper.Pi / 36f).ToRotationVector2();
        var offset1 = ((age + 90 * MathHelper.Pi / 36f) *
                       new Vector2(1, 0).Length()).ToRotationVector2() * 8;
        var offset2 = ((age - 90 * MathHelper.Pi / 36f) *
                       new Vector2(-1, 0).Length()).ToRotationVector2() * 8;
        Projectile.NewProjectile(source, position + offset1, velocity, type,//offset * 9
            damage, knockback, player.whoAmI);
        Projectile.NewProjectile(source, position + offset2, velocity, type,//offset * 9
            damage, knockback, player.whoAmI);//new ProjectileSource_Item(player, Item)
        return false;

    }

    //public override Vector2? HoldoutOffset() =>
    //    new Vector2(4, -4);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(),12)
        .AddIngredient(ModContent.ItemType<ShiverBow>())
        .AddTile(TileID.Anvils)
        .Register();
}
