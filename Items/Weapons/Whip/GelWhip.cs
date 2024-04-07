using EmptySet.Items.Materials;
using EmptySet.Projectiles.Whip;
using EmptySet.Utils;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Whip;

public class GelWhip : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Gel Whip");
        
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 40;

        Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(0, 0, 0, 30));

        Item.damage = 13;
        Item.knockBack = KnockBackLevel.TooLower - 1.5f;
        Item.DamageType = DamageClass.SummonMeleeSpeed;
        Item.crit = 4;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item152;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<GelWhipProj>();
        Item.shootSpeed = 8f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<HardenedGel>(7)
        .AddIngredient<GoldFragments>(4)
        .AddTile(TileID.Anvils)
        .Register();

}