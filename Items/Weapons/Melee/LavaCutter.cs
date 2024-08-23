using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 熔岩裂切
/// </summary>
public class LavaCutter : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 37;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 2, 35);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<LavaFissureCuttingProj>();
        Item.shootSpeed = 5;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, Main.MouseScreen + Main.screenPosition, Vector2.Zero, type, damage, knockback, Main.myPlayer);
        return false;
    }
}