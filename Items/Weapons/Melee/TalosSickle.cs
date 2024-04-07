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
/// 烁金镰
/// </summary>
public class TalosSickle : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 78;
        Item.height = 72;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 125;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 12 - 4;
        Item.value = Item.sellPrice(0, 15, 0, 0);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item71;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<TalosSickleProj>();
        Item.shootSpeed = 5;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 20)
        .AddIngredient(ItemID.GoldBar, 25)
        .AddIngredient(ItemID.Ruby, 5)
        .AddIngredient(ItemID.CrystalShard, 15)
        .AddIngredient(ItemID.Ectoplasm, 5)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
