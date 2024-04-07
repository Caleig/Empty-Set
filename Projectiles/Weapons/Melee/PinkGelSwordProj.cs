using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 粉凝胶剑弹幕
/// </summary>
public class PinkGelSwordProj : ModProjectile
{
    private int _collideCount = 0;

    public override void SetDefaults()
    {
        Projectile.width = 16; //已精确测量
        Projectile.height = 14;

        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.penetrate = 4;
        Projectile.timeLeft = 15 * EmptySet.Frame;

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
}