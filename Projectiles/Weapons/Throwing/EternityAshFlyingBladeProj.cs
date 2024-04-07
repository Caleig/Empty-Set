using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 恒灰飞刃弹幕
/// </summary>
public class EternityAshFlyingBladeProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 48; //已精确测量
        Projectile.height = 48;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = 5 + 1;
        Projectile.scale = 0.8f;
        Projectile.light = 0.28f;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
		DrawOffsetX = -4;
	}
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        //SoundEngine.PlaySound(SoundID.Item63, Projectile.position);
        for (int i = 0; i < 4; i++)
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                DustID.Crimslime);
            dust.noGravity = true;
        }
    }
    public override void AI()
    {
        for (int i = 0; i < 2; i++)
        {
            var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                DustID.Silver);
            dust.noGravity = true;
        }
		Projectile.rotation += 0.05f * Projectile.velocity.Length();
		if (Projectile.ai[0] == 0f)
		{
			bool flag = true;
			int num286 = Type;
			if (num286 == 866)
			{
				flag = false;
			}
			if (flag)
			{
				Projectile.ai[1] += 1f;
			}
			if (Projectile.ai[1] >= 50f || Projectile.penetrate == 1)
			{
				Projectile.ai[0] = 1f;
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
		}
		else
		{
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			float num543 = 15f;
			float num554 = 1.5f;

			Vector2 vector42 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num565 = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector42.X;
			float num576 = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector42.Y;
			float num588 = (float)Math.Sqrt(num565 * num565 + num576 * num576);
			if (num588 > 3000f)
			{
				Projectile.Kill();
			}
			num588 = num543 / num588;
			num565 *= num588;
			num576 *= num588;

			if (Projectile.velocity.X < num565)
			{
				Projectile.velocity.X += num554;
				if (Projectile.velocity.X < 0f && num565 > 0f)
				{
					Projectile.velocity.X += num554;
				}
			}
			else if (Projectile.velocity.X > num565)
			{
				Projectile.velocity.X -= num554;
				if (Projectile.velocity.X > 0f && num565 < 0f)
				{
					Projectile.velocity.X -= num554;
				}
			}
			if (Projectile.velocity.Y < num576)
			{
				Projectile.velocity.Y += num554;
				if (Projectile.velocity.Y < 0f && num576 > 0f)
				{
					Projectile.velocity.Y += num554;
				}
			}
			else if (Projectile.velocity.Y > num576)
			{
				Projectile.velocity.Y -= num554;
				if (Projectile.velocity.Y > 0f && num576 < 0f)
				{
					Projectile.velocity.Y -= num554;
				}
			}

			if (Main.myPlayer == Projectile.owner)
			{
				Rectangle rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
				Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
				if (rectangle.Intersects(value12))
				{
					Projectile.Kill();
				}
			}
		}
	}
}