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
/// 狂炎
/// </summary>
public class FrenziedFlame : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Red;
        Item.value = Item.sellPrice(0, 35, 0, 0);

        Item.width = 48; //已精确测量
        Item.height = 50;

        Item.damage = 120;
        Item.crit = 27;

        Item.knockBack = 7;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 10;
        Item.useAnimation = 10;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item19;
        Item.shoot = ModContent.ProjectileType<FrenziedFlameProj>();
        Item.shootSpeed = 80f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 2;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Ectoplasm,10)
        .AddIngredient<EvilFire>()
        .AddIngredient(ItemID.SoulofNight,10)
        .AddTile(412)
        .Register();
}
