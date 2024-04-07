using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 仲裁巨剑弹幕
/// </summary>
public class ArbitralSwordProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 44;
        Projectile.height = 141;
        Projectile.friendly = true;
        Projectile.alpha = 100;
        Projectile.light = 0.25f;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(8 * EmptySet.Frame);
    }

    private bool hitNpc = false;
    private int killFrame = (int)2 * EmptySet.Frame;
    public override void AI()
    {
        Projectile.penetrate = -1;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
            DustID.PinkTorch);//, 0, 0, 0, Color.Orange
       //if (!Projectile.tileCollide && Projectile.ai[0] < Projectile.position.Y)
       //    Projectile.tileCollide = true;
       if (hitNpc)
       {
               Projectile.alpha += (255-Projectile.alpha)/ killFrame;
       }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!hitNpc)
        {
            Projectile.timeLeft = 180;
            hitNpc = true;
        }
    }
}
