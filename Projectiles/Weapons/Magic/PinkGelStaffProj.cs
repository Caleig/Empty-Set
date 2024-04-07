using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Magic;

/// <summary>
/// 粉凝胶法杖弹幕
/// </summary>
public class PinkGelStaffProj : ModProjectile
{
    private int _collideCount = 0;
    public override void SetDefaults()
    {
        Projectile.width = 16; //已精确测量
        Projectile.height = 14;
        Projectile.penetrate = 3 + 1;
        Projectile.friendly = true;
        Projectile.timeLeft = 11 * EmptySet.Frame;
        Projectile.alpha = 72;
        //Projectile.aiStyle = 1;
        Projectile.DamageType = DamageClass.Magic;
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch,
            0, 0, 0, Color.Pink);
        dust.noGravity = true;
        Projectile.rotation += Projectile.velocity.ToRotation() * 0.07f;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (_collideCount < 2)
        {
            _collideCount++;
            Projectile.velocity.Rebound(oldVelocity);
            return false;
        }
        return true;
    }
}