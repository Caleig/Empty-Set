using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    class ShockWaveProjectile:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("冲击波");
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 316;
			Projectile.height = 26;
			Projectile.alpha = 100;
			Projectile.penetrate = -1;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.light = 1;
			Projectile.timeLeft = 30;
		}

		public override void AI()
		{
			if (++Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
					Projectile.frame = 0;
			}
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			target.AddBuff(BuffID.BrokenArmor, 300);
			base.OnHitPlayer(target, info);
        }
    }
}
