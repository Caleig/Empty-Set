using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Held
{
    internal class ChargedCrystalLongSpearProj:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("能晶长枪");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Trident);
			AIType = ProjectileID.Trident;
		}
        public override void AI()
        {
			//Projectile.position
            Dust.NewDustDirect(Projectile.position,2,2,DustID.Electric).noGravity = true;
        }
    }
}
