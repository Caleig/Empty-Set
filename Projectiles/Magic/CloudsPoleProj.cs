using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;
/// <summary>
/// 阴云极点弹幕
/// </summary>
public class CloudsPoleProj : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Clouds Pole");
    }
    public override void SetDefaults()
    {
        Projectile.width = 14;
        Projectile.height = 14;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.timeLeft = 240;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.extraUpdates = 1;
        Projectile.aiStyle = 1;
        AIType = ProjectileID.Bullet;
        Projectile.penetrate = -1;
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Main.instance.LoadProjectile(Projectile.type);
        var texture = TextureAssets.Projectile[Projectile.type].Value;
        var spriteBatch = Main.spriteBatch;

        //如果这个项目是动画的，这将选择正确的帧
        var frame = Main.itemAnimations[Projectile.type] != null ? Main.itemAnimations[Projectile.type].GetFrame(texture, Main.itemFrameCounter[Projectile.whoAmI]) : texture.Frame();

        var frameOrigin = frame.Size() / 2f;
        var offset = new Vector2(Projectile.width / 2f - frameOrigin.X, Projectile.height - frame.Height);
        var drawPos = Projectile.position - Main.screenPosition + frameOrigin + offset;

        var time = Main.GlobalTimeWrappedHourly;
        var timer =  +time * 0.04f;

        time %= 4f;
        time /= 2f;

        if (time >= 1f)
            time = 2f - time;

        time = time * 0.5f + 0.5f;

        for (var i = 0f; i < 1f; i += 0.25f)
        {
            var radians = (i + timer) * MathHelper.TwoPi;
            spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        }

        for (var i = 0f; i < 1f; i += 0.34f)
        {
            var radians = (i + timer) * MathHelper.TwoPi;
            spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), Projectile.rotation, frameOrigin, Projectile.scale, SpriteEffects.None, 0);
        }

        return true;
    }
}