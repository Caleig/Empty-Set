using Microsoft.Xna.Framework;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

public class MeteoriteBow : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Meteorite Bow");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 7, 50);

        Item.width = 28; //已精确测量
        Item.height = 58;

        Item.damage = 13;
        Item.crit = 0;
        Item.scale = 0.8f;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item5;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.useTime = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Ranged;
        Item.autoReuse = true;
        Item.noMelee = true;

        Item.useAmmo = AmmoID.Arrow;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 12f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (type != ProjectileID.WoodenArrowFriendly)
            return base.CanUseItem(player);
        Projectile.NewProjectile(source, position, velocity, ProjectileID.JestersArrow, damage, knockback,
            player.whoAmI);
        return false;
    }

    //public override Vector2? HoldoutOffset() => new Vector2(-6, -1);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.FallenStar, 4)
        .AddIngredient(ItemID.MeteoriteBar, 3)
        .AddTile(TileID.Anvils) //铁砧
        .Register();
}