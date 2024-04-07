using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing;

/// <summary>
/// 碎星飞盘弹幕
/// </summary>
public class BrokenStarDiscProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Broken Star Disc");

    }
    public override void SetDefaults()
    {
        Projectile.width = 48;
        Projectile.height = 48;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = 600;
        Projectile.alpha = 0;
        //Projectile.light = 0.5f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.aiStyle = 3;
    }

    //public override bool PreDraw(ref Color lightColor)
    //{
    //    Main.instance.LoadProjectile(Projectile.type);
    //    Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

    //    Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
    //    for (int k = 0; k < Projectile.oldPos.Length; k++)
    //    {
    //        Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
    //        Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
    //        Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
    //    }
    //    return true;
    //}

    public override void AI()
    {
        
        //if (Main.rand.NextBool(5)) 
        //{
        //    int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Frost);
        //    Main.dust[dust].noGravity = true;
        //}
        //if (Main.rand.NextBool(5)) Dust.NewDustPerfect(Projectile.Center, DustID.Frost, Projectile.velocity).noGravity = true;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

        for (int i = 1; i <= 4; i++)
        {
            var star = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center,
                Projectile.velocity + (new Vector2(20).Length() + ((150f + i * 5f) * MathHelper.Pi / 36f)).ToRotationVector2() * 4,
                ProjectileID.FallingStar, Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
            star.aiStyle = 2;
            star.scale = 0.75f;
        }//生成坠星
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return true;
    }
}