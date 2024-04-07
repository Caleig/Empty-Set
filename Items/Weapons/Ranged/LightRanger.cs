using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using System;
using EmptySet.Projectiles.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 曙光巡游者
/// </summary>
internal class LightRanger : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(11, 11, 11, 11);
        Item.width = 48;
        Item.height = 92;
        Item.damage = 47;
        Item.crit = 18;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.autoReuse = true;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;

        Item.shoot = ProjectileID.HolyArrow;
        Item.shootSpeed = 12f;
        Item.useTurn = true;
        Item.useAmmo = AmmoID.Arrow;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useTime = 9;
            Item.useAnimation = 9;
        }
        else
        {
            Item.useTime = UseSpeedLevel.NormalSpeed;
            Item.useAnimation = UseSpeedLevel.NormalSpeed;
        }
        return true;
    }
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        return player.altFunctionUse != 2;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
            var age = (float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X);

            var offset = ((float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X) + 0 * MathHelper.Pi / 36f).ToRotationVector2() * velocity.Length();
            var offset1 = ((age + 90 * MathHelper.Pi / 36f) * new Vector2(0, 1).Length()).ToRotationVector2() * 8;
            var offset2 = ((age - 90 * MathHelper.Pi / 36f) * new Vector2(0, 1).Length()).ToRotationVector2() * 8;
            type = type == 1 ? ProjectileID.HolyArrow : type;
            var proj1 = Projectile.NewProjectileDirect(source, position + offset1, offset, type, damage, knockback, player.whoAmI);
            proj1.rotation = proj1.velocity.ToRotation() + MathHelper.ToRadians(90f);
            var proj2 = Projectile.NewProjectileDirect(source, position + offset2, offset, type, damage, knockback, player.whoAmI);
            proj2.rotation = proj2.velocity.ToRotation() + MathHelper.ToRadians(90f);

            return false;
        }

        if (Main.mouseRight)
        {
            Projectile.NewProjectile(source, position, velocity/2, ModContent.ProjectileType<LightRangerStar>(), damage, KnockBackLevel.BeLower, player.whoAmI);
            return false;
        }
        return false;
    }


    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<EternityAshShortBow>()
        .AddIngredient(ItemID.SoulofLight,3)
        .AddIngredient(ItemID.SoulofNight,3)
        .AddIngredient(ModContent.ItemType<FelShadowBar>(),3)
        .AddIngredient(ItemID.HallowedBar,3) 
        .AddTile(TileID.MythrilAnvil) 
        .Register();
}