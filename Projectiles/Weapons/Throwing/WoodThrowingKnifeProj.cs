using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 木投刀弹幕
/// </summary>
public class WoodThrowingKnifeProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 10; //已精确测量
        Projectile.height = 24;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
    }
    public override void AI() => Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
}