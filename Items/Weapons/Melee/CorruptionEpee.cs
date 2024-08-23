using System;
using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Weapons.Special;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 腐化重剑
/// </summary>
public class CorruptionEpee : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 15, 0, 0);

        Item.width = 68; //已精确测量
        Item.height = 68;

        Item.damage = 85;
        Item.crit = 5;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useAnimation = UseSpeedLevel.VerySlowSpeed;
        Item.useTime = UseSpeedLevel.VerySlowSpeed;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.scale = 0.8f;

        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useTurn = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CursedFlameBall>(); //ProjectileID.WoodenArrowFriendly
        Item.shootSpeed = 9f;
    }
    /*public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var age = (float) Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X);
        var offset = //3倍为默认
            ((float) Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X) +
             0 * MathHelper.Pi / 36f).ToRotationVector2(); //90
        var offset1 = ((age + 90 * MathHelper.Pi / 36f) * new Vector2(1, 0).Length()).ToRotationVector2() * 8;
        var offset2 = ((age - 90 * MathHelper.Pi / 36f) * new Vector2(-1, 0).Length()).ToRotationVector2() * 8;

        Projectile.NewProjectile(source,position + offset1, offset * 9, type, damage, knockback, player.whoAmI);
        Projectile.NewProjectile(source,position + offset2, offset * 9, type, damage, knockback, player.whoAmI);
        return false;
    }*/

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone) =>
        target.AddBuff(BuffID.CursedInferno, hit.Crit ? 15 * EmptySet.Frame : 3 * EmptySet.Frame);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.DemoniteBar, 5) //魔矿锭
        .AddIngredient(ItemID.CursedFlame, 12) //诅咒焰
        .AddIngredient(ItemID.SoulofNight, 6) //暗影之魂
        .AddIngredient(ItemID.DarkShard) //暗黑碎块
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
