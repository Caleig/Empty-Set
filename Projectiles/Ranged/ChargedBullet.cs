using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.Extensions;
using Terraria.Audio;

namespace EmptySet.Projectiles.Ranged;
/// <summary>
/// 充能弹
/// </summary>
public class ChargedBullet : ModProjectile
{
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 2;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        //Projectile.penetrate = 1;
        Projectile.timeLeft = 600;
        //Projectile.alpha = 0;
        Projectile.light = 0.5f;
        //Projectile.ignoreWater = false;
        //Projectile.tileCollide = true;

        //Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(10))
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Projectile.LetExplodeWith(9, () =>
            {
                for (int i = 0; i < 9; i++)
                    Dust.NewDust(Projectile.position, 10, 10, DustID.BlueTorch);//Projectile.width, Projectile.height
            },default,true,false);
            Projectile.timeLeft = 3;
        }
    }
}
