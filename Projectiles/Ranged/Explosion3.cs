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
    public class Explosion3 : ModProjectile
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
            Projectile.width = 352;
            Projectile.height = 352;
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
    }
}
