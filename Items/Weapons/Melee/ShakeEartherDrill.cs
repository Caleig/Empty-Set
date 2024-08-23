using EmptySet.Items.Materials;
using EmptySet.Projectiles.Held;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 撼地钻头
/// </summary>
public class ShakeEartherDrill : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Shake Earther Drill");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 80;
        Item.height = 80;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 17;
        Item.knockBack = KnockBackLevel.None;
        Item.crit = 6;
        Item.value = Item.sellPrice(0,0,75,0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item23;

        Item.channel = true;
        Item.noMelee = true;
        Item.autoReuse = true;
        Item.noUseGraphic = true;
        Item.pick = 55;

        Item.shoot = ModContent.ProjectileType<ShakeEartherDrillProj>();
        Item.shootSpeed = 20f;

    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 20)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 5)
        .AddTile(TileID.Anvils)
        .Register();
}