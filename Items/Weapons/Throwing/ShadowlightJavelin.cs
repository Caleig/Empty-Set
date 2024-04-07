using EmptySet.Items.Materials;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 暗影焰投矛
/// </summary>
public class ShadowlightJavelin : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Shadowlight Javelin");
        
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 84;
        Item.height = 84;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 9, 0, 0);
        Item.damage = 51;
        Item.crit = 7;
        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.DamageType = DamageClass.Throwing;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<ShadowlightJavelinProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<ShadowlightSparkle>(), 8)
        .AddIngredient(ItemID.HellstoneBar, 9)
        //.AddIngredient(ItemID.ShadowFlameKnife)
        .AddTile(TileID.Anvils)
        .Register();
}