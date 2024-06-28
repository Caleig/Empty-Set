using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace EmptySet.Projectiles.Magic
{
    public class BookofRocks2 : BTLcProj
    {
        int i;
        float d;
        float r;
        float r2;
        private Texture2D tex;
        private Vector2[] oldPosi = new Vector2[8];
        private Vector2[] oldVec = new Vector2[8];
        private int frametime = 0;
        public override void SetDefaults()
        {
            Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.damage = 5;

            tex = ModContent.Request<Texture2D>("EmptySet/Projectiles/Magic/BookofRocks").Value;
            base.SetDefaults();
        }
        public override void AI()
        {
            for(i = 0 ; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position , 32, 32 ,DustID.Stone, 0, 0, 0, Color.LightYellow , 1.2f);
            }
            Player player = Main.player[Projectile.owner];
            if(i > 0)
            {
                Projectile.rotation += 0.2f;
            }
            else
            {
                Projectile.rotation -= 0.2f;
            }
            if (Main.time % 2 == 0)    //每两帧记录一次（打一次点）
            {
                for (int i = oldVec.Length - 1; i > 0; i--) //你应该知道为什么这里要写int i = oldVec.Length - 1
                {
                    oldPosi[i] = oldPosi[i - 1];
                }
                oldPosi[0] = Projectile.Center;
            }
    Projectile.rotation = Projectile.velocity.ToRotation() + (float)(0.5 * MathHelper.Pi);  //这是你的弹幕贴图方向朝上的情况下的，如果你的弹幕贴图的方向朝向其他位置，你应该知道你要做什么
    frametime++;
            base.AI();
            base.AI();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            r2 = Main.rand.NextFloat(-1, 1);
            for(int i = 0; i < 50; i++)
            {
                
                r = Main.rand.NextFloat(-MathHelper.TwoPi, MathHelper.TwoPi);
                d = Main.rand.Next(7, 14);
                Vector2 vector = new Vector2(d * (float)Math.Cos(Projectile.velocity.ToRotation() - r), (float)Math.Sin(Projectile.velocity.ToRotation() + r));
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.Stone, vector, 0, Color.LightYellow, 2f);
                dust.noGravity = true;
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnSpawn(IEntitySource source)
        {
            i = Main.rand.Next(-2, 2);
            base.OnSpawn(source);
        }
        public override void PostDraw(Color lightColor)
        {
            for (int i = oldPosi.Length - 1; i > 0; i--)
            {
                if (oldPosi[i] != Vector2.Zero)
                {
                    Main.spriteBatch.Draw(tex, oldPosi[i] - Main.screenPosition, null, Color.White * 1 * (1 - .1f * i), (oldPosi[i - 1] - oldPosi[i]).ToRotation() + (float)(0.5 * MathHelper.Pi), tex.Size() * .5f, 1 * (1 - .05f * i), SpriteEffects.None, 0);  //如果贴图不在你所期望的方向上，你应该知道你要做什么
                    //试试修改这里的.05f与.02f，想想它们意味着什么
                }
            }
            base.PostDraw(lightColor);
        }
    }
}