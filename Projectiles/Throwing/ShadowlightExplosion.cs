using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing
{
    public class ShadowlightExplosion : ModProjectile
    {
        public override string Texture => "EmptySet/Projectiles/InvisibleProjectile";
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
			Projectile.light = 1f;
			Projectile.timeLeft = 30;
        }
		public override void AI()
		{
			
			Projectile.ai[0] += 8f;
			for (int i = 0; i < 20; i++)
            {
				float num1 = Main.rand.Next(-27, 28);
				float num2 = Main.rand.Next(-27, 28);
				float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
				num3 = Main.rand.Next(9, 18) / num3;
				num1 *= num3;
				num2 *= num3;
				Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Scale: Main.rand.NextFloat(1.5f, 3f));				
				dust.noGravity = true;
				dust.position = Projectile.Center;
				dust.position += new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11));
				dust.velocity.X = num1;
				dust.velocity.Y = num2;
			}
		}
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.ShadowFlame, 180);
		}
    }
}
