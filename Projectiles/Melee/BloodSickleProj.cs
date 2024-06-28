using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Extensions;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 血镰弹幕 <br/>
/// ai[0] ==0 -> 血镰弹幕
/// ai[0] !=0 -> 奥德赛啦弹幕
/// </summary>
public class BloodSickleProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 28; //已精确测量
        Projectile.height = 52;
        Projectile.friendly = true;
        Projectile.penetrate = 5 + 1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = (12 * EmptySet.Frame);
    }
    public override void AI()
    {
        if (Projectile.timeLeft > 300)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        }
        Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Torch).noGravity = true;
        if (Projectile.ai[0] == 0)
        {
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                DustID.RedTorch).noGravity = true;
            if (Projectile.timeLeft > 5 * EmptySet.Frame)
            {

            }
            else
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.alpha += 1;
            }
        }
        else if (Projectile.ai[0] != 0)
        {
            if (Projectile.timeLeft == 12 * EmptySet.Frame)
                Projectile.timeLeft = 61+300;
            if (Projectile.timeLeft == 300)
                Projectile.velocity = Vector2.Zero;
        }

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Projectile.ai[0] == 0)
            target.AddBuff(BuffID.Ichor, 3 * EmptySet.Frame);
        //if (Projectile.ai[0] != 0)
        //    Projectile.velocity = Vector2.Zero;
    }
}
