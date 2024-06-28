using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;

namespace EmptySet.Projectiles.Magic
{
    public class BookofRocks : BTLcProj
    {
        int i;
        float Accelerate;
        private Texture2D tex;
        private Vector2[] oldPosi = new Vector2[8];
        private Vector2[] oldVec = new Vector2[8]; //示例中记录16个坐标用于绘制，你可以试着修改这个值，并思考这意味着什么。
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
			Projectile.tileCollide = false;
			Projectile.damage = 5;

            tex = ModContent.Request<Texture2D>("EmptySet/Projectiles/Magic/BookofRocks").Value;
            base.SetDefaults();
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            for(i = 0 ; i < 2; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position , 32, 32 ,DustID.Stone, 0, 0, 0, Color.LightYellow , 1f);
            }
            if(Accelerate <= 16f)
            {
                Accelerate += 0.3f;
            }
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * Accelerate; 
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
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Vector2 velocity = new Vector2(0, 25);
            Vector2 startPos = target.position + new Vector2(0, -700);
            Vector2 velo = (target.position - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), startPos, velo, ModContent.ProjectileType<Projectiles.Magic.BookofRocks2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Main.MouseWorld.Y);
            for (int i = 0; i < 2; i++)
            {
                startPos = target.position + new Vector2((i == 1 ? -1 : 1) * Main.rand.Next(50, 1001), -1200);
                velo = (target.position - startPos).SafeNormalize(Vector2.UnitY) * velocity.Length();
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), startPos, velo, ModContent.ProjectileType<Projectiles.Magic.BookofRocks2>(), Projectile.damage, Projectile.knockBack, Projectile.owner, Main.MouseWorld.Y);
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void OnSpawn(IEntitySource source)
        {
            Accelerate = 0.5f;
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