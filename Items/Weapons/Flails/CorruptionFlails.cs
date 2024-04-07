using EmptySet.Items.Materials;
using EmptySet.Projectiles.Flails;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Flails;

/// <summary>
/// 深谙连枷
/// </summary>
public class CorruptionFlails : ModItem
{
    public override void SetStaticDefaults()
    {

        //Tooltip.SetDefault("Shoot multiple swords from the sky at the player's cursor position");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "从天上向玩家光标位置发射多枚剑气");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 4, 0, 0);

        Item.width = 88; //已精确测量
        Item.height = 88;

        Item.crit = 0;
        Item.damage = 57;

        Item.knockBack = KnockBackLevel.BeLower + 0.5f;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;

        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.autoReuse = true;
        Item.channel = true;

        Item.shoot = ModContent.ProjectileType<CorruptionFlailsProj>();
        Item.shootSpeed = 10f;
    }


    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.BallOHurt)
        .AddIngredient<FelShadowBar>(3)
        .AddIngredient<CorruptShard>(7)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}