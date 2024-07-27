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
/// 红焰
/// </summary>
public class ArdorBlossom : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        Item.width = 46; //已精确测量
        Item.height = 40;

        Item.damage = 66;
        Item.crit = 21;

        Item.knockBack = 3;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 21;
        Item.useAnimation = 21;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<ArdorBlossomProj>();
        Item.shootSpeed = 50f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 1;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.LivingFireBlock,20)
        .AddIngredient(ItemID.HellstoneBar, 5)
        .AddIngredient<MoltenDebris>(5)
        .AddIngredient<ViridSteel>()
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
