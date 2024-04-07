using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 永夜投矛弹幕
/// </summary>
public class NightsThrowingSpearProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 20;//30 墙体碰撞体问题
        Projectile.height = 20;//82
        Projectile.friendly = true;
        Projectile.penetrate = 3 + 1;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        //Projectile.aiStyle = 3;
        DrawOffsetX = -5;
    }
    //public override void ModifyDamageHitbox(ref Rectangle hitbox)
    //{
    //    hitbox.X += 8;
    //    hitbox.Y += 8;
    //    hitbox.Width -= 16;
    //    hitbox.Height -= 62;
    //}
    //public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    // {
    //    return base.Colliding(projHitbox, targetHitbox);
    //}
    public override void AI()
    {
        //for (int i = 0; i < 4; i++)
        //{
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
            dust.noGravity = true;
        //}
        Projectile.localAI[0]++;
        if (Projectile.localAI[0] == 45) Projectile.velocity *= 2.7f;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit && !Main.player[Projectile.owner].dead)
        {
            var p =Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Main.player[Projectile.owner].Center,
                (target.Center - Main.player[Projectile.owner].Center).SafeNormalize(Vector2.UnitX) * 10,
                ModContent.ProjectileType<NightsThrowingSpearProj2>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            p.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        Projectile.Kill();
        return false;
    }
}
/// <summary>
/// 暗影投矛弹幕
/// </summary>
public class NightsThrowingSpearProj2 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 48;
        Projectile.friendly = true;
        Projectile.tileCollide =false;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        //DrawOffsetX = -5;
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        hitbox.X += 4;
        hitbox.Y += 18;
        hitbox.Width -= 8;
        hitbox.Height -= 36;
    }
    public override void AI()
    {
        //for (int i = 0; i < 4; i++)
        //{
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
        dust.noGravity = true;
        //}
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    }

    //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    //{
    //    if (crit && !Main.player[Projectile.owner].dead)
    //    {
    //        var p = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Main.player[Projectile.owner].Center, (target.Center - Main.player[Projectile.owner].Center).SafeNormalize(Vector2.UnitX) * 10, Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
    //        p.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    //    }
    //}
    //public override bool OnTileCollide(Vector2 oldVelocity)
    //{
    //    SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
    //    Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
    //    Projectile.Kill();
    //    return false;
    //}
}