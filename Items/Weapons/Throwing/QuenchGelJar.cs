using Microsoft.Xna.Framework;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 淬火凝胶罐
/// </summary>
public class QuenchGelJar : ModItem
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
        Item.value = Item.sellPrice(0, 0, 0, 10);

        Item.width = 24;
        Item.height = 24;

        Item.damage = 7;
        Item.crit = 4 - 4;
        Item.maxStack = 999;

        Item.knockBack = KnockBackLevel.None;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed - 2;
        Item.useAnimation = UseSpeedLevel.NormalSpeed - 2;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = true;
        Item.consumable = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<QuenchGelJarProj>();
        Item.shootSpeed = 5f;
    }

    public override void AddRecipes() => CreateRecipe(20)
        .AddIngredient(ItemID.Bottle, 20)
        .AddIngredient(ItemID.Gel, 5)
        .AddIngredient(ItemID.Rope, 10)
        .AddTile(TileID.WorkBenches)
        .AddTile(TileID.Campfire)
        .Register();
}


/// <summary>
/// 淬火凝胶罐弹幕
/// </summary>
public class QuenchGelJarProj : ModProjectile
{
    public override string Texture => "EmptySet/Items/Weapons/Throwing/QuenchGelJar";
    private int fr = 0;
    public override void SetDefaults()
    {
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
        Projectile.aiStyle = 1;
    }

    public override void AI()
    {
        if (fr == 10)
        {
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDustDirect(Projectile.Center, 1, 1, DustID.RedTorch).noGravity = true;
            }
            fr = 0;
        }
        fr++;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, (Projectile.velocity).RotatedByRandom(180f),
            ProjectileID.MolotovFire, Projectile.damage, Projectile.knockBack, Projectile.owner);
        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, (Projectile.velocity).RotatedByRandom(180f),
            ProjectileID.MolotovFire2, Projectile.damage, Projectile.knockBack, Projectile.owner);
        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, (Projectile.velocity).RotatedByRandom(180f),
            ProjectileID.MolotovFire3, Projectile.damage, Projectile.knockBack, Projectile.owner);

    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        hitbox.X += 2;
        hitbox.Y += 2;
        hitbox.Width -= 4;
        hitbox.Height -= 4;
    }
}