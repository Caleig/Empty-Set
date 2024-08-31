using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace EmptySet.Projectiles.Magic
{
    public class DemonHand : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("魔之手");
        Main.projFrames[Projectile.type] = 16 ;//设置帧数
        base.SetStaticDefaults();
    }
    public override void SetDefaults()
    {
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.height = 200;
        Projectile.width = 200;
        Projectile.light = 1f;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 2;
        Projectile.ignoreWater = true;
        Projectile.penetrate = -1;
            base.SetDefaults();
    }
        
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;//Vector2.Normalize(Main.MouseWorld - Projectile.Center)
            if (player.direction > 0)
            {
                Projectile.Center = player.Center + Vector2.Normalize(Projectile.velocity) * 50;
                Projectile.rotation = Projectile.velocity.ToRotation();
            }
            else
            {
                Projectile.Center = player.Center + Vector2.Normalize(Projectile.velocity) * 50;
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi;
            }
            Projectile.spriteDirection = player.direction;
            
            // 这是一个简单的“从上到下循环所有帧”动画
            int frameSpeed = 5;
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;//画面帧

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.Kill();
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.ShadowFlame, 3 * 60);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
                Projectile.Center + Projectile.velocity * 8, Projectile.width / 2, ref Projectile.localAI[1]);
        }
    }
}