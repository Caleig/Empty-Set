using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss;

/// <summary>
/// 激光瞄准弹幕
/// </summary>
public class AimedProjectile : ModProjectile
{
    private const float MOVE_DISTANCE = 0f;

    public float Distance
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    public int ParentIndex
    {
        get => (int)Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }
    Vector2 nextProjectliePosition;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("激光瞄准");
    }

    public override void SetDefaults()
    {
        Projectile.width = 1;
        Projectile.height = 1;
        //Projectile.alpha = 255;
        Projectile.penetrate = -1;

        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.tileCollide = false;
        Projectile.light = 1;
    }
    public override void AI()
    {
        Projectile.timeLeft = 2;
        Projectile.position = Main.projectile[ParentIndex].Center;
        nextProjectliePosition = (Main.projectile[ParentIndex].Center - Main.npc[(int)Main.projectile[ParentIndex].ai[1]].Center)
            .RotatedBy(MathHelper.ToRadians(45)) + Main.npc[(int)Main.projectile[ParentIndex].ai[1]].Center;
        Projectile.velocity = (nextProjectliePosition - Projectile.Center).SafeNormalize(Vector2.UnitX);
        SetLaserPosition(Main.projectile[ParentIndex].Center);
    }
    private void SetLaserPosition(Vector2 v)
    {
        for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
        {
            var start = v + Projectile.velocity * Distance;
            if (!Collision.CanHit(v, 1, 1, start, 1, 1))
            {
                Distance -= 5f;
                break;
            }
        }
    }
    public override bool PreDraw(ref Color lightColor)
    {
        SpriteBatch spriteBatch = Main.spriteBatch;
        DrawLaser(spriteBatch, ((Texture2D)TextureAssets.Projectile[Projectile.type]), Main.projectile[ParentIndex].Center,
            Projectile.velocity, 20, Projectile.damage, -(float)Math.PI / 2, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
        return false;
    }


    // 绘制激光的核心功能
    /**
		 * start 开始位置
		 * unit 方向
		 * step 每次绘图间隔
		 * rotation 素材绘制旋转角度
		 * scale 缩放
		 * maxDist 最远距离
		 * color 颜色
		 * transDist 绘制起点与start的距离
		 */
    public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
    {
        float r = unit.ToRotation() + rotation;

        // Draws the laser 'body'
        for (float i = transDist; i <= Distance; i += step)
        {
            Color c = Color.White;
            var origin = start + i * unit;
            new Vector2(-(float)Math.Cos(r) * 3 / 2, (float)Math.Sin(r) * 3 / 2);
            spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 0, 1, 20), i < transDist ? Color.Transparent : c, r, new Vector2(0,0), scale, 0, 0);
        }

    }

    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        return false;
    }

}