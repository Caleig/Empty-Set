using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 精金回旋镖
/// </summary>
public class AdamantiteBoomerang : ModItem
{
    private int critCounter = 0;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 2, 0, 0);

        Item.width = 26; //已精确测量
        Item.height = 42;

        Item.damage = 51;
        Item.crit = 4;
        Item.maxStack = 5;

        Item.knockBack = KnockBackLevel.Normal;
        Item.useTime = UseSpeedLevel.FastSpeed - 3;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 3;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
            
        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<AdamantiteBoomerangProj>();
        Item.shootSpeed = 25f;
    }

    public override bool CanUseItem(Player player)
        => player.ownedProjectileCounts[Item.shoot] < Item.stack;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        critCounter++;
        if (critCounter % 5 == 0)
        {
            var p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            p.CritChance = 100;
            critCounter = 0;
            return false;
        }
        return true;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.AdamantiteBar, 10)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}