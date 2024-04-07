using EmptySet.Projectiles.Summon;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Summon;

/// <summary>
/// 钨短刀
/// </summary>
public class TungstenKnife: ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Self-defense knife");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 9, 35);

        Item.width = 36; //已精确测量
        Item.height = 36;

        Item.crit = 4;
        Item.damage = 8;

        Item.knockBack = KnockBackLevel.None + 4f;
        Item.useTime = UseSpeedLevel.SuperFastSpeed + 5;
        Item.useAnimation = UseSpeedLevel.SuperFastSpeed + 5;

        Item.DamageType = DamageClass.Summon;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.TungstenKnife>();
        Item.shootSpeed = 4f;
    }
    public override void HoldItem(Player player)
    {
        player.statDefense += (int)player.maxMinions * 4;
        base.HoldItem(player);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.TungstenBar, 4)
        .AddTile(TileID.WorkBenches)
        .Register();
}
