using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 奥德赛拉弹幕
/// </summary>
public class OudersaleProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 50; //已精确测量
        Projectile.height = 50;
        Projectile.friendly = true;
        Projectile.penetrate = 6;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;
    }


    public override void AI()
    {
        Projectile.rotation += 0.3f * Projectile.direction;

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
}