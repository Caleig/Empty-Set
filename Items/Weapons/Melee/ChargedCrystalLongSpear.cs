using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Held;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 能晶长枪
/// </summary>
public class ChargedCrystalLongSpear : ModItem
{
    private int thisProjId = -1;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Charged Crystal Long Spear");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 86;
        Item.height = 88;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 15;
        Item.knockBack = KnockBackLevel.Normal + 1f;
        Item.crit = 3;
        Item.value = Item.sellPrice(0, 0, 65, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;

        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shoot = ModContent.ProjectileType<ChargedCrystalLongSpearProj>();
        Item.shootSpeed = 6f;

        Item.channel = true;
    }

    public override bool AltFunctionUse(Player player) => true;

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var i = thisProjId;
        
        if (Main.mouseRight && (i == -1 || Main.projectile[i].type != ModContent.ProjectileType<ChargedCrystalLongSpearTurnProj>() || Main.projectile[i] == null || !Main.projectile[i].active))
        {
            Item.useStyle = ItemUseStyleID.Swing;
            thisProjId = Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<ChargedCrystalLongSpearTurnProj>(), Item.damage, Item.knockBack, player.whoAmI);
        }
        else if (Main.mouseLeft) 
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        }
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<MetalFragment>(), 14)
        .AddIngredient(ModContent.ItemType<ChargedCrystal>(), 9)
        .AddTile(TileID.Anvils)
        .Register();
}