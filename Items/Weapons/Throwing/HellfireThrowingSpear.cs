using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 狱炎投矛
/// </summary>
public class HellfireThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 30, 0);

        Item.width = 50; //已精确测量
        Item.height = 50;

        Item.damage = 45;
        Item.crit = 0;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<HellfireThrowingSpearProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.HellstoneBar, 12)
        .AddTile(TileID.Anvils)
        .Register();
}
