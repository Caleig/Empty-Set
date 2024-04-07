using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
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
/// 影恒
/// </summary>
public class LastingShadow : ModItem
{
    public override void SetStaticDefaults()
    {
       CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        Item.width = 52;
        Item.height = 52;

        Item.damage = 35;
        Item.crit = 4-4;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed + 3;
        Item.useTime = UseSpeedLevel.VeryFastSpeed + 3;
        Item.useStyle = ItemUseStyleID.Swing;
        //Item.scale = 0.8f;

        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useTurn = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<ShadowBall>(); //ProjectileID.WoodenArrowFriendly
        Item.shootSpeed = 20f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Vector2 startPos = Main.MouseWorld + new Vector2(0, -700);
        Vector2 velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
        Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI,Main.MouseWorld.Y);
        for (int i = 0; i < 2; i++)
        {
            startPos = Main.MouseWorld + new Vector2((i == 1 ? -1 : 1) * Main.rand.Next(200, 500), -700);
            velo = (Main.MouseWorld - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
            Projectile.NewProjectile(source, startPos, velo, type, damage, knockback, player.whoAmI, Main.MouseWorld.Y);
        }

        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.DarkShard,2)
        .AddIngredient(ItemID.SoulofNight, 10)
        .AddIngredient<FelShadowBar>(6)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
