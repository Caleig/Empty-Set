using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 日盘回旋镖
/// </summary>
public class SunplateBoomerang : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Crimtane Flying Blade");
        //Tooltip.SetDefault("Crimtane Flying Blade");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "猩红旋刃");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 30, 0);

        Item.width = 18; //已精确测量
        Item.height = 40;

        Item.damage = 15;
        Item.crit = 4;

        Item.knockBack = 4;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 24;
        Item.useAnimation = 24;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<SunplateBoomerangProj>();
        Item.shootSpeed = 15f;
    }

    public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 2;

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.WoodenBoomerang, 1)
        .AddIngredient(ItemID.SunplateBlock, 7)
        .AddIngredient(ItemID.Feather, 2)
        .AddTile(TileID.SkyMill)
        .Register();
}
