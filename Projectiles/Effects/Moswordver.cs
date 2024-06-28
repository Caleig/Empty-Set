using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using System.Security.Cryptography.X509Certificates;
using EmptySet.Buffs;

namespace EmptySet.Projectiles.Effects
{
    public class Moswordver : ModProjectile
    {
        int i = 0;
        float to;
        Effect DefaultEffect = ModContent.Request<Effect>("EmptySet/Assets/Effects/Trail").Value;
        Texture2D   MainColor = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/heatmap").Value;
         Texture2D   MainShape = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/Extra_197").Value;
         Texture2D   MaskColor = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/Extra_189").Value;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.netUpdate = true;
            Projectile.ownerHitCheck = true;
            Projectile.timeLeft = 30;
            Projectile.extraUpdates = 1;
            Projectile.width = 2;
            Projectile.height = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vel = Vector2.Normalize(player.Center - Projectile.Center);
            if(i > 20)
            {        
                player.velocity.Y = Vector2.Zero.Y;      
                i = 20;
                Projectile.Kill();
            }
            i++;
            player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * (30-i);
            Projectile.velocity = vel * 15;
            for (int i = Projectile.oldPos.Length - 1; i > 0; --i)
                    Projectile.oldRot[i] = Projectile.oldRot[i - 1];
                Projectile.oldRot[0] = Projectile.rotation;
            base.AI();
        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(player.Center, 20, 20, DustID.Cloud, 0, 0, 0, default, 1.3f);
            }
            player.AddBuff(ModContent.BuffType<Speedfaster>(), 300);
            base.OnKill(timeLeft);
        }
        public override void OnSpawn(IEntitySource source)
        {
            //在冲刺的时候设置弹幕的初始坐标
            Player player = Main.player[Projectile.owner];
            player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 30;
            Vector2 mouse = Vector2.Normalize(player.Center - Main.MouseWorld);
            Projectile.Center = player.Center + mouse * 30;
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(player.Center, 20, 20, DustID.Cloud, 0, 0, 0, default, 1.3f);
            }
            base.OnSpawn(source);
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Player player = Main.player[Projectile.owner];
            Texture2D texture = ModContent.Request<Texture2D>("EmptySet/Items/Weapons/Melee/Broadsword").Value;
            Vector2 pos = player.Center - Main.screenPosition;
            if (player.direction > 0)
            {
                Main.spriteBatch.Draw(texture, (pos + (Vector2.Normalize(Projectile.Center - player.Center) * 18)), null, Color.White, Vector2.Normalize(Main.MouseWorld - player.Center).ToRotation() + MathHelper.PiOver4, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                Main.spriteBatch.Draw(texture, new Vector2(pos.X + 10, pos.Y + 7), null, Color.White, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
        public override void PostDraw(Color lightColor) {
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();

            // 把所有的点都生成出来，按照顺序
            for (int i = 1; i < Projectile.oldPos.Length; ++i) {
                if (Projectile.oldPos[i] == Vector2.Zero) break;
                //spriteBatch.Draw(Main.magicPixel, Projectile.oldPos[i] - Main.screenPosition,
                //    new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), 5f, SpriteEffects.None, 0f);

                int width = 5;
                var normalDir = Projectile.oldPos[i - 1] - Projectile.oldPos[i];
                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X));

                var factor = i / (float)Projectile.oldPos.Length;
                var color = Color.Lerp(Color.White, Color.Red, factor);
                var w = MathHelper.Lerp(1f, 0.05f, factor);

                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + normalDir * width, color, new Vector3((float)Math.Sqrt(factor), 1, w)));
                bars.Add(new CustomVertexInfo(Projectile.oldPos[i] + normalDir * -width, color, new Vector3((float)Math.Sqrt(factor), 0, w)));
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();

            if (bars.Count > 2) {

                // 按照顺序连接三角形
                triangleList.Add(bars[0]);
                var vertex = new CustomVertexInfo((bars[0].Position + bars[1].Position) * 0.5f + Vector2.Normalize(Projectile.velocity) * 30, Color.White,
                    new Vector3(0, 0.5f, 1));
                triangleList.Add(bars[1]);
                triangleList.Add(vertex);
                for (int i = 0; i < bars.Count - 2; i += 2) {
                    triangleList.Add(bars[i]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 1]);

                    triangleList.Add(bars[i + 1]);
                    triangleList.Add(bars[i + 2]);
                    triangleList.Add(bars[i + 3]);
                }


                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);
                RasterizerState originalState = Main.graphics.GraphicsDevice.RasterizerState;
                // 干掉注释掉就可以只显示三角形栅格
                //RasterizerState rasterizerState = new RasterizerState();
                //rasterizerState.CullMode = CullMode.None;
                //rasterizerState.FillMode = FillMode.WireFrame;
                //Main.graphics.GraphicsDevice.RasterizerState = rasterizerState;

                var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));

		// 把变换和所需信息丢给shader
                DefaultEffect.Parameters["uTransform"].SetValue(model * projection);
                DefaultEffect.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = MainColor;
                Main.graphics.GraphicsDevice.Textures[1] = MainShape;
                Main.graphics.GraphicsDevice.Textures[2] = MaskColor;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                //Main.graphics.GraphicsDevice.Textures[0] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[1] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[2] = Main.magicPixel;

                DefaultEffect.CurrentTechnique.Passes[0].Apply();


                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();


            }
        }


        // 自定义顶点数据结构，注意这个结构体里面的顺序需要和shader里面的数据相同
        private struct CustomVertexInfo : IVertexType {
            private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            public Vector2 Position;
            public Color Color;
            public Vector3 TexCoord;

            public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord) {
                this.Position = position;
                this.Color = color;
                this.TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration {
                get {
                    return _vertexDeclaration;
                }
            }
        }
    }
}