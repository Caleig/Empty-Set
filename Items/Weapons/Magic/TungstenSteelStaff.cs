using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 钨钢法杖
/// </summary>
public class TungstenSteelStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2, 10,0);

        Item.width = 46;
        Item.height = 40;

        Item.mana = 7;
        Item.crit = 4-4;
        Item.damage = 25;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = UseSpeedLevel.FastSpeed+2;
        Item.useAnimation = UseSpeedLevel.FastSpeed+2;

        Item.DamageType = DamageClass.Magic;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item43; //使用法杖

        Item.noMelee = true;
        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<TungstenSteelStaffProj>();
        Item.shootSpeed = 12f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 10)
        .AddTile(TileID.HeavyWorkBench)//重型工作台
        .AddTile(TileID.Anvils)
        .Register();

}
