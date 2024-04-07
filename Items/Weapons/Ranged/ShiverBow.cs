using Microsoft.Xna.Framework;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 寒颤弓
/// </summary>
public class ShiverBow : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Shiver Bow");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 0, 50);

        Item.width = 34; //已精确测量
        Item.height = 70;

        Item.damage = 9;
        Item.crit = 0;
        Item.scale = 0.6f;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = KnockBackLevel.TooLower;
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
        if (type != ProjectileID.WoodenArrowFriendly)
            return base.CanUseItem(player);
        Projectile.NewProjectile(source, position, velocity, ProjectileID.FrostburnArrow, damage, knockback,
            player.whoAmI);
        return false;

    }

    //public override Vector2? HoldoutOffset() =>
    //    new Vector2(4, -4);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.BorealWoodBow)//针叶木弓
        .AddIngredient(ItemID.Shiverthorn)//寒颤棘
        .AddIngredient(ItemID.IceBlock,25)//冰雪块
        .AddTile(TileID.WorkBenches)
        .Register();
}