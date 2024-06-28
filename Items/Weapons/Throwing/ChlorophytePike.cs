using Terraria;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 叶绿鉸
/// </summary>
public class ChlorophytePike : ModItem
{
    public override void SetStaticDefaults()
    {
         CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 52;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;
        Item.DamageType = DamageClass.Throwing;
        Item.damage = 45;
        Item.knockBack = KnockBackLevel.TooLower;
        Item.crit = 17;
        Item.value = Item.sellPrice(0, 4, 92, 0);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item1;

        Item.noMelee = true;
        Item.autoReuse = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<ChlorophytePikeProj>();
        Item.shootSpeed = 9;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.ChlorophyteBar, 13)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
