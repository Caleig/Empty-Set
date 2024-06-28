using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using EmptySet.UI;

namespace EmptySet.Projectiles.Buffs
{
    public class SingtheWood2 : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 255;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.damage = 0;
            Projectile.light = 0.2f;
            base.SetDefaults();
        }
        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if(player.Center.Y < Projectile.Center.Y + 40 && player.Center.Y > Projectile.Center.Y - 40 && player.Center.X < Projectile.Center.X + 40 && player.Center.X > Projectile.Center.X - 40)
            {
                player.Heal(5);
               player.statLife += 5;
                Projectile.Kill();
            }
        }
        public override void PostDraw(Color lightColor)
        {
                Dust.NewDustDirect(Projectile.Center, 20, 20, DustID.Grass, 0, 0, 0, Color.GreenYellow, 1);
            base.PostDraw(lightColor);
        }
    }
}