using Microsoft.Xna.Framework;
using EmptySet.Items.Weapons.Melee;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Projectiles.Held
{
    internal class ChargedCrystalLongSpearTurnProj:ModProjectile
    {
        public override string Texture => "EmptySet/Projectiles/Held/ChargedCrystalLongSpearProj";

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("能晶长枪");
		}

		public override void SetDefaults()
		{
            Projectile.width = 124;
            Projectile.height = 122;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
            Projectile.localNPCHitCooldown = 1;
            Projectile.tileCollide = false;
            Projectile.scale = 1.2f;
            //Projectile.Center = Projectile.position;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.dead)
                Projectile.Kill();

            
            Main.player[Projectile.owner].heldProj = base.Projectile.whoAmI;
            Projectile.Center = player.Center;
            Projectile.rotation += MathHelper.ToRadians(12) * player.direction;
            Projectile.spriteDirection = player.direction;
            Main.dust[
                Dust.NewDust((Projectile.position - player.Center).RotatedBy(Projectile.rotation) + player.Center, 2, 2,
                    DustID.Electric)].noGravity = true;
            if (player.HeldItem.type == ModContent.ItemType<ChargedCrystalLongSpear>() && Main.mouseRight) 
            {
                Projectile.timeLeft = 20;
            }
        }

    }
}
