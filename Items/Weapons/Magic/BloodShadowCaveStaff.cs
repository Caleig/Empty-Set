using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Linq;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 血影空洞杖
/// </summary>
public class BloodShadowCaveStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Blood Shadow Cave Staff");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 46;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed - 1;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 1;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 39;
        Item.knockBack = KnockBackLevel.TooLower - 0.5f;
        Item.crit = 4-4;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item66;
        Item.mana = 21;

        Item.noMelee = true;
        Item.autoReuse = true;


        Item.shoot = ModContent.ProjectileType<BloodShadowCave>();
        Item.shootSpeed = 0;
    }
    public override bool AltFunctionUse(Player player)
    {
        var list = Main.projectile.Where(x => x.owner == Item.whoAmI)
            .Where(x => x.type == ModContent.ProjectileType<BloodShadowCave>() && x.timeLeft!=0).ToList();
        foreach (var p in list)
        {
            p.Kill();
        }

        return true;
    }
    public override bool CanUseItem(Player player) => player.ownedProjectileCounts[Item.shoot] < 3;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if(Main.mouseLeft)
            Projectile.NewProjectile(source, Main.MouseWorld, velocity, type, damage, knockback, Item.whoAmI);
        if (Main.mouseRight)
            player.statMana += 21;
        return false;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<BloodShadow>(7)
        .AddIngredient<FelShadowBar>(3)
        .AddIngredient(ItemID.CrimsonRod)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}