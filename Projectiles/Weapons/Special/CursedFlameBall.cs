using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Special;

/// <summary>
/// 诅咒焰球
/// </summary>
public class CursedFlameBall : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 34;
        Projectile.height = 34;
        Projectile.scale = 0.9f;
        Projectile.penetrate = 1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 6 * EmptySet.Frame;
        Projectile.light = 0.35f;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 15; i++)
            Dust.NewDust(Projectile.Center + Projectile.velocity, Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X, Projectile.velocity.Y, 255, default(Color), 1.2f);
        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        target.AddBuff(BuffID.CursedInferno, 3 * EmptySet.Frame);
        if (hit.Crit) 
        {
            Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero,
                ModContent.ProjectileType<CursedFlameBallExplode>(), (int)Projectile.damage, 1f, Projectile.owner);
            projectile.Center = Projectile.Center;
        }
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.CursedTorch);
        dust.scale = 1.5f;
        dust.noGravity = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
        for (int i = 0; i < 15; i++)
            Dust.NewDust(Projectile.Center + Projectile.velocity, Projectile.width, Projectile.height, DustID.CursedTorch, Projectile.velocity.X, Projectile.velocity.Y, 255, default(Color), 1.2f);
        return base.OnTileCollide(oldVelocity);
    }
}
public class CursedFlameBallExplode : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_0";
    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 100;
        Projectile.friendly = true;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.light = 1f;
        Projectile.timeLeft = 30;
    }
    public override void AI()
    {
        for (int i = 0; i < 5; i++)
        {
            float num1 = Main.rand.Next(-50, 51);
            float num2 = Main.rand.Next(-50, 51);
            float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
            num3 = Main.rand.Next(4, 8) / num3;
            num1 *= num3;
            num2 *= num3;
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PoisonStaff, Scale: Main.rand.NextFloat(1f, 2f));
            dust.noGravity = true;
            dust.position = Projectile.Center;
            dust.position += new Vector2((float)Main.rand.Next(-5, 6), (float)Main.rand.Next(-5, 6));
            dust.velocity.X = num1;
            dust.velocity.Y = num2;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        => target.AddBuff(BuffID.CursedInferno, 5 * EmptySet.Frame);
}