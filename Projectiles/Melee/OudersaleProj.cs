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
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 30; //已精确测量
        Projectile.height = 80;
        Projectile.friendly = true;
        Projectile.penetrate = 15 + 1;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
    }


    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (hit.Crit)
        {
            int range = 500;
            var posList = new Vector2[]
            {
                new(range, 0),
                new(-range, 0),
                new(0, range),
                new(0, -range),
            };

            foreach (var v2 in posList)
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.position + v2,
                    -v2.SafeNormalize(Vector2.One) * 8f, ModContent.ProjectileType<BloodSickleProj>(), Projectile.damage,
                    Projectile.knockBack, Projectile.owner, 1);
            }
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
}