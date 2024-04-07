using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 仙人掌长枪
/// </summary>
public class CactusPike : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Cactus Pike");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "仙人掌长枪");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
       
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Trident);//Spare
            
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 4, 0);
            
        Item.width = 100; //已精确测量
        Item.height = 100;
            
        Item.damage = 8;
        Item.crit = 0;
            
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = UseSpeedLevel.VerySlowSpeed;
        Item.useTime = UseSpeedLevel.VerySlowSpeed;
        Item.knockBack = KnockBackLevel.TooLower - 1f;

        Item.DamageType = DamageClass.Melee;
        Item.noMelee = true;
        Item.autoReuse = false;
        Item.noUseGraphic = true;
        //Item.channel = true;

        Item.shoot = ModContent.ProjectileType<CactusPikeProj>();
        Item.shootSpeed = 3.7f;
    }
    public override bool CanUseItem(Player player)
    {
        // Ensures no more than one spear can be thrown out, use this when using autoReuse
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Cactus, 15)
        .AddTile(TileID.WorkBenches)
        .Register();
}