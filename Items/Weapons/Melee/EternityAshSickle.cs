using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 恒灰镰刀
/// </summary>
public class EternityAshSickle : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2, 15, 0);

        Item.width = 54;
        Item.height = 52;

        Item.damage = 35;
        Item.crit = 10;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = KnockBackLevel.Normal;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.UseSound = SoundID.Item71; //镰刀

        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<EternityAshSickleProj>();
        Item.shootSpeed = 7f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 12)
        .AddIngredient(ModContent.ItemType<JungleSickle>())
        .AddTile(TileID.Anvils)
        .Register();
}