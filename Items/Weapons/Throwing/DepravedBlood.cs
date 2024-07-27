using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 堕血
/// </summary>
public class DepravedBlood : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(0, 30, 0, 0);

        Item.width = 64; //已精确测量
        Item.height = 60;

        Item.damage = 150;
        Item.crit = 27;

        Item.knockBack = 6;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 36;
        Item.useAnimation = 36;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item71;
        Item.shoot = ModContent.ProjectileType<DepravedBloodProj>();
        Item.shootSpeed = 60f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 1;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<BloodShadow>(10)
        .AddIngredient(ItemID.Ichor,12)
        .AddIngredient<ArdorBlossom>()
        .AddIngredient<Flamtaint>(4)
        .AddIngredient<FelShadowBar>(3)
        .AddTile(TileID.DemonAltar)
        .Register();
}
