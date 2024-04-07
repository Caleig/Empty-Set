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
/// 逐光邪奏曲
/// </summary>
public class HolyTune : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Holy Tune");
 
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 58;
        Item.height = 48;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed-1;
        Item.useAnimation = UseSpeedLevel.FastSpeed-1; // 武器使用动画的时间跨度，建议与useTime设置相同。
        //Item.autoReuse = true; // 自动挥舞
        Item.DamageType = DamageClass.Magic; // 伤害类型
        
        Item.damage = 50;
        Item.mana = 9;
        Item.knockBack = KnockBackLevel.TooLower;
        Item.crit = 4 - 4;
        Item.value = Item.sellPrice(0, 11, 11, 11);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item8;

        Item.channel = true;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.shoot = ModContent.ProjectileType<HolyTuneProj>();//PursuitOfLightTuneProj
        Item.shootSpeed = 13f;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        //Projectile.NewProjectile(source, new Vector2(player.Center.X, player.Center.Y - 300f), Vector2.Zero, type,
        //    damage, knockback, player.whoAmI);
        var pList = Main.projectile.Where(proj => proj.owner == player.whoAmI)
            .Where(proj => proj.type == type)
            .Where(proj=>proj.timeLeft!=0);
            //.ToList();
        if (!pList.Any())
            return true;
        return false;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SoulofLight, 3)
        .AddIngredient(ItemID.SoulofNight, 3)
        .AddIngredient(ItemID.HallowedBar, 3)
        .AddIngredient(ModContent.ItemType<FelShadowBar>(),3)
        .AddIngredient(ModContent.ItemType<EternityAshMagicBall>())
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
