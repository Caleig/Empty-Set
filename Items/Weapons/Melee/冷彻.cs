using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using System;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 冷彻
/// </summary>
public class 冷彻 : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Blood Sickle");
       
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        Item.width = 36;
        Item.height = 44;

        Item.crit = 4;
        Item.damage = 29;

        Item.knockBack = KnockBackLevel.Normal +1f;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Melee;
        
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Swing;

        Item.shoot = ProjectileID.FrostBoltStaff;
        Item.shootSpeed = 15f;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useTime = UseSpeedLevel.VeryFastSpeed + 2;
            Item.useAnimation = UseSpeedLevel.VeryFastSpeed + 2;
        }
        else
        {
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = UseSpeedLevel.FastSpeed;
            Item.useAnimation = UseSpeedLevel.FastSpeed;
        }
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
            for (int i = -1; i <= 1; i++)
                Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(12f * i)),
                    type, damage, knockback, player.whoAmI);
        }

        if (Main.mouseRight)
        {
            Projectile.NewProjectile(source,position,velocity.SafeNormalize(Vector2.One)*2.8f,ModContent.ProjectileType<冷彻弹幕>(),damage,knockback, player.whoAmI);
            return true;
        }
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<寒川锭>(), 30)
        .AddIngredient(ItemID.Frostbrand) //寒霜剑
        .AddIngredient(ItemID.FrostCore,2)
        .AddTile(TileID.MythrilAnvil)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
