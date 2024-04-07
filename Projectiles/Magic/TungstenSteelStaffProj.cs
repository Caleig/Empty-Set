using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

/// <summary>
/// 钨钢法杖弹幕
/// </summary>
public class TungstenSteelStaffProj : ModProjectile
{
    //private int _collideCount = 0;
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 22;
        Projectile.penetrate = 3 + 1;
        Projectile.friendly = true;
        Projectile.timeLeft = 11 * EmptySet.Frame;
        //Projectile.alpha = 72;
        //Projectile.aiStyle = 1;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemDiamond,0,0,0,new (93, 136, 136));//,0, 0, 0, Color.Pink
        dust.noGravity = true;
        //Projectile.rotation += Projectile.velocity.ToRotation() * 0.07f;
    }

}