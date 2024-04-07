using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EmptySet.Projectiles.Ranged
{
    public class Explosion2 : ModProjectile
    {
        public float num1;
        public float num2;
        public float num3;
        public int min;
        public int max;
        public void set(int _min, int _max)
        {
            min = _min; max = _max;
        }
        public override void SetDefaults()
        {
            Projectile.width = 256;
            Projectile.height = 256;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.light = 1f;
            Projectile.timeLeft = 5;
        }
        public override void AI()
        {

            Projectile.ai[0] += 8f;
            for (int i = 0; i < 20; i++)
            {
                float num1 = Main.rand.Next(-63, 64);
                float num2 = Main.rand.Next(-63, 64);
                float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
                num3 = Main.rand.Next(9, 18) / num3;
                num1 *= num3;
                num2 *= num3;
            }
        }
    }
}
