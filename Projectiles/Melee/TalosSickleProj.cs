using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 烁金镰镰气
/// </summary>
public class TalosSickleProj:ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("烁金镰镰气");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 26;
        Projectile.height = 26;
        Projectile.friendly = true;
        Projectile.penetrate = 8;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;
    }


    public override void AI()
    {
        Projectile.rotation += 0.5f * Projectile.direction;
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
        dust1.noGravity = true;
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
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.velocity *= 0.4f;
    }
}
