using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 碎片投刀
/// </summary>
public class Scrapthrowingknives : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Quench Gel Jar");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 300;
    }


    // 攻击方式:扔出一个受重力影响的淬火凝胶罐，击中敌人或物块后会向周围发射3个燃烧碎片（原版莫洛托夫鸡尾酒的那个）
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.value = Item.sellPrice(0, 0, 0, 0);

        Item.width = 20;
        Item.height = 20;

        Item.damage = 10;
        Item.crit = 4 - 4;
        Item.maxStack = 999;

        Item.knockBack = 2;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed - 2;
        Item.useAnimation = UseSpeedLevel.NormalSpeed - 2;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;
        Item.consumable = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<ScrapthrowingknivesProj>();
        Item.shootSpeed = 7f;
    }

    public override void AddRecipes() => CreateRecipe(30)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.IronOrLead), 1)
        .AddTile(TileID.Anvils)
        .Register();
}


/// <summary>
/// 碎片投刀弹幕
/// </summary>
public class ScrapthrowingknivesProj : ModProjectile
{
    public override string Texture => "EmptySet/Items/Weapons/Throwing/Scrapthrowingknives";
    private int fr = 0;
    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
        Projectile.aiStyle = 2;
    }

    public override void AI()
    {
        Projectile.rotation += 0.3f * Projectile.direction;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);


    }
        public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        hitbox.X += 4;
        hitbox.Y += 4;
        hitbox.Width -= 7;
        hitbox.Height -= 7;
    }
}
