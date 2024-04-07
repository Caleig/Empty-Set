using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Firework
{
    class YanHuaProj : ModProjectile
    {
        private float accelerationY = 0.04f;
        private int timer = 0;
        public Dust dust;
        public float radius = 16 * 10;
        private Vector2 pos;
        private Vector2 vel;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烟花");
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = 600;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = false;
            Projectile.light = 1;

        }

        public override void AI()
        {
            timer++;
            int p;
            if (timer == 1) 
            {
                Projectile.velocity = new(0,-15);
            }
            if (Projectile.velocity.Y >= 0)
            {
                Projectile.velocity = new(0, 0);
                Projectile.alpha = 255;
                SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
                IEntitySource source = Projectile.GetSource_FromAI();
                for (int i = 0;i<250;i++) 
                {
                    pos = Main.rand.NextVector2Circular(radius, radius) + Projectile.position;
                    vel = Vector2.Normalize(pos - Projectile.position) * Vector2.Distance(pos,Projectile.position)*0.007f;
                    p = Projectile.NewProjectile(source, pos, vel, ModContent.ProjectileType<ZiProj>(), 0, 0, Projectile.owner);
                    ((ZiProj)Main.projectile[p].ModProjectile).random = Main.rand.Next(600, 1000);
                }
                Projectile.Kill();
            }
            else 
            {
                Projectile.velocity.Y += accelerationY;
                //270
                dust = Dust.NewDustDirect(Projectile.position, 4, 4, DustID.Flare);
                //dust.velocity = new(0, 0);
                //dust.fadeIn = 0.1f;
                //dust.alpha = 100;
                
            }
        }
    }
    class ZiProj : ModProjectile
    {
        public Dust dust;
        private float accelerationY = 0.003f;
        public int random = 0;
        private int timer = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烟花");
        }
        public override void SetDefaults()
        {
            Projectile.timeLeft = 99999;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.ignoreWater = false;
            Projectile.light = 1;
            Projectile.alpha = 0;

            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 50; // 要记录的旧位置长度
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
        }
        public override void AI()
        {
            timer++;
            Projectile.velocity = Projectile.velocity - Projectile.velocity * 0.001f;
            Projectile.velocity.Y += accelerationY;
            if (timer >= random) 
            {
                Projectile.alpha++;
                if (Projectile.alpha >= 255) 
                {
                    Projectile.Kill();
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            // 用不受光线影响的颜色重新绘制投射体
            Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
            Vector2 drawPos;
            Color color;
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                color.G = (color.G- color.G * k / Projectile.oldPos.Length)<=0?(byte)0:(byte)(color.G - color.G * k / Projectile.oldPos.Length);
                color.B = (color.B - color.B * k / Projectile.oldPos.Length) <= 0 ? (byte)0 : (byte)(color.B - color.B * k / Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, (1- k / Projectile.oldPos.Length), SpriteEffects.None, 0);
            }
            return true;
        }

    }
}
