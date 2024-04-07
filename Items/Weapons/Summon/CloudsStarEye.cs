using Microsoft.Xna.Framework;
using EmptySet.Buffs;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Summon;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Summon;

/// <summary>
/// 阴云星眼
/// </summary>
public class CloudsStarEye : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Cloud Star Eye");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // 这让玩家能够在使用控制器时瞄准整个屏幕上的任何位置
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 78;
        Item.height = 78;
        Item.damage = 26;
        Item.mana = 11;
        Item.knockBack = KnockBackLevel.TooLower;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed; // 武器使用动画的时间跨度，建议与useTime设置相同。
        Item.crit = 7;
        Item.value = Item.sellPrice(0,0,40,0);
        Item.rare = ItemRarityID.Blue;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item44;


        // 下面的这些是召唤武器需要的
        Item.noMelee = true;
        Item.DamageType = DamageClass.Summon;
        Item.buffType = ModContent.BuffType<CloudStarEyeBuff>();
        // 没有buffTime否则项目工具提示会显示"1分钟持续时间"
        Item.shoot = ModContent.ProjectileType<CloudStarEyeProj>(); // 这个物品可以制造召唤物弹幕

    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        // 在这里你可以改变仆从的生成位置。大多数原版仆从生成在光标位置
        position = Main.MouseWorld;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        // 这是必需的，所以增益保持你的仆从活着，并允许你适当地解除它
        player.AddBuff(Item.buffType, 2);

        // 召唤物必须手动生成，然后有原始伤害分配到召唤物品的伤害
        var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
        projectile.originalDamage = Item.damage;

        //因为我们已经手动生成了投射物，所以我们不再需要游戏为我们自己生成它，所以返回false
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.RainCloud, 3)
        .AddIngredient(ItemID.Cloud, 5)
        .AddIngredient(ModContent.ItemType<RaySoul>(), 7)
        .AddTile(TileID.Anvils)
        .Register();
}
