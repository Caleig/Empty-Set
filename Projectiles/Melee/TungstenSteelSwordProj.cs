using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 钨钢剑弹幕
/// </summary>

public class TungstenSteelSwordProj : ModProjectile
{
    int _HitTime = 0;
    int _HitTime2 = 0;
    private Texture2D tex;
    private Vector2[] oldPosi = new Vector2[3];
    private Vector2[] oldVec = new Vector2[3];
    private float[] oldRot = new float[3];
    private int frametime = 0;
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        base.SetStaticDefaults();
    }
    public override void SetDefaults()
    {
        Projectile.width = 26; //已精确测量
        Projectile.height = 46;
        Projectile.friendly = true;
        Projectile.penetrate = 3 + 1;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (12 * EmptySet.Frame);
        Projectile.alpha = 50;
        Projectile.Opacity = 0.6f;
        tex = ModContent.Request<Texture2D>("EmptySet/Projectiles/Melee/TungstenSteelSwordProj").Value;
        
    }
    public override void AI()
    {
        Projectile.rotation += 0.3f * Projectile.direction;
        if (Projectile.timeLeft < (11.5f * EmptySet.Frame))
        {
            Projectile.velocity *= 0.9f;
            Projectile.Opacity -= 0.05f;
        }
        else if(_HitTime2 > 0)
            {
                Projectile.velocity *= 0.9f;
                Projectile.Opacity -= 0.05f;
        }
        if (Projectile.Opacity < 0)
        {
            Projectile.Kill();
            _HitTime2 = 0;
        }
            
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        _HitTime2 = 1;
    }
    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        _HitTime++;
        if (_HitTime >= 3)
        {
            modifiers.SetCrit();
        }
        base.ModifyHitNPC(target, ref modifiers);
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 200, 0) * Projectile.Opacity;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
        int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
        int y3 = num156 * Projectile.frame;
        Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
        Vector2 origin2 = rectangle.Size() / 2f;

        Color color26 = lightColor;
        color26 = Projectile.GetAlpha(color26);

        for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i++)
        {
            Color color27 = color26;
            color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
            Vector2 value4 = Projectile.oldPos[i];
            float num165 = Projectile.oldRot[i];
            Main.EntitySpriteDraw(texture2D13, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, Projectile.scale, SpriteEffects.None, 0);
        }

        Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation, origin2, Projectile.scale, SpriteEffects.None, 0);
        return false;
    }
}
