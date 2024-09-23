using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
using EmptySet.Tiles;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// ON157
/// </summary>
public class ON157 : ModItem
{
    private int EnergyCost = 9;
    public override void SetStaticDefaults()
    {CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 100;
        Item.height = 36;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 36;
        Item.knockBack = 6;
        Item.crit = 6;
        Item.value = Item.sellPrice(0, 6, 6, 6);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item38;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 15f;
        Item.useAmmo = AmmoID.Bullet;
    }
    //public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.OnyxBlaster)
        .AddIngredient(ModContent.ItemType<WindStorm>(), 1)
        .AddIngredient(ModContent.ItemType<SoulOfPolymerization>(), 2)
        .AddTile(ModContent.TileType<FusioninstrumentTile>())
        .Register();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-3f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-1f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.One)*21f, ModContent.ProjectileType<SpilledProj>(), damage, knockback, Main.myPlayer);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(1f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(3f)), type, damage, knockback, player.whoAmI);
            return false;
        }
        if(Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
            {
                Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.One)*3f, ModContent.ProjectileType<MissileProj>(), damage, knockback, Main.myPlayer);
            }
        }

        return true;
    }
    public override Vector2? HoldoutOffset() => new Vector2(-15f, 0);
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
}
