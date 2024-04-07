using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 恒灰法球
/// </summary>
public class EternityAshMagicBall : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Eternity Ash Magic Ball");
       CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 48;
        Item.height = 52;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed; // 武器使用动画的时间跨度，建议与useTime设置相同。
        Item.autoReuse = true; // 自动挥舞
        Item.DamageType = DamageClass.Magic; // 伤害类型
        Item.noMelee = true;
        Item.damage = 24;
        Item.mana = 9;
        Item.knockBack = KnockBackLevel.TooLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 2, 15, 0);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item8;

        Item.shoot = ModContent.ProjectileType<EternityAshMagicBallProj>();
        Item.shootSpeed = 13;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 3)
        .AddIngredient(ModContent.ItemType<FlightLeaves>())
        .AddTile(TileID.Anvils)
        .Register();
}
