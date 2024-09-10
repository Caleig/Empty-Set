using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 钨翠
/// </summary>
public class Tungstemerald:ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Mini Fireball Gun");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 21;
        Item.useAnimation = 21;
        Item.DamageType = DamageClass.Magic;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.damage = 28;
        Item.mana = 7;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 2, 35);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item43;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.channel = true;
        Item.shoot = ModContent.ProjectileType<TungstenSteelProj>();
        Item.shootSpeed = 14;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 14)
        .AddTile(TileID.HeavyWorkBench)//重型工作台
        .AddTile(TileID.Anvils)
        .Register();

}

