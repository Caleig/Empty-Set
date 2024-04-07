using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Models;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Abstract.Projectiles;

public abstract class VertexProjectile : ModProjectile
{
    /// <summary>
    /// 拖影长度
    /// </summary>
    protected abstract int TrailCacheLength { get; }
    /// <summary>
    /// 拖影模式？
    /// </summary>
    protected abstract int TrailingMode { get; }
    /// <summary>
    /// 要绘制的顶点列表
    /// </summary>
    /// <returns></returns>
    protected abstract List<VertexInfo> GetVertexList(List<VertexInfo> list);
    /// <summary>
    /// 绘制模式
    /// </summary>
    protected abstract LinkMode LinkMode { get; }
    /// <summary>
    /// 指示是否调用系统（原本）绘制
    /// </summary>
    protected abstract bool IsShow { get; }

    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = TrailCacheLength; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = TrailingMode; // 记录模式
    }

    public override bool PreDraw(ref Color lightColor)
    {
        var list = GetVertexList(new List<VertexInfo>());
        
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
        var countRequire = LinkMode switch
        {
            LinkMode.Line => 2,
            LinkMode.Triangle => 3,
            _=> throw new Exception("no match LinkMode type")
        };
        var linkRequire = countRequire - 1;

        if ( list.Count >= countRequire)
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - linkRequire);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        
        return IsShow;
    }

}