using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 钨钢剑
/// </summary>
public class TungstenSteelSword : ModItem
{
    public override void SetStaticDefaults()
    { 
   
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 2, 60, 0);

        Item.width = 68;
        Item.height = 68;

        //Item.scale = 1.1f;
        Item.crit = 4-4;
        Item.damage = 35;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = 33;
        Item.useAnimation = 33;

        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<TungstenSteelSwordProj>();
        Item.shootSpeed = 15f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<TungstenSteelBar>(), 12)
        .AddTile(TileID.HeavyWorkBench)
        .AddTile(TileID.Anvils)
        .Register();
}
