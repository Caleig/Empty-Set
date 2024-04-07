using EmptySet.Projectiles.Summon;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Summon;

/// <summary>
/// 铂金短刀
/// </summary>
public class PlatinumKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Self-defense knife");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 19, 0);

        Item.width = 36; //已精确测量
        Item.height = 36;

        Item.crit = 0;
        Item.damage = 11;

        Item.knockBack = KnockBackLevel.None + 5f;
        Item.useTime = UseSpeedLevel.SuperFastSpeed + 3;
        Item.useAnimation = UseSpeedLevel.SuperFastSpeed + 3;

        Item.DamageType = DamageClass.Summon;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.PlatinumKnife>();
        Item.shootSpeed = 5f;
    }
    public override void HoldItem(Player player)
    {
        player.moveSpeed += player.maxMinions * 0.4f;
        base.HoldItem(player);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.PlatinumBar, 4)
        .AddTile(TileID.Anvils)
        .Register();
}
