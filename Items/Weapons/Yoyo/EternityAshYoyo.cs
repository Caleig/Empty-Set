using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Yoyo;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Yoyo;

/// <summary>
/// 恒灰悠悠球
/// </summary>
public class EternityAshYoyo : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Eternity Ash Yoyo");
        
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.WoodYoyo);

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 3, 0, 0);

        Item.width = 30; //已精确测量
        Item.height = 26;

        Item.damage = 33;
        Item.crit = 0;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useStyle = ItemUseStyleID.Shoot;

        Item.shoot = ModContent.ProjectileType<EternityAshYoyoProj>();
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(),15)
        .AddTile(TileID.Anvils)
        .Register();

}