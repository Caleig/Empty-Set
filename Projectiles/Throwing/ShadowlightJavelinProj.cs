using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing
{
    public class ShadowlightJavelinProj : ModProjectile
    {
        public override string Texture => "EmptySet/Items/Weapons/Throwing/ShadowlightJavelin";
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.timeLeft = 600;
            Projectile.light = 0.6f;
            Projectile.extraUpdates = 1;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            if (Projectile.velocity.Y > 0)
            {
                Projectile.velocity.Y += 0.15f;
            }
            else Projectile.velocity.Y += 0.06f;
            Projectile.rotation = Projectile.velocity.ToRotation() + 0.785f;

            Projectile.alpha -= 50;
            if (Projectile.alpha < 60)
            {
                Projectile.alpha = 60;
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Scale: 1.5f).noGravity = true;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            SpawnDust(oldVelocity);
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SpawnDust(Projectile.oldVelocity);
            if(hit.Crit || Main.rand.NextBool(3))
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
                Projectile.NewProjectileDirect(null, Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ShadowlightExplosion>(), hit.Damage * 2, 1f, Projectile.owner);
                return;
            }
            target.AddBuff(BuffID.ShadowFlame, 60);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 pos = new(Projectile.position.X - Main.screenPosition.X + (float)(Projectile.width / 2), Projectile.position.Y - Main.screenPosition.Y + (float)(Projectile.height / 2));
            Rectangle rectangle = texture.Bounds;
            Vector2 drawOrigin = new(texture.Width / 2, texture.Height / 2);
            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                Color color = Color.Purple * Projectile.Opacity * 0.8f;
                color *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 drawPos = new(Projectile.oldPos[i].X - Main.screenPosition.X + (float)(Projectile.width / 2), Projectile.oldPos[i].Y - Main.screenPosition.Y + (float)(Projectile.height / 2));
                Main.EntitySpriteDraw(texture, drawPos, rectangle, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            Main.EntitySpriteDraw(texture, pos, rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }
        private void SpawnDust(Vector2 vel)
        {
            for (int i = 0; i < 30; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
                dust.velocity = vel.SafeNormalize(Vector2.Zero) * Main.rand.Next(10);
                dust.rotation += 1f;
            }
        }
    }
}
