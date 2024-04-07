using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 粉凝胶回飞刃弹幕
/// </summary>
public class PinkGelFlybackBladeProj : ModProjectile
{
    //private int FrameCount = 0;
    //bool IsFlyBack = false;
    public override void SetDefaults()
    {
        Projectile.width = 40; //已精确测量
        Projectile.height = 40;
        Projectile.friendly = true; 
        Projectile.penetrate = 3 + 1;
        Projectile.timeLeft = 10 * EmptySet.Frame;
        Projectile.aiStyle = -3;
    }

	public override void AI()
	{
		var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
			DustID.PurpleTorch, 0, 0, 0, Color.Pink);
		dust.noGravity = true;

		Projectile.rotation += 0.035f * Projectile.velocity.Length();
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
			if (Projectile.ai[1] >= 55f || Projectile.penetrate == 1)
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

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
		Projectile.ai[0] = 1f;
		return false;
	}
}