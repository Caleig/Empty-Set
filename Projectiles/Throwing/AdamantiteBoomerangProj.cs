using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing;

/// <summary>
/// 精金回旋镖弹幕
/// </summary>
public class AdamantiteBoomerangProj : ModProjectile
{
    private int lastTime = 0;
    public override void SetDefaults()
    {
        Projectile.width = 26;
        Projectile.height = 42;
        Projectile.aiStyle = 3;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Throwing;

        AIType = 301;
    }

    //private void CritInit()
    //{
    //    if (Projectile.ai[0] != 0)
    //        Crit = true;
    //    Projectile.ai[0] = 0;
    //}
    //private bool Crit=false;
    public override void AI()
    {
        //反弹回力镖 新特性代码？！
        //lastTime = lastTime < (int)Projectile.ai[1] ? (int)Projectile.ai[1] : lastTime;
        //if (!canKill&& lastTime<29)
        //{
        //    Projectile.ai[0] = 0;
        //}

        
        //Main.NewText(Projectile.ai[1]);
        //if (Projectile.penetrate <= 1)
        //{
        //    canKill=true;
        //    //Projectile.ai[0] = 1;
        //    Projectile.ai[1] = 0;
        //    Projectile.penetrate = -1;
        //}

        if (Projectile.CritChance >= 100)
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                DustID.RedTorch);
            dust.noGravity = true;
        }
        else
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                DustID.PinkTorch);
            dust.noGravity = true;
        }
    }
    //public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
    //{
    //    Main.NewText($"crit? {Crit}");

    //    if (Crit)
    //    {
    //        crit = true;
    //    }
    //}
}