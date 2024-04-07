using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Extensions;

namespace EmptySet.Projectiles.Melee.Issloos;

/// <summary>
/// 伊希鲁斯镰气
/// </summary>
public class IssloosProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("伊希鲁斯镰气");
        //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        //ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
    //ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
    //ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
}
    public override void SetDefaults()
    {
        Projectile.width = 90;
        Projectile.height = 74;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 8 * EmptySet.Frame;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = 5 + 1;
        //Projectile.extraUpdates = 1;
        //Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit) 
        {
            Main.player[Projectile.owner].HealEffect(3);
            Main.player[Projectile.owner].statLife += 3;
        }
    }
    public override void AI()
    {
        Projectile.DirectionalityRotation(0.3f);
        //Projectile.rotation +=  0.075f;
        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        for (int i = 0; i < 2; i++)
        {
            var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch);
            dust1.noGravity = true;
        }
    }

}