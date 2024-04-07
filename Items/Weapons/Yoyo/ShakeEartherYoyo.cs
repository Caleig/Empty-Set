using EmptySet.Items.Materials;
using EmptySet.Projectiles.Yoyo;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Yoyo;

/// <summary>
/// 震撼悠悠球
/// </summary>
internal class ShakeEartherYoyo : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("ShakeEarther Yoyo");
        
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 26;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.damage = 18;
        Item.knockBack = KnockBackLevel.BeHigher;
        Item.crit = 5;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.rare = ItemRarityID.Blue;

        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.channel = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;

        Item.shoot = ModContent.ProjectileType<ShakeEartherYoyoProj>();
        Item.shootSpeed = 12f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 10)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 5)
        .AddTile(TileID.Anvils)
        .Register();

}