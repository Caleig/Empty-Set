using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 充能激光发射器
/// </summary>
public class ChargedLaserEmitter : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.damage = 13;
        Item.knockBack = KnockBackLevel.None;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 60, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item12;
        Item.mana = 8;

        Item.shoot = ModContent.ProjectileType<LaserProj>();
        Item.shootSpeed = 14;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 13)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 12)
        .AddTile(TileID.Anvils)
        .Register();
}