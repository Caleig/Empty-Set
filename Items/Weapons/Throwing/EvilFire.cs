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
/// 邪炎
/// </summary>
public class EvilFire : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Yellow;
        Item.value = Item.sellPrice(0, 25, 0, 0);

        Item.width = 48; //已精确测量
        Item.height = 50;

        Item.damage = 65;
        Item.crit = 12;

        Item.knockBack = 7;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 15;
        Item.useAnimation = 15;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item19;
        Item.shoot = ModContent.ProjectileType<EvilFireProj>();
        Item.shootSpeed = 60f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 2;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<Flamtaint>(4)
        .AddIngredient<FelShadowBar>(3)
        .AddIngredient<ArdorBlossom>()
        .AddIngredient<CorruptShard>(10)
        .AddIngredient(ItemID.CursedFlame,12)
        .AddTile(TileID.DemonAltar)
        .Register();
}
