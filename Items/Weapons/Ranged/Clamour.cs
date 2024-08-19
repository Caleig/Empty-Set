using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
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
/// 喧嚣
/// </summary>
public class Clamour : ModItem
{
    private int EnergyCost = 5;
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
        Item.useTime = UseSpeedLevel.ExtremeSpeed + 3;
        Item.useAnimation = UseSpeedLevel.ExtremeSpeed + 3;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 5;
        Item.knockBack = KnockBackLevel.None + 0.5f;
        Item.crit = 4 - 4;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item11;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 9f;
        Item.useAmmo = AmmoID.Bullet;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
        var offset = Main.rand.NextFloat(MathHelper.ToRadians(-3f), MathHelper.ToRadians(3f));
        var vel = velocity.RotatedBy(offset);
        Projectile.NewProjectile(source, position, vel, type, damage, knockback, Main.myPlayer);
        return false;
        }
        if (Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
            {
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-5f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(-2f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(5f)), type, damage, knockback, player.whoAmI);
            Projectile.NewProjectileDirect(source, position, velocity.RotatedBy(MathHelper.ToRadians(2f)), type, damage, knockback, player.whoAmI);
            }
        }
        return false;
    }

    public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Minishark)
        .AddIngredient(ModContent.ItemType<Easyparts>(), 3)
        .AddIngredient(324, 1)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
