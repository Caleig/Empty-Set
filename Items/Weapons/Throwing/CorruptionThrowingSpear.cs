using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 腐化投矛
/// </summary>
public class CorruptionThrowingSpear : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Corruption Throwing Spear");
        //Tooltip.SetDefault("Corruption Throwing Spear");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "腐化投矛");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 13, 0);

        Item.width = 44; //已精确测量
        Item.height = 44;

        Item.damage = 19;
        Item.crit = 1;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CorruptionThrowingSpearProj>();
        Item.shootSpeed = 10f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.DemoniteBar, 4) //魔矿锭
        .AddIngredient(ItemID.ShadowScale, 5) //暗影鳞片
        .AddTile(TileID.Anvils)
        .Register();
}