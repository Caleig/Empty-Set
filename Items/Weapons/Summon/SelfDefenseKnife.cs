using EmptySet.Projectiles.Summon;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Summon;

/// <summary>
/// 防身小刀
/// </summary>
public class SelfDefenseKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Self-defense knife");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 0, 5);

        Item.width = 36; //已精确测量
        Item.height = 36;

        Item.crit = 4 - 4;
        Item.damage = 5;

        Item.knockBack = KnockBackLevel.None + 1f;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;

        Item.DamageType = DamageClass.Summon;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true; 
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<SelfDefenseKnifeProj>();
        Item.shootSpeed = 5f;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Wood, 10)
        .AddIngredient(ItemID.StoneBlock, 20)
        .AddTile(TileID.WorkBenches)
        .Register();
}
