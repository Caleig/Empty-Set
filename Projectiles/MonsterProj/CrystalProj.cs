using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using System;
using Terraria.Localization;

namespace EmptySet.Projectiles.MonsterProj
{
    public class CrystalProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
			Projectile.damage = 0;
			Projectile.light = 0.5f;
            Projectile.scale = 0.7f;

			AIType = ProjectileID.Bullet;
        }
        public override void Kill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Shatter, Projectile.position);
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			for(int i = 0; i < 10; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.BlueCrystalShard, 0, 0, 0, default, 1.2f);
			}
            Projectile.Kill();
            return base.OnTileCollide(oldVelocity);
        }

		public override void AI()
		{
			for(int i = 0; i < 2; i++)
			{
                Dust dust = Dust.NewDustDirect(Projectile.position, 4, 4, DustID.GemSapphire, 0, 0, 0, default, 1.3f);
                dust.noGravity = true;
            }
		}
    }
}