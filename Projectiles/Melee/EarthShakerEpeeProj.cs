using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

internal class EarthShakerEpeeProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("撼地者重剑剑气");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 72;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 2 + 1;
        Projectile.timeLeft = 10 * EmptySet.Frame;
        Projectile.alpha = 0;
        Projectile.light = 0.3f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = true;
        // Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        //hitbox.X -= 2;
        //hitbox.Y -= 6;
        //hitbox.Width -= 4;
        //hitbox.Height -= 12;
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
