using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace EmptySet.Projectiles.Throwing
{
    public class ScarletSpike : ModProjectile
    {
        float d;
        float r = 0;
        float r2;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.light = 0.1f;
            base.SetDefaults();
        }
        public override void AI()
        {

            Lighting.AddLight(Projectile.Center, new Vector3(Color.Red.R / 1000f, Color.Red.G / 1000f, Color.Red.B / 1000f));
            base.AI();
        }
        public override void PostDraw(Color lightColor)
        {
            if (Main.time % 3 == 0)
            {
                Dust.NewDustDirect(Projectile.Center, 20, 20, DustID.DynastyShingle_Red, 0, 0, 0, default, 1);
            }
            base.PostDraw(lightColor);
        }
    }
}