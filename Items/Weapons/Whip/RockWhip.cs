using EmptySet.Projectiles.Whip;
using EmptySet.Utils;
using EmptySet.Items.Materials;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Whip;

/// <summary>
/// 岩流鞭
/// </summary>
public class RockWhip : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Rock Whip");
        
        //Tooltip.SetDefault("Suck the shit");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "抽他丫的");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 40;

        Item.SetShopValues(ItemRarityColor.LightRed4, Item.buyPrice(0, 3, 0, 0));

        Item.damage = 45;
        Item.knockBack = KnockBackLevel.TooLower - 1f;
        Item.DamageType = DamageClass.SummonMeleeSpeed;
        Item.crit = 4-4;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item152;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;

        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<RockWhipProj>();
        Item.shootSpeed = 8f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.BoneWhip)
        .AddIngredient<RockFragments>(7)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}