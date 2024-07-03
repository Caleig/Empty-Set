using EmptySet.Common.Players;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 溢能激光发射器
/// </summary>
public class ChargedLaserEmitter : ModItem
{
    private int EnergyCost = 15;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.damage = 13;
        Item.knockBack = KnockBackLevel.None;
        Item.crit = 4-4;
        Item.value = Item.sellPrice(0, 0, 9, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item12;
        Item.mana = 8;

        Item.shoot = ModContent.ProjectileType<ChargedLaserProj>();
        Item.shootSpeed = 14;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
        if (Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
            {
                var mouse = Main.MouseWorld;
                var scr = Main.screenPosition;
                 Projectile.NewProjectile(source, new Vector2(mouse.X,scr.Y), default, ModContent.ProjectileType<DrillMissile>(), damage*2, knockback, -1,mouse.X,mouse.Y);
            }
        }
        return false;
    }
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<MetalFragment>(10)
        .AddIngredient<ChargedCrystal>(5)
        .AddTile(TileID.Anvils)
        .Register();
}