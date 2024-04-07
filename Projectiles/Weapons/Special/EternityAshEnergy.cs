using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Special;

/// <summary>
/// 恒灰能量
/// </summary>
public class EternityAshEnergy : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 8; //已精确测量
        Projectile.height = 8;
        Projectile.friendly = true;
        //Projectile.scale = 1.6f;
        Projectile.timeLeft = (int)(2.5 * EmptySet.Frame);
    }
    public override void AI()
    {
		Vector2 move = Vector2.Zero;
		float distance = 160f;
		bool target = false;
		for (int k = 0; k < 200; k++)
		{
			if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
			{
				Vector2 newMove = Main.npc[k].Center - Projectile.Center;
				float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
				if (distanceTo < distance)
				{
					move = newMove.SafeNormalize(Vector2.UnitX) * 4;
					distance = distanceTo;
					target = true;
				}
			}
		}
		if (target)
		{
			Projectile.velocity = move;
		}

		var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
        dust.noGravity = true;
		dust.noLight = false;
    }
}