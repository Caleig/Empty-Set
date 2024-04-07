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
/// 永夜投矛
/// </summary>
public class NightsThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Throwing Spear");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        Item.width = 76; //已精确测量
        Item.height = 76;

        Item.damage = 41;
        Item.crit = 8;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<NightsThrowingSpearProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<AncientThrowingSpear>())
        .AddIngredient(ModContent.ItemType<HellfireThrowingSpear>())
        .AddIngredient(ModContent.ItemType<JungleThrowingSpear>())
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilThrowingSpear))
        .AddTile(TileID.DemonAltar)
        .Register();
}