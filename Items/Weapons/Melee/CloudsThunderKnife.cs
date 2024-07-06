using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 阴云雷切
/// </summary>
public class CloudsThunderKnife : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2, 15, 0);

        Item.width = 50;
        Item.height = 50;

        Item.damage = 21;
        Item.crit = 10;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 1;
        Item.useAnimation = 32;
        Item.useTime = 32;
        Item.UseSound = SoundID.Item77; //镰刀

        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<whirlpoolProj>();
        Item.shootSpeed = 7f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<RaySoul>(), 4)
        .AddIngredient(ItemID.RainCloud, 7)
        .AddIngredient(ItemID.Cloud, 5)
        .AddTile(TileID.Anvils)
        .Register();

}
