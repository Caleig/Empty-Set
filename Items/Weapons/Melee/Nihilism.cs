using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 奈尔丽增
/// </summary>
public class Nihilism : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 70;
        Item.height = 70;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 130;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 12 - 4;
        Item.value = Item.sellPrice(0, 2, 3, 6);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item71;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<TalosSickleProj>();
        Item.shootSpeed = 5;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 15)
        .AddIngredient(1508, 2)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
