using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 暗影焰十字架
/// proj 手持有伤害问题（弹幕位置问题？）    弹幕无伤害（哪里的source把proj的friend给干了可能）  
/// </summary>
public class DarklingCrux : ModItem
{
    public override void SetStaticDefaults()
    {Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.width = 26;
        Item.height = 40;            
        Item.damage = 41;
        Item.mana = 5;
        Item.crit = 7;
        Item.noUseGraphic = true;
        Item.useTime = UseSpeedLevel.ExtremeSpeed + 4;
        Item.useAnimation = UseSpeedLevel.ExtremeSpeed + 4;
        Item.DamageType = DamageClass.Magic;
        Item.UseSound = SoundID.Item43;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.noMelee = true;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<DarklingCrossing>();
        Item.channel = true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, player.Center, Vector2.Zero, Item.shoot, damage, knockback, player.whoAmI, 0, 0);
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<ShadowlightSparkle>(), 8)
        .AddIngredient(ItemID.HellstoneBar, 9)
        .AddTile(TileID.Anvils)
        .Register();
    
}