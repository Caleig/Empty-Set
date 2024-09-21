using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 奈尔丽增
/// </summary>
public class Nihilism : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 70;
        Item.height = 70;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 130;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 12 - 4;
        Item.value = Item.sellPrice(0, 2, 3, 6);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item71;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<TalosSickleProj>();
        Item.shootSpeed = 5;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 15)
        .AddIngredient(1508, 2)
        .AddTile(TileID.MythrilAnvil)
        .Register();
    public override bool CanUseItem(Player player)
    {

        return true;

    }

    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        if (Main.rand.NextBool(1))
        {
            Dust.NewDustDirect(new Vector2(0, -100).RotatedBy(player.itemRotation + MathHelper.ToRadians(30 * player.direction)) + player.Center, 2, 2, DustID.Silver).noGravity = true;
            Dust.NewDustDirect(new Vector2(0, -100).RotatedBy(player.itemRotation + MathHelper.ToRadians(30 * player.direction)) + player.Center, 2, 2, DustID.SilverCoin).noGravity = true;
        }
        base.UseItemHitbox(player, ref hitbox, ref noHitbox);
    }
}
