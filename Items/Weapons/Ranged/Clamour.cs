using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 喧嚣
/// </summary>
public class Clamour : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Clamour");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 64;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.ExtremeSpeed + 1;
        Item.useAnimation = UseSpeedLevel.ExtremeSpeed + 1;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 5;
        Item.knockBack = KnockBackLevel.None + 0.5f;
        Item.crit = 4 - 4;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item11;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 9f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var offset = Main.rand.NextFloat(MathHelper.ToRadians(-3f), MathHelper.ToRadians(3f));
        var vel = velocity.RotatedBy(offset);
        Projectile.NewProjectile(source, position, vel, type, damage, knockback, Main.myPlayer);
        return false;
    }
    public override void OnConsumeAmmo(Item ammo, Player player)
    {
        if (Main.rand.NextBool(10))
            ammo.stack--;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Minishark)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 20)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilBar),4)
        .AddTile(TileID.Anvils)
        .Register();
}