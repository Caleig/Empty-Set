using EmptySet.Common.Systems;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 碎星飞盘
/// </summary>
public class BrokenStarDisc : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 23, 0, 0);

        Item.width = 48; //已精确测量
        Item.height = 48;

        Item.damage = 62;
        Item.crit = 4 - 4;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<BrokenStarDiscProj>();
        Item.shootSpeed = 9f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.MeteoriteBar,6)
        .AddIngredient(ItemID.FallenStar, 3)
        .AddIngredient(ItemID.CrystalShard,10)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.TitaniumOrAdamantite),5)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}