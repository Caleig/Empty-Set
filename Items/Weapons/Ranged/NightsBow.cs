using EmptySet.Common.Systems;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 永夜弓
/// </summary>
public class NightsBow : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.width = 48; //已精确测量
        Item.height = 100;

        Item.damage = 37;
        Item.crit = 4;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.autoReuse = false;
        Item.channel = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        //Item.UseSound = SoundID.Item5; //不注释 最开始的点击会有声音 但是没有箭
        Item.shoot = ModContent.ProjectileType<NightsBowProj>();
        Item.shootSpeed = 6f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, player.Center, Vector2.Zero, Item.shoot, damage, knockback, player.whoAmI, 0, 60);
        return false;
    }


    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.BeesKnees) //蜂膝弓
        .AddIngredient(ItemID.MoltenFury) //熔火之怒
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilBow))
        .AddTile(TileID.DemonAltar) //祭坛
        .Register();

}
