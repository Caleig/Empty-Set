using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Common.Abstract.Projectiles;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Extensions;
using EmptySet.Models;

namespace EmptySet.Projectiles.TEST;

public class SuperGoProj1  :ModProjectile
{
    public override string Texture => "EmptySet/Projectiles/TEST/SuperGoProj";
    private const int Len = 8;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("super go");
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = Len; // 要记录的旧位置长度
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // 记录模式
    }
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 99;
        Projectile.timeLeft = 3 * EmptySet.Frame;
        //Projectile.Center = new Vector2(Projectile.position.X + Projectile.height,
        //    Projectile.position.Y + Projectile.width / 2f);
        //Projectile.alpha = 0;
        //Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        //Projectile.extraUpdates = 1;
        Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }

    //private bool first = false;
    public override void AI()
    {
        
        //Projectile.Center=new Vector2(Projectile.position.X- Projectile.height, Projectile.position.Y + Projectile.width / 2f);
        //lock target to center bottom
        Projectile.DirectionalityRotation(0.07f);
        //Projectile.velocity = Vector2.Zero;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        var list = new List<VertexInfo>();
        for (int i = 1; i < Len; i++)
        {
            if (Projectile.oldPos[i] == Vector2.Zero) continue;
            list.Add(new(
                Projectile.oldPos[i] - Main.screenPosition + Projectile.oldRot[i].ToRotationVector2() * 1f + Projectile.oldRot[i].ToRotationVector2() * 40f * ((Len - 1 - (float) i) / (Len - 1)),
                Color.Red * (float) Math.Sqrt(((Len - 1) - (float) i) / (Len - 1)),
                new Vector3(i / (float) (Len - 1), 0, 1 - (i / (Len - 1)))
            ));
            list.Add(new(
                Projectile.oldPos[i] - Main.screenPosition + Projectile.oldRot[i].ToRotationVector2() * 1f - Projectile.oldRot[i].ToRotationVector2() * 40f * ((Len - 1 - (float)i) / (Len - 1)),
                Color.Red * (float)Math.Sqrt(((Len - 1) - (float)i) / (Len - 1)),
                new Vector3(i / (float)(Len - 1), 1, 1 - (i / (Len - 1)))
            ));
        }
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.Additive,SamplerState.AnisotropicClamp,DepthStencilState.None,RasterizerState.CullNone,null,Main.GameViewMatrix.TransformationMatrix);
        Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
        if (list.Count >= 3)
            Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
        Main.spriteBatch.End();
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

        return true;
    }
}

public class SuperGoProj2 : VertexProjectile
{
    public override string Texture => "EmptySet/Projectiles/TEST/SuperGoProj";
    protected override int TrailCacheLength => 30;
    protected override int TrailingMode => 2;
    protected override LinkMode LinkMode => LinkMode.Triangle;
    protected override bool IsShow => true;

    protected override List<VertexInfo> GetVertexList(List<VertexInfo> list)
    {
        for (int i = 1; i < TrailCacheLength; i++)
        {
            if (Projectile.oldPos[i] == Vector2.Zero) continue;
            var pos = Projectile.oldPos[i];
            pos = new Vector2(pos.X + Projectile.width / 2f, pos.Y + Projectile.height / 2f);
            /*+ Projectile.oldRot[i].ToRotationVector2() * 1f*/
            list.Add(new(
                pos - Main.screenPosition + Projectile.oldRot[i].ToRotationVector2() * 4f * ((TrailCacheLength - 1 + (float)i) / (TrailCacheLength - 1)),
                Color.BlueViolet * (float)Math.Sqrt(((TrailCacheLength - 1) - (float)i) / (TrailCacheLength - 1)),
                new ()
            ));
            list.Add(new(
                pos - Main.screenPosition - Projectile.oldRot[i].ToRotationVector2() * 4f * ((TrailCacheLength - 1 + (float)i) / (TrailCacheLength - 1)),
                Color.DeepPink * (float)Math.Sqrt(((TrailCacheLength - 1) - (float)i) / (TrailCacheLength - 1)),
                new ()
            ));

         
        }

        return list;
    }

    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();//must
        // DisplayName.SetDefault("super go");
    }
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.penetrate = 99;
        Projectile.timeLeft = 3 * EmptySet.Frame;
        //Projectile.Center = new Vector2(Projectile.position.X + Projectile.height,
        //    Projectile.position.Y + Projectile.width / 2f);
        //Projectile.alpha = 0;
        //Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        //Projectile.extraUpdates = 1;
        Projectile.aiStyle = 1;
        //AIType = ProjectileID.Bullet;
    }

    //private bool first = false;
    public override void AI()
    {

        //Projectile.Center=new Vector2(Projectile.position.X- Projectile.height, Projectile.position.Y + Projectile.width / 2f);
        //lock target to center bottom
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        //Projectile.velocity = Vector2.Zero;
    }
    

    
}