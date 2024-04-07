using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Throwing;
using EmptySet.Projectiles.Weapons.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 冰川之破
/// </summary>
public class 冰川之破 : ModItem
{
    public override void SetStaticDefaults()
    {
         CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 23, 0, 0);

        Item.width = 62;
        Item.height = 62;

        Item.damage = 35;
        Item.crit = 10;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = UseSpeedLevel.VeryFastSpeed - 1;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed - 1;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
            
        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<冰川之破弹幕>();
        Item.shootSpeed = 10f; //1 update
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-5f)), type, damage, knockback, player.whoAmI);
        Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(5f)), type, damage, knockback, player.whoAmI);
        return false;
    }
}
