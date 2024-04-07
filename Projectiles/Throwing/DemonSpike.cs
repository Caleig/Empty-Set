using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace EmptySet.Projectiles.Throwing
{
    public class DemonSpike : ModProjectile
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
            Projectile.timeLeft = 600;
            Projectile.alpha = 255;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.light = 0.2f;
            base.SetDefaults();
        }
        public override void AI()
        {

            Lighting.AddLight(Projectile.Center, new Vector3(Color.Red.R / 100f, Color.Red.G / 100f, Color.Red.B / 100f));
            base.AI();
        }
        public override void PostDraw(Color lightColor)
        {
            if (Main.time % 3 == 0)
            {
                Dust.NewDustDirect(Projectile.Center, 20, 20, DustID.Demonite, 0, 0, 0, default, 1);
            }
            base.PostDraw(lightColor);
        }
    }
}