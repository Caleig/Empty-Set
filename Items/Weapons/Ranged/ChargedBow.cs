using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 充能弓
/// </summary>
public class ChargedBow : ModItem
{
    private int _count = 1;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Charged Bow");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 62;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 11;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 75, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item5;

        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 7;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
        if (_count == 2) 
        {
            SoundEngine.PlaySound(SoundID.Item75, player.position);
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ChargedArrowProj>(), damage, knockback, Main.myPlayer);
            _count = 0;
        }
        _count++;
        return false;
    }

    public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 15)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 10)
        .AddTile(TileID.Anvils)
        .Register();

}