using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 深谙魔炮
/// </summary>
public class AbyssCannon : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("FlightLeaves");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 66;
        Item.height = 44;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 28;
        Item.useAnimation = 28; // 武器使用动画的时间跨度，建议与useTime设置相同。
        Item.autoReuse = true; // 自动挥舞
        Item.DamageType = DamageClass.Magic; // 伤害类型
        Item.noMelee = true;
        Item.damage = 70;
        Item.mana = 15;
        Item.knockBack = 5;
        Item.crit = 5;
        Item.value = Item.sellPrice(0, 7, 0, 0);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item45;

        Item.shoot = ProjectileID.BlackBolt;
        Item.shootSpeed = 21;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-5f, 5f);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.DarkShard,2)
        .AddIngredient<FelShadowBar>(8)
        .AddIngredient<CorruptShard>(5)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
