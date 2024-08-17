using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Ranged;

/// <summary>
/// 导弹弹幕
/// </summary>
public class MissileProj : ModProjectile
{
    Dust dust;
    public override void SetDefaults()
    {
        Projectile.width = 12;//22; //已精确测量
        Projectile.height = 12;//70;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
		Projectile.ignoreWater = false;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        Projectile.DamageType = DamageClass.Throwing;
        DrawOffsetX = -4;
    }
    public override void AI() 
    {
        Projectile.localAI[0]++;
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        if (Projectile.localAI[0] == 81) Projectile.velocity *= 6.3f;
        if (Main.rand.NextBool(2))
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(0, 12).RotatedBy(Projectile.rotation), 1, 1, DustID.Electric);
            dust.noGravity = true;
            dust.fadeIn = 1f;
        }
        if (Main.rand.NextBool(1))
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(0, 12).RotatedBy(Projectile.rotation), 1, 1, DustID.Flare_Blue, Scale: Main.rand.NextFloat(2f, 3f));
            dust.noGravity = true;
            dust.fadeIn = 2f;
        }
        Player player = Main.player[Projectile.owner];
        float distanceMax = 400f;
        NPC target = null;
        foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                            distanceMax = currentDistance;
                            target = npc;
                    }
                }
            }
            if(target != null)
            {
                var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 21f;
                Projectile.velocity = (targetVel + Projectile.velocity * 8) / 9f;
            }
    }
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
		Projectile.ai[0] = 1f;
		return false;
	}
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
        modifiers.ScalingArmorPenetration += 3 ;
        modifiers.FinalDamage.Flat = 540;
        base.ModifyHitNPC(target, ref modifiers);
        {
            Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero,
                ModContent.ProjectileType<missile>(), (int)Projectile.damage, 1f, Projectile.owner);
            projectile.Center = Projectile.Center;
        }
    }

    public class missile : ModProjectile
    {
    public override string Texture => "Terraria/Images/Projectile_0";
    public override void SetDefaults()
    {
        Projectile.width = 120;
        Projectile.height = 120;
        Projectile.friendly = true;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.light = 1f;
        Projectile.timeLeft = 10;
    }
    public override void AI()
    {
        for (int i = 0; i < 5; i++)
        {

            float num1 = Main.rand.Next(-70, 71);
            float num2 = Main.rand.Next(-70, 71);
            float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
            num3 = Main.rand.Next(4, 20) / num3;
            num1 *= num3;
            num2 *= num3;
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Flare_Blue, Scale: Main.rand.NextFloat(3f, 4f));
            dust.noGravity = true;
            dust.position = Projectile.Center;
            dust.position += new Vector2((float)Main.rand.Next(-5, 6), (float)Main.rand.Next(-5, 6));
            dust.velocity.X = num1;
            dust.velocity.Y = num2;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        => target.AddBuff(BuffID.Frostburn2, 3 * EmptySet.Frame);
    }
}
