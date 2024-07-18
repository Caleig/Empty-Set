using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 苍翠钺弹幕
/// </summary>
public class ChlorophyteAxeProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // 记录模式
    }

    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 30;//54
        Projectile.friendly = true;
        Projectile.penetrate = 2;
        Projectile.tileCollide = false;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);

    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        for (int i = 0; i < 3; i++)
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0, MathHelper.Pi * 2)) * 3, ProjectileID.SporeCloud, Projectile.damage, Projectile.knockBack, Main.myPlayer);
        return base.OnTileCollide(oldVelocity);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        base.OnHitNPC(target, hit,damageDone);
        for (int i = 0; i < 3; i++)
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, Vector2.UnitX.RotatedBy(Main.rand.NextFloat(0, MathHelper.Pi * 2)) * 3, ProjectileID.SporeCloud, Projectile.damage, Projectile.knockBack, Main.myPlayer);
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
