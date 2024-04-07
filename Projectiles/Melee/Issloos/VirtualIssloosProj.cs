using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Extensions;
using EmptySet.Models;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee.Issloos;

/// <summary>
/// 虚伊希鲁斯弹幕
/// </summary>
public class VirtualIssloosProj : ModProjectile
{
    private int Len = 35;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Virtual Issoos");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = Len;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.width = 158;
        Projectile.height = 138;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 4 * EmptySet.Frame;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
    }

    public override void AI() => Projectile.DirectionalityRotation(0.15f);
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Main.player[Projectile.owner].HealEffect(1);
        Main.player[Projectile.owner].statLife += 1;
    }

    public override void OnKill(int timeLeft)
    {
        Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.position, Vector2.Zero,
            ModContent.ProjectileType<VirtualIssloosExplode>(), (int)Projectile.damage, 1f, Projectile.owner);
        projectile.Center = Projectile.Center;
        Main.player[Projectile.owner].HealEffect(25);
        Main.player[Projectile.owner].statLife += 25;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Color color = new Color(50, 200, 20);
        var list = new List<VertexInfo>();
        for (int i = 1; i < Len; i++)
        {
            if (Projectile.oldPos[i] == Vector2.Zero) continue;
            Vector2 oldCenter = Projectile.oldPos[i] + new Vector2(Projectile.width / 2f, Projectile.height / 2f);
            Vector2 pos = (Projectile.oldPos[i] + new Vector2(Projectile.spriteDirection == 1 ? 94 : 32, 14) - oldCenter).RotatedBy(Projectile.oldRot[i]) + oldCenter;

            var factor = i / (float)Projectile.oldPos.Length;
            var w = MathHelper.Lerp(1f, 0.05f, factor);

            list.Add(new(
                pos - Main.screenPosition,
                color,
                new Vector3(i / (float)(Len - 1), 0, 1 - (i / (Len - 1)))
            ));
            list.Add(new(
                pos - Main.screenPosition + new Vector2(0, Projectile.height + 10 - i * Projectile.height / Len).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver4 * Projectile.spriteDirection),
                color,
                new Vector3(i / (float)(Len - 1), 1, 1 - (i / (Len - 1)))
            ));
        }
        
        Effect GeneralTrailing = EmptySet.Instance.Assets.Request<Effect>("Assets/Effects/GeneralTrailing", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
        GeneralTrailing.CurrentTechnique.Passes["Trailing"].Apply();
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, GeneralTrailing);

        Main.graphics.GraphicsDevice.Textures[0] = EmptySet.Instance.Assets.Request<Texture2D>("Assets/Textures/GeneralTrailingFlame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

        if (list.Count >= 3)
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin();
        
        return true;
    }
}