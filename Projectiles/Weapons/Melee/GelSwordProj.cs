using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 凝胶剑弹幕
/// </summary>
public class GelSwordProj : ModProjectile
{
    private int _collideCount = 0;

    public override void SetDefaults()
    {
        Projectile.width = 16; //已精确测量
        Projectile.height = 14;

        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.penetrate = 2 + 1;
        Projectile.timeLeft = 9 * EmptySet.Frame;

        Projectile.DamageType = DamageClass.Melee;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (_collideCount < 3)
        {
            _collideCount++;
            Projectile.velocity.Rebound(oldVelocity);
            return false;
        }
        return true;
    }
    //public override void AI()
    //{
    //Projectile.velocity.Y += 0.1f;
    //}
}