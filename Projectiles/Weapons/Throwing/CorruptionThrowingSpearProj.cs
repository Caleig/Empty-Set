using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 腐化投矛弹幕
/// </summary>
public class CorruptionThrowingSpearProj : ModProjectile
{
	bool canHitNPC = true;
	bool onHitNPC = false;
	public override void SetDefaults()
    {
        Projectile.width = 8;//26; //已精确测量
        Projectile.height = 8;//60;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        Projectile.aiStyle = -1;
		Projectile.penetrate = -1;
		Projectile.MaxUpdates = 1;
		DrawOffsetX = -9;
	}
	public override void AI()
	{
		if (Main.rand.NextBool(2))
		{
			int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Demonite);
			Main.dust[dust].noGravity = true;
		}
		if (Main.rand.NextBool(5)) Dust.NewDustPerfect(Projectile.Center, 14, Projectile.velocity).noGravity = true;

		if (Projectile.alpha > 0)
		{
			Projectile.alpha -= 25;
		}
		if (Projectile.alpha < 0)
		{
			Projectile.alpha = 0;
		}

		if (Projectile.ai[0] == 0f)
		{
			Projectile.ai[1]++;
			Projectile.velocity.Y += 0.07f;
			Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}

		if (Projectile.ai[0] == 1f)
		{
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			int num891 = 5;
			bool kill = false;
			canHitNPC = false;
			Projectile.localAI[0]++;
			if (Projectile.localAI[0] % 60f == 0f)
			{
				canHitNPC = true;
			}
			if (Projectile.localAI[0] >= (float)(60 * num891))
			{
				kill = true;
			}
			else if ((int)Projectile.ai[1] < 0 || (int)Projectile.ai[1] >= 200)
			{
				kill = true;
			}
			else if (Main.npc[(int)Projectile.ai[1]].active && !Main.npc[(int)Projectile.ai[1]].dontTakeDamage && !Main.npc[(int)Projectile.ai[1]].GetGlobalNPC<EmptySetNPC>().flag)
			{
				Main.npc[(int)Projectile.ai[1]].GetGlobalNPC<EmptySetNPC>().flag = ++Main.npc[(int)Projectile.ai[1]].GetGlobalNPC<EmptySetNPC>().num < 3 ? false : true;
				onHitNPC = true;
				Projectile.velocity = Microsoft.Xna.Framework.Vector2.Zero;
				Projectile.position = Main.npc[(int)Projectile.ai[1]].Center;
				if (canHitNPC)
				{
					Main.npc[(int)Projectile.ai[1]].HitEffect(0, 1);
				}
			}
			else
			{
				kill = true;
			}
			if (kill)
			{
				Projectile.Kill();
			}
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		return !canHitNPC || target.GetGlobalNPC<EmptySetNPC>().flag ? false : base.CanHitNPC(target);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (onHitNPC) modifiers.SourceDamage /= 5;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.active && !target.dontTakeDamage)
		{
			Projectile.ai[1] = target.whoAmI;
			Projectile.ai[0] = 1f;
			target.immune[Projectile.owner] = 0;
		}
	}
	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
		return base.OnTileCollide(oldVelocity);
	}
}