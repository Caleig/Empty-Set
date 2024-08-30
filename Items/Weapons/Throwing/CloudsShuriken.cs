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
/// 阴云磁刃
/// </summary>
public class CloudsShuriken : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 1, 30, 0);

        Item.width = 46;
        Item.height = 46;

        Item.damage = 15;
        Item.crit = 4;

        Item.knockBack = 2;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 35;
        Item.useAnimation = 35;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item77;
        Item.shoot = ModContent.ProjectileType<CloudsShurikenProj>();
        Item.shootSpeed = 5f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<RaySoul>(), 3)
        .AddIngredient(ItemID.RainCloud, 10)
        .AddIngredient(ItemID.Cloud, 7)
        .AddTile(TileID.Anvils)
        .Register();

}

