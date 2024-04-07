using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 恒灰飞刃
/// </summary>
public class EternityAshFlyingBlade : ModItem
{
    public override void SetStaticDefaults()
    {
    CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 3, 25, 0);

        Item.width = 56; //已精确测量
        Item.height = 48;

        Item.damage = 35;
        Item.crit = 0;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<EternityAshFlyingBladeProj>();
        Item.shootSpeed = 10f;
    }

    //public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 5;

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 15)
        .AddTile(TileID.Anvils)
        .Register();
}