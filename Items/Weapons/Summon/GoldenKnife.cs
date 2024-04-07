using EmptySet.Projectiles.Summon;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Summon;

/// <summary>
/// 金短刀
/// </summary>
public class GoldenKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Self-defense knife");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 12, 0);

        Item.width = 36; //已精确测量
        Item.height = 36;

        Item.crit = 0;
        Item.damage = 10;

        Item.knockBack = KnockBackLevel.None + 5f;
        Item.useTime = UseSpeedLevel.SuperFastSpeed + 3;
        Item.useAnimation = UseSpeedLevel.SuperFastSpeed + 3;

        Item.DamageType = DamageClass.Summon;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.UseSound = SoundID.Item1; //挥剑

        Item.autoReuse = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.GoldenKnife>();
        Item.shootSpeed = 5f;
    }
    public override void HoldItem(Player player)
    {
        int i = (int)(player.maxMinions / 3);
        if(i <= 2) {
            player.lifeRegen += i;
        }
        else
        {
            i = 2;
        }
        base.HoldItem(player);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.PlatinumBar, 4)
        .AddTile(TileID.Anvils)
        .Register();
}
