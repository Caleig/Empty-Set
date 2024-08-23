using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using System;
using EmptySet.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 血影魔镰
/// </summary>
public class BloodSickle : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Blood Sickle");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 15, 0, 0);

        Item.width = 86;
        Item.height = 80;

        //Item.scale = 1.1f;
        Item.crit = 9-4;
        Item.damage = 37;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item71; //挥剑

        Item.autoReuse = true;
        

        Item.shoot = ModContent.ProjectileType<BloodSickleProj>();
        Item.shootSpeed = 10f;
    }
    //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //{
    //    Vector2 startPos = Main.MouseWorld + new Vector2(0, -500);
    //    Vector2 velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
    //    Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI, Main.MouseWorld.Y);
    //    for (int i = 0; i< 2; i++) 
    //    {
    //        startPos = Main.MouseWorld + new Vector2((i == 1 ? -1 : 1) * Main.rand.Next(100, 300), -500);
    //        velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
    //        Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI, Main.MouseWorld.Y);
    //    }
    //    return false;
    //}

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.CrimtaneBar, 5)
        .AddIngredient(ItemID.Ichor, 12)
        .AddIngredient(ItemID.SoulofNight, 6)
        .AddIngredient(ItemID.DarkShard)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
