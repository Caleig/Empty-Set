using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 微型彗星雷
/// </summary>
public class MiniMeteoriteThunder : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Mini Meteorite Thunder");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 150;
    }

    public override void SetDefaults()
    {
        //Item.CloneDefaults(ItemID.Grenade);
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 10, 75);

        Item.width = 54; //已精确测量
        Item.height = 38;

        Item.damage = 23;
        Item.crit = 0;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = KnockBackLevel.Normal;
        Item.useAnimation = UseSpeedLevel.VerySlowSpeed;
        Item.useTime = UseSpeedLevel.VerySlowSpeed;
        Item.UseSound = SoundID.Item1;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.consumable = true;
        Item.noUseGraphic = true;
        Item.maxStack = 999;

        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<MiniMeteoriteThunderProj>();//ProjectileID.WoodenArrowFriendly
        Item.shootSpeed = 6f;
    }
    public override void AddRecipes() => CreateRecipe(50)
        .AddIngredient(ItemID.FallenStar, 2)
        .AddIngredient(ItemID.MeteoriteBar, 3)
        .AddTile(TileID.Anvils)
        .Register();
}
