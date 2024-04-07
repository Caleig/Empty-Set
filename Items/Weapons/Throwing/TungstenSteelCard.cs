using EmptySet.Items.Materials;
using EmptySet.Projectiles.Throwing;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 钨钢牌
/// </summary>
public class TungstenSteelCard : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Tungsten Steel Card");
        
        //Tooltip.SetDefault("Spear throwing becomes extremely fast after a period of time.");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "投矛扔出一段时间后会变得极快");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 2, 0);

        Item.width = 18;
        Item.height = 30;

        Item.damage = 20;
        Item.crit = 4-4;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed-2;
        Item.useAnimation = UseSpeedLevel.NormalSpeed-2;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<TungstenSteelCardProj>();
        Item.shootSpeed = 30f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 9)
        .AddTile(TileID.HeavyWorkBench)
        .AddTile(TileID.Anvils)
        .Register();
}

