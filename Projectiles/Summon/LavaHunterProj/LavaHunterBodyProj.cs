using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Summon.LavaHunterProj
{
    internal class LavaHunterBodyProj:ModProjectile
    {
        Projectile segment;
        Projectile head;
        int timer = 0;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; 
            //ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;  这是必要的，这样你的仆从可以正确地生成时，当其他仆从被召唤时替换
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 42;
            Projectile.tileCollide = false;

            // 下面的这些是召唤武器需要的
            Projectile.friendly = true; // 只控制它是否在接触时对敌人造成伤害
            Projectile.minion = true; // 宣布这是一个召唤物(有很多效果)
            Projectile.DamageType = DamageClass.Summon; // 声明伤害类型(造成伤害所需的)
            Projectile.minionSlots = 0f; // 这个仆从从玩家可用的仆从槽总数中占有的槽数(稍后详细介绍)
            Projectile.penetrate = -1; // 这样仆从就不会在与敌人或贴图碰撞时掉落
            Projectile.timeLeft = 2;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            segment = null;
            head = null;
            if (Projectile.ai[0] > -1 && Projectile.ai[0] < Main.maxProjectiles && Main.projectile[(int)Projectile.ai[0]].active && (ModContent.ProjectileType<LavaHunterProj>() == Main.projectile[(int)Projectile.ai[0]].type || ModContent.ProjectileType<LavaHunterBodyProj>() == Main.projectile[(int)Projectile.ai[0]].type))
            {
                segment = Main.projectile[(int)Projectile.ai[0]];
            }
            if (Projectile.ai[1] > -1 && Projectile.ai[1] < Main.maxProjectiles && Main.projectile[(int)Projectile.ai[1]].active && ModContent.ProjectileType<LavaHunterProj>() == Main.projectile[(int)Projectile.ai[1]].type)
            {
                head = Main.projectile[(int)Projectile.ai[1]];
            }

            if (head != null) Projectile.timeLeft = 2;

            Projectile.velocity = Vector2.Zero;

            if (head is null)
                return;
            int pastPos = ProjectileID.Sets.TrailCacheLength[Projectile.type] - (int)head.ai[1] - 1;

            if (Projectile.localAI[0] == 0)
            {
                Projectile.localAI[0] = 1;
                for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
                    Projectile.oldPos[i] = Projectile.position;
            }


            float num191 = segment.Center.X  - Projectile.Center.X;
            float num192 = segment.Center.Y - Projectile.Center.Y;
            Projectile.rotation = (float)System.Math.Atan2((double)num192, (double)num191);
            float num193 = (float)System.Math.Sqrt((double)(num191 * num191 + num192 * num192));
            num193 = Projectile.type==ModContent.ProjectileType<LavaHunterTailProj>()? (num193 - Projectile.width + 20) / num193 : (num193 - Projectile.width+10) / num193;
            num191 *= num193;
            num192 *= num193;
            Projectile.velocity = Vector2.Zero;
            Projectile.position.X = Projectile.position.X + num191;
            Projectile.position.Y = Projectile.position.Y + num192;

            //Projectile.Center = segment.oldPos[pastPos] + segment.Size / 2;
            //Projectile.rotation = Projectile.DirectionTo(segment.Center).ToRotation();

            /*if (Projectile.Distance(Projectile.oldPos[pastPos - 1] + Projectile.Size / 2) > 45 * Projectile.scale)
            {
                Projectile.oldPos[pastPos - 1] = Projectile.position + Vector2.Normalize(Projectile.oldPos[pastPos - 1] - Projectile.position) * 45 * Projectile.scale;
            }*/
            if (Main.rand.Next(30) == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, 0f, 0f, 0, default(Color), 2f);
                Main.dust[dust].noGravity = true;
            }
        }
    }
}
