using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 明
/// </summary>
public class Bright : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Bright");
   
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 64;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.ExtremeSpeed + 1;
        Item.useAnimation = UseSpeedLevel.ExtremeSpeed + 1;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 30;
        Item.knockBack = KnockBackLevel.None + 1f;
        Item.crit = 4 - 4;
        Item.value = Item.sellPrice(0, 9, 0, 0);
        Item.rare = ItemRarityID.LightPurple;
        Item.UseSound = SoundID.Item11;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 15f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
        
        return false;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => !Main.rand.NextBool();
    //public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Megashark)
        .AddIngredient(ItemID.BeetleHusk, 20) //甲虫外壳
        .AddIngredient(ModContent.ItemType<EternityAshBar>(), 3)
        .AddTile(TileID.MythrilAnvil)
        .Register();

}
