using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 仲裁巨剑
/// </summary>
public class ArbitralSword : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Arbitral Sword");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(0, 15, 0, 0);

        Item.width = 88;
        Item.height = 88;

        //Item.scale = 1.1f;
        Item.crit = 7;
        Item.damage = 95;

        Item.knockBack = KnockBackLevel.BeHigher;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;

        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<ArbitralSwordProj>();
        Item.shootSpeed = 15f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        //Vector2 startPos = Main.MouseWorld + new Vector2(0, -500);
        //Vector2 velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
        //Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI, Main.MouseWorld.Y);
        //for (int i = 0; i< 2; i++) 
        //{
        //    startPos = Main.MouseWorld + new Vector2((i == 1 ? -1 : 1) * Main.rand.Next(100, 300), -500);
        //    velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
        //    Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI, Main.MouseWorld.Y);
        //}
        return true;
    }
    public override void AddRecipes() => CreateRecipe()
        //.AddIngredient(ModContent.ItemType<SunplateBlade>())
        .AddIngredient(ItemID.SoulofLight, 6) //光明之魂
        .AddIngredient(ItemID.BrokenHeroSword) //ItemID.CrystalShard 水晶碎块
        .AddIngredient(ItemID.HallowedBar, 15) //神圣锭  ItemID.LightShard 光明碎块
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
