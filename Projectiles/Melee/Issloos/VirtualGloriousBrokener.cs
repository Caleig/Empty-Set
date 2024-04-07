using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using EmptySet.Models;
using Microsoft.Xna.Framework.Graphics;

namespace EmptySet.Projectiles.Melee.Issloos;

/// <summary>
/// 虚光耀破碎者
/// 运动轨迹待修改
/// </summary>
public class VirtualGloriousBrokener : ModProjectile
{
    private bool mouse = false;
    private int timer;
    private int Len = 20;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Virtual Glorious Brokener");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = Len;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.width = 54;
        Projectile.height = 46;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 3 * EmptySet.Frame;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
    }

    public override void AI()
    {
        timer++;
        if (!mouse)
        {
            Projectile.localAI[0] = Main.MouseWorld.X;
            Projectile.localAI[1] = Main.MouseWorld.Y;
            mouse = true;
        }

        /*if (timer < 180)
        {
            Projectile.DirectionalityRotationSet(
                (new Vector2(Projectile.ai[0], Projectile.ai[1]) - Projectile.Center).ToRotation() -
                MathHelper.PiOver4);
        }*/
        Projectile.DirectionalityRotation(0.35f);
        if (Projectile.Center.Distance(new Vector2(Projectile.localAI[0], Projectile.localAI[1])) < 10 || timer >= 180) Projectile.velocity = Vector2.Zero;
        if (timer >= 140) Projectile.alpha += 5;
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PinkTorch);
        dust.noGravity = true;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Color color = new Color(203, 181, 71);
        var list = new List<VertexInfo>();
        for (int i = 1; i < Len; i++)
        {
            if (Projectile.oldPos[i] == Vector2.Zero) continue;
            Vector2 oldCenter = Projectile.oldPos[i] + new Vector2(Projectile.width / 2f, Projectile.height / 2f);
            Vector2 pos = (Projectile.oldPos[i] + new Vector2(Projectile.spriteDirection == 1 ? 38 : 16, 6) - oldCenter).RotatedBy(Projectile.oldRot[i]) + oldCenter;
            list.Add(new(
                pos - Main.screenPosition,
                color,
                new Vector3(i / (float)(Len - 1), 0, 1 - (i / (Len - 1)))
            ));
            list.Add(new(
                pos - Main.screenPosition + new Vector2(0, Projectile.height + 15 - i * Projectile.height / Len).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver4 * Projectile.spriteDirection),
                color,
                new Vector3(i / (float)(Len - 1), 1, 1 - (i / (Len - 1)))
            ));
        }
        Effect GeneralTrailing = EmptySet.Instance.Assets.Request<Effect>("Assets/Effects/GeneralTrailing", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        GeneralTrailing.CurrentTechnique.Passes["Trailing"].Apply();
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, GeneralTrailing, Main.GameViewMatrix.TransformationMatrix);

        Main.graphics.GraphicsDevice.Textures[0] = EmptySet.Instance.Assets.Request<Texture2D>("Assets/Textures/GeneralTrailingFlame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        if (list.Count >= 3)
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin();

        return true;
    }
}