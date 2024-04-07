using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 林翠投矛
/// </summary>
public class JungleThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
       CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 0, 50);

        Item.width = 52; //已精确测量
        Item.height = 52;

        Item.damage = 25;
        Item.crit = 9;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<JungleThrowingSpearProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Stinger, 12) //毒刺
        .AddIngredient(ItemID.JungleSpores, 5) //丛林孢子
        .AddTile(TileID.WorkBenches)
        .Register();
}