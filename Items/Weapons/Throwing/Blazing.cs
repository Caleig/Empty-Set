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
/// 耀炎
/// </summary>
public class Blazing : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Red;
        Item.value = Item.sellPrice(0, 36, 0, 0);

        Item.width = 46; //已精确测量
        Item.height = 40;

        Item.damage = 200;
        Item.crit = 30;

        Item.knockBack = 3;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 21;
        Item.useAnimation = 21;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<BlazingProj>();
        Item.shootSpeed = 70f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 1;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(3458,10)
        .AddIngredient(2766, 15)
        .AddIngredient<ArdorBlossom>()
        .AddTile(TileID.LihzahrdAltar)
        .Register();
}
