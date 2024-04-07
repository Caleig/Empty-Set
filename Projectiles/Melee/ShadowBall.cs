using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 影球弹幕
/// </summary>
public class ShadowBall : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 48; //已精确测量
        Projectile.height = 22;
        Projectile.friendly = true;
        //Projectile.light = 0.35f;
        //Projectile.penetrate = 0 + 1;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
            DustID.PurpleTorch, 0, 0, 0, Color.Purple);
        //if(!Projectile.tileCollide && Projectile.ai[0] <Projectile.position.Y)
        //    Projectile.tileCollide=true;
    }
}
