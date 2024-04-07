using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 猩红旋刃
/// </summary>
public class CrimtaneFlyingBlade : ModItem
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
        Item.value = Item.sellPrice(0, 0, 10, 0);

        Item.width = 38; //已精确测量
        Item.height = 38;

        Item.damage = 16;
        Item.crit = 1;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CrimtaneFlyingBladeProj>();
        Item.shootSpeed = 11f;
    }

    public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 1;

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.CrimtaneBar, 6)
        .AddIngredient(ItemID.TissueSample, 5) //组织样本
        .AddTile(TileID.Anvils)
        .Register();
}