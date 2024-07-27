using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 狂炎弹幕
/// </summary>
public class FrenziedFlameProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
	}
    public override void SetDefaults()
    {
        Projectile.width = 50; //已精确测量
        Projectile.height = 50;
        Projectile.friendly = true;
        Projectile.penetrate = 8 + 1;
        Projectile.timeLeft = 10 * EmptySet.Frame;

		Projectile.tileCollide = false;
		Projectile.ignoreWater = false;
        Projectile.aiStyle = -3;
    }

	public override void AI()
	{
	    var yellow = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.PurpleTorch, Scale: Main.rand.NextFloat(1f, 2f));
        yellow.noGravity = true;
        var blue = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Scale: Main.rand.NextFloat(1f, 2f));
        blue.noGravity = true;

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
			if (Projectile.ai[1] >= 7f || Projectile.penetrate == 1)
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
			float num543 = 80f;
			float num554 = 10f;

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
	    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 15; i++)
        SoundEngine.PlaySound(SoundID.Item20, Projectile.position);
		target.AddBuff(BuffID.BetsysCurse, 2 * EmptySet.Frame);
		target.AddBuff(BuffID.Oiled, 3 * EmptySet.Frame);
        target.AddBuff(BuffID.ShadowFlame, 5 * EmptySet.Frame);
        if (hit.Crit)
        {
            Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero,
                ModContent.ProjectileType<FrenziedExplode>(), (int)Projectile.damage, 1f, Projectile.owner);
            projectile.Center = Projectile.Center;
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

        // 用不受光线影响的颜色重新绘制投射体
        Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
            Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        }

        return true;
    }

    public class FrenziedExplode : ModProjectile
    {
    public override string Texture => "Terraria/Images/Projectile_0";
    public override void SetDefaults()
    {
        Projectile.width = 90;
        Projectile.height = 90;
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

            float num1 = Main.rand.Next(-50, 51);
            float num2 = Main.rand.Next(-50, 51);
            float num3 = (float)Math.Sqrt(num1 * num1 + num2 * num2);
            num3 = Main.rand.Next(4, 12) / num3;
            num1 *= num3;
            num2 *= num3;
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Shadowflame, Scale: Main.rand.NextFloat(2f, 3f));
            dust.noGravity = true;
            dust.position = Projectile.Center;
            dust.position += new Vector2((float)Main.rand.Next(-5, 6), (float)Main.rand.Next(-5, 6));
            dust.velocity.X = num1;
            dust.velocity.Y = num2;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        => target.AddBuff(BuffID.ShadowFlame, 9 * EmptySet.Frame);
    }
}
