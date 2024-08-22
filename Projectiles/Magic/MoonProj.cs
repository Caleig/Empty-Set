using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

/// <summary>
/// 寒月弹幕
/// </summary>
public class MoonProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 1; // 记录模式
	}
	Dust dust;
    public override void SetDefaults()
    {
        Projectile.width = 50;//22; //已精确测量
        Projectile.height = 50;//70;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);

        Projectile.tileCollide = false;
		Projectile.ignoreWater = false;
        Projectile.aiStyle = -3;
        Projectile.DamageType = DamageClass.Magic;
        DrawOffsetX = -4;
    }
    public override void AI() 
    {
        Projectile.localAI[0]++;
        Projectile.rotation += 0.3f * Projectile.direction;
        if (Projectile.localAI[0] == 45) Projectile.velocity *= 2.7f;
        if (Main.rand.NextBool(1))
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(0, 30).RotatedBy(Projectile.rotation), 0, 0, DustID.Flare_Blue, Scale: Main.rand.NextFloat(2f, 3f));
            dust.noGravity = true;
            dust.fadeIn = 0f;
        }
        if (Main.rand.NextBool(1))
        {
            dust = Dust.NewDustDirect(Projectile.position + new Vector2(30, -25).RotatedBy(Projectile.rotation), 0, 0, DustID.Flare_Blue, Scale: Main.rand.NextFloat(2f, 3f));
            dust.noGravity = true;
            dust.fadeIn = 0f;
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
        SoundEngine.PlaySound(SoundID.Item92, Projectile.position);
        target.AddBuff(BuffID.Frostburn2, 7 * EmptySet.Frame);
        {
            Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero,
                ModContent.ProjectileType<MoonExplode>(), (int)Projectile.damage, 1f, Projectile.owner);
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

    public class MoonExplode : ModProjectile
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
        => target.AddBuff(BuffID.Frostburn2, 10 * EmptySet.Frame);
    }
}
