using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.FrozenCore
{
    internal class IcicleProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("冰锥");
        }
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 38;
            Projectile.ignoreWater = false;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;
        }

        public override void AI()
        {
            Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.FrostStaff, 0f, 0f, 200)].noGravity = true;
            if (Projectile.velocity.Y <= 17) Projectile.velocity.Y += 0.3f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Frostburn, 300);
            base.OnHitPlayer(target, info);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}
