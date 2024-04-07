
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Ranged;

public class NightsBowArrow : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_1";
    private int Timer
    {
        get { return (int)Projectile.localAI[0]; }
        set { Projectile.localAI[0] = value; }
    }

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("永夜箭");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 5;
        Projectile.height = 5;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = 1;
        Projectile.timeLeft = 300;
        Projectile.alpha = 255;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.extraUpdates = 1;
        Projectile.arrow = true;
    }

    public override void AI()
    {
        Vector2 vecToMse = new(Projectile.ai[0], Projectile.ai[1]);
        Projectile.alpha -= 10;
        if (Projectile.alpha <= 0) Projectile.alpha = 0;
        if (Timer == 0)
        {
            Projectile.rotation = vecToMse.ToRotation() + MathHelper.PiOver2;
            Projectile.CritChance = Main.player[Projectile.owner].GetWeaponCrit(Main.player[Projectile.owner].HeldItem);
        }
        if (Timer <= 60 / Projectile.localAI[1])
            Projectile.tileCollide = false;
        else
        {
            Projectile.tileCollide = true;
            Projectile.velocity = vecToMse.SafeNormalize(Vector2.UnitX) * Projectile.localAI[1];
            Dust.NewDustDirect(Projectile.position, 5, 5, DustID.Shadowflame).noGravity = true;
            Dust.NewDustPerfect(Projectile.position, DustID.Shadowflame, Projectile.velocity).noGravity = true;
        }
        Timer++;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
        Vector2 drawOrigin = texture.Bounds.Size() / 2;
        for (int k = 0; k < Projectile.oldPos.Length; k++)
        {
            Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + Projectile.Size / 2 + new Vector2(0f, Projectile.gfxOffY);
            Color color = Color.DarkViolet * ((float)(Projectile.oldPos.Length - k) / Projectile.oldPos.Length) * 0.5f * Projectile.Opacity;
            Main.EntitySpriteDraw(texture, drawPos, texture.Bounds, color, Projectile.oldRot[k], drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, Projectile.position - Main.screenPosition + Projectile.Size / 2, texture.Bounds, Color.Black * Projectile.Opacity, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 10; i++)
            Dust.NewDustPerfect(Projectile.position, DustID.Shadowflame, Projectile.velocity).noGravity = true;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.ShadowFlame, 90);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        if (Main.rand.NextBool(3))
            target.AddBuff(BuffID.ShadowFlame, 90);
    }
}