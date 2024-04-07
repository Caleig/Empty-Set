using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 血腥投矛
/// </summary>
public class CrimtaneThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Crimtane Throwing Spear");
        //Tooltip.SetDefault("Crimtane Throwing Spear");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "血腥投矛");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 12, 0);

        Item.width = 46; //已精确测量
        Item.height = 46;

        Item.damage = 23;
        Item.crit = 3;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CrimtaneThrowingSpearProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.CrimtaneBar,6)
        .AddIngredient(ItemID.TissueSample,5) //组织样本
        .AddTile(TileID.Anvils)
        .Register();
}