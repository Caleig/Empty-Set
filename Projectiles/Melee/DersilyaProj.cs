using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Extensions;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 德塞拉亚斩击弹幕
/// </summary>
public class DersilyaProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Melee;
        Projectile.width = 214; //已精确测量
        Projectile.height = 66;
        Projectile.friendly = true;
        Projectile.scale = 1.4f;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.timeLeft = (int)(2 * EmptySet.Frame);
    }
    public override void AI()
    {
        //Projectile.DirectionalityRotation(0.27f);
        //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
        //    DustID.PurpleTorch, 0, 0, 0, Color.Purple);
        if(Projectile.timeLeft>95) Projectile.alpha -= 10;
        if (Projectile.timeLeft < 25) Projectile.alpha += 10;
        //Projectile.spriteDirection = (int) Projectile.ai[0];
        Projectile.rotation = Projectile.ai[0];
        Projectile.velocity *= 0.95f;

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {

        
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        modifiers.FinalDamage.Flat += target.defense;
    }
}