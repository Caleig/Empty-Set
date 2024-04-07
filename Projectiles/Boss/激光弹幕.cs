using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss 
{
    public class 激光弹幕 : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.light = 1;
            Projectile.timeLeft = 30 * EmptySet.Frame;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        }

    }
}
