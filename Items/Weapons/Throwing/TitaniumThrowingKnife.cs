using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Throwing;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 钛金飞刀
/// </summary>
public class TitaniumThrowingKnife : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Titanium Throwing Knife");
        
        //Tooltip.SetDefault("Wood Throwing Knife");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "木投刀");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 300;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 0, 70, 0);

        Item.width = 28;
        Item.height = 32;

        Item.damage = 43;
        Item.crit = 4;
        Item.maxStack = 999;

        Item.knockBack = KnockBackLevel.TooLower;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed -3;
        Item.useAnimation = UseSpeedLevel.FastSpeed -3;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.consumable = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<TitaniumThrowingKnifeProj>();
        Item.shootSpeed = 11f;
    }

    private int counter = 0;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        counter++;
        if (counter % 3 == 0)
        {
            Projectile.NewProjectile(source, position, Vector2.Normalize(velocity) * 20,
                ModContent.ProjectileType<TitaniumThrowingKnifeProj2>(), damage, knockback, player.whoAmI);
            counter = 0;
        }
        return true;
    }
    public override void AddRecipes() => CreateRecipe(33)
        .AddIngredient(ItemID.TitaniumBar, 3)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}