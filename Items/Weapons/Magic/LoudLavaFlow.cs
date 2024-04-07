using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Common.Effects.Item;
using EmptySet.Projectiles.Held;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 熔炎啸流
/// 问题<br/>
/// 左键弹幕：在Y方向  散发最小 在X方向 散发最大<br/>
/// </summary>
public class LoudLavaFlow : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 68;
        Item.height = 70;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.DamageType = DamageClass.Magic;
        Item.knockBack = KnockBackLevel.None;
        Item.damage = 37;
        Item.mana = 11;
        Item.crit = 10;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.channel = true;
        Item.shoot = ModContent.ProjectileType<LoudLavaFlowProj>();
        Item.shootSpeed = 9;
        Item.staff[Item.type] = true;
    }

    public override bool AltFunctionUse(Player player) => true;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseRight)
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LoudLavaFlowHeldProj>(), 70, KnockBackLevel.BeHigher, player.whoAmI);
        else if (Main.mouseLeft)
        {
            var isDouble = player.GetModPlayer<LoudLavaFlowEffect>().IsDouble;
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LoudLavaFlowProj>(), isDouble ? damage*2:damage, knockback, player.whoAmI);
            player.GetModPlayer<LoudLavaFlowEffect>().IsDouble = false;
        }
        return false;
    }

    public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
    {
        if (Main.mouseRight)
            mult *= 18.1f;
    }
}
