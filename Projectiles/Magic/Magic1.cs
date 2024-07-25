using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;
using FighterSickle;
using Terraria.GameContent;
using System;

namespace EmptySet.Projectiles.Magic
{
    public class Magic1 : ModProjectile
    {
        public float tmr = 0;
        public override void SetDefaults() 
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.damage = 5;

			AIType = ProjectileID.Bullet;
		}
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, new Vector3(Color.Blue.R / 100f, Color.Blue.G / 100f, Color.Blue.B / 100f));
            NPC target = null;
            Player player = Main.player[Projectile.owner];
            Dust.NewDustDirect(Projectile.Center, 2, 2, ModContent.DustType<Dusts.Magic1>(), 0, 0);

            float distanceMax = 600f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                        if (Projectile.timeLeft <= 270)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
            }
            if(target != null)
            {
                var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 10f;
                Projectile.velocity = (targetVel + Projectile.velocity * 6) / 7f;
            }
            else
            {
                tmr++;
                int frequency = 60;//转一圈需要的时间(帧)
                float radius = 80;//半径
                tmr = tmr % frequency;

                float Xdeviation = 0;//x偏移量
                float Ydeviation = 0;//y偏移量
                                     //x^2+y^2=r^2变式
                if (tmr < frequency / 2)
                {
                    float counter = tmr / 15 - 1;
                    Xdeviation = (float)(Math.Sqrt(1 - (counter * counter)) * radius);
                }
                else
                {
                    float counter = tmr / 15 - 3;
                    Xdeviation = -(float)(Math.Sqrt(1 - (counter * counter)) * radius);
                }
                if (tmr < frequency / 4 * 3 && tmr >= frequency / 4)
                {
                    Ydeviation = -(float)(Math.Sqrt(radius * radius - (Xdeviation * Xdeviation)));
                }
                else
                {
                    Ydeviation = (float)(Math.Sqrt(radius * radius - (Xdeviation * Xdeviation)));
                }
                Projectile.position.X = player.position.X + Xdeviation;
                Projectile.position.Y = player.position.Y + Ydeviation;
            }
        }
    }
}