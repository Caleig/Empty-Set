using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Held
{
    internal class ShakeEartherDrillProj:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("撼地钻头");
		}

		public override void SetDefaults()
		{
			Projectile.width = 26;
			Projectile.height = 56;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
		}

		public override bool PreAI()
		{
			/*int dust = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 187, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
			Main.dust[dust].scale = 1f;
			Main.dust[dust].noGravity = true;
			Main.dust[dust].noLight = true;*/

			return true;
		}
	}
}
