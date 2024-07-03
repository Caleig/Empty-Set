using EmptySet.Common.Players;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria.DataStructures;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 能晶投矛
/// </summary>
public class ChargedCrystalSpear : ModItem
{
    private int EnergyCost = 10;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 150;
    }
    public override void SetDefaults()
    {
        Item.width = 60;
        Item.height = 60;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.SlowSpeed-1;
        Item.useAnimation = UseSpeedLevel.SlowSpeed-1;
        Item.DamageType = DamageClass.Throwing;
        Item.damage = 23;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.crit = 4-4;
        Item.value = 100;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.maxStack = 999;

        Item.noMelee = true;
        Item.autoReuse = true;
        Item.consumable = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<ChargedCrystalSpearProj>();
        Item.shootSpeed = 9f;
    }

    public override void AddRecipes() => CreateRecipe(40)
        .AddIngredient<MetalFragment>(10)
        .AddIngredient<ChargedCrystal>(2)
        .AddTile(TileID.Anvils)
        .Register();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
            return true;
        if (Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
                Projectile.NewProjectile(source, position + new Vector2(0, -12), velocity, ModContent.ProjectileType<ChargedCrystalSpearProj2>(), damage, knockback, Main.myPlayer);
        }
        return false;

    }

    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
}