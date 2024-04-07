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
/// 古遗投矛
/// </summary>
public class AncientThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Ancient Throwing Spear");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 15, 0);

        Item.width = 56; //已精确测量
        Item.height = 56;

        Item.damage = 31;
        Item.crit = 1;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<AncientThrowingSpearProj>();
        Item.shootSpeed = 7f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(),4)
        .AddIngredient(ItemID.Bone,10)
        .AddTile(TileID.Anvils)
        .Register();
}