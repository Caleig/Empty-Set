using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 奥德赛拉额外弹幕
/// </summary>
public class OudersaleProj2 : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 50; //已精确测量
        Projectile.height = 50;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;

    }


    public override void AI()
    {
        Projectile.rotation += 0.3f * Projectile.direction;
            Player player = Main.player[Projectile.owner];
            float distanceMax = 400f;
        NPC target = null;
        foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                            distanceMax = currentDistance;
                            target = npc;
                    }
                }
            }
            if(target != null)
            {
                var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 10f;
                Projectile.velocity = (targetVel + Projectile.velocity * 6) / 7f;
            }
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