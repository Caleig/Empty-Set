using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 碧钢
/// </summary>
public class ViridSteel : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Night's Fly Back Blade");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 5, 0, 0);

        Item.width = 48; //已精确测量
        Item.height = 40;

        Item.damage = 30;
        Item.crit = 5;

        Item.knockBack = 2;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 25;
        Item.useAnimation = 25;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<ViridSteelProj>();
        Item.shootSpeed = 40f;
    }
    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < 1;
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 15)
        .AddTile(TileID.HeavyWorkBench)
        .AddTile(TileID.Anvils)
        .Register();
}
