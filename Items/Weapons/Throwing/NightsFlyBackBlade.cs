using EmptySet.Common.Systems;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 永夜飞刃
/// </summary>
public class NightsFlyBackBlade : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        Item.width = 50; //已精确测量
        Item.height = 50;

        Item.damage = 47;
        Item.crit = 5;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<NightsFlyBackBladeProj>();
        Item.shootSpeed = 16f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 1;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<EternityAshFlyingBlade>()
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilFlyingBlade))
        .AddIngredient(ItemID.ThornChakram) //荆棘旋刃
        .AddIngredient(ItemID.Flamarang) //狱焰回旋镖
        .AddTile(TileID.DemonAltar)
        .Register();
}