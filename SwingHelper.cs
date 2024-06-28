using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;

namespace FighterSickle
{
    public class SwingHelper
    {
        public struct CustomVertex : IVertexType
        {
            public static VertexDeclaration vertexDeclaration = new(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            public Vector2 Position;
            public Color Color;
            public Vector3 TexCoord;
            public CustomVertex(Vector2 position, Color color, Vector3 texCoord)
            {
                Position = position;
                Color = color;
                TexCoord = texCoord;
            }
            public VertexDeclaration VertexDeclaration => vertexDeclaration;
        }
        public Projectile projectile;
        /// <summary>
        /// 弹幕的头部位置
        /// </summary>
        public Vector2 ProjHeadPos => projectile.Center + projectile.velocity;
        /// <summary>
        /// 起点向量
        /// </summary>
        public Vector2 StartVel;
        public Vector2 VelScale;
        public Vector2[] oldVels;
        public float VisualRotation;
        /// <summary>
        /// 挥舞的启用
        /// </summary>
        private bool _acitveSwing;
        private bool _changeLerpInvoke;
        private readonly float HalfSizeLength;
        private float _velLerp;
        public SwingHelper(Projectile proj, int oldVelLength)
        {
            projectile = proj;
            HalfSizeLength = proj.Size.Length() * 0.5f;
            oldVels = new Vector2[oldVelLength];
        }
        public void Change(Vector2 startVel, Vector2 velScale, float visualRotation = 0f)
        {
            StartVel = startVel;
            StartVel.Normalize();
            VisualRotation = visualRotation;
            VelScale = velScale;
        }
        public void Change_Lerp(Vector2 startVel, float velLerp, Vector2 velScale, float scaleAmount, float visualRotation = 0f, float visualRotAmount = 0.1f)
        {
            startVel.Normalize();
            StartVel = startVel;
            _velLerp = velLerp;
            VelScale = Vector2.Lerp(VelScale, velScale, scaleAmount);
            VisualRotation = MathHelper.Lerp(VisualRotation, visualRotation, visualRotAmount);
            _changeLerpInvoke = true;
        }
        /// <summary>
        /// 挥舞函数
        /// </summary>
        /// <param name="velLength">弹幕长度</param>
        /// <param name="dir">朝向</param>
        /// <param name="Rot">旋转弧度</param>
        public void SwingAI(float velLength, int dir, float Rot)
        {
            if (_acitveSwing)
            {
                Vector2 start = StartVel;
                start.X *= dir;

                Vector2 vel = start;

                if (_changeLerpInvoke)
                {
                    //projectile.velocity = Vector2.Lerp(projectile.velocity, vel * velLength, _velLerp);
                    vel *= VelScale;
                    if (projectile.velocity == default) projectile.velocity = Vector2.One;
                    projectile.velocity = projectile.velocity.
                        RotatedBy(MathHelper.WrapAngle(vel.ToRotation() - projectile.velocity.ToRotation()) * _velLerp).
                        SafeNormalize(default) * MathHelper.Lerp(projectile.velocity.Length(), velLength, _velLerp);
                }
                else
                {
                    vel = vel.RotatedBy(Rot * dir);
                    vel *= VelScale;
                    projectile.velocity = vel * velLength;
                }
            }

            for (int i = oldVels.Length - 1; i >= 0; i--)
            {
                if (_acitveSwing && !_changeLerpInvoke)
                {
                    if (i == 0)
                    {
                        oldVels[0] = projectile.velocity;
                    }
                    else
                    {
                        oldVels[i] = oldVels[i - 1];
                    }
                }
                else
                {
                    oldVels[i] = default;
                }
            }
            projectile.timeLeft = 2;
            _acitveSwing = _changeLerpInvoke = false;
        }
        /// <summary>
        /// 将弹幕锁定位置在玩家身上   <para>当<paramref name="isUseSwing"/>为ture的时候,
        /// 则会根据<code> MathF.Atan2(projectile.velocity.Y * player.direction, projectile.velocity.X * player.direction)</code>
        /// 决定玩家手臂方向,同时设置使用时间和挥舞时间为2</para>
        /// </summary>
        /// <param name="player"></param>
        /// <param name="length">根据弹幕速度,决定最终位置  就是位置加上速度的单位向量乘以的这个<paramref name="length"/></param>
        /// <param name="isUseSwing">如果开始挥舞,请设置这个为true,当这个为true的时候,调用挥舞函数才能使挥舞继续</param>
        public void ProjFixedPlayerCenter(Player player, float length = 0f, bool isUseSwing = false)
        {
            projectile.position = player.RotatedRelativePoint(player.MountedCenter) - projectile.Size * 0.5f;
            projectile.position += projectile.velocity.SafeNormalize(default) * length;
            if (isUseSwing)
            {
                SetSwingActive();
                player.itemAnimation = player.itemTime = 2;
                player.itemRotation = MathF.Atan2(projectile.velocity.Y * player.direction, projectile.velocity.X * player.direction);
            }
        }
        /// <summary>
        /// 将弹幕锁定位置在对应位置上
        /// </summary>
        /// <param name="pos">位置</param>
        /// <param name="length">根据弹幕速度,决定最终位置  就是位置加上速度的单位向量乘以的这个<paramref name="length"/></param>
        public void ProjFixedPos(Vector2 pos, float length = 0f)
        {
            projectile.position = pos + projectile.Size * 0.5f;
            projectile.position += projectile.velocity.SafeNormalize(default) * length;
        }
        public void SetSwingActive()
        {
            _acitveSwing = true;
        }
        public void Swing_Draw_All(Color drawColor,Texture2D tex, Func<float, Color> colorFunc, Effect effect = null)
        {
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone);

            DrawSwingItem(drawColor);
            DrawTrailing(tex, colorFunc, effect);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.Transform);
        }

        public void Swing_Draw(Color drawColor)
        {
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicWrap, DepthStencilState.Default, RasterizerState.CullNone);

            DrawSwingItem(drawColor);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.Transform);
        }

        private void DrawSwingItem(Color drawColor)
        {
            GraphicsDevice gd = Main.graphics.GraphicsDevice;
            //var origin = gd.RasterizerState;
            //RasterizerState rasterizerState = new RasterizerState();
            //rasterizerState.CullMode = CullMode.None;
            //rasterizerState.FillMode = FillMode.WireFrame;
            //gd.RasterizerState = rasterizerState
            Vector2 v = new Vector2(-projectile.velocity.Y, projectile.velocity.X).RotatedBy(VisualRotation * projectile.spriteDirection).SafeNormalize(default)
                * HalfSizeLength * Main.LocalPlayer.direction;
            Vector2[] pos = new Vector2[4]
            {
                projectile.Center - Main.screenPosition,
                projectile.Center - v + projectile.velocity * 0.5f - Main.screenPosition,
                projectile.Center + projectile.velocity - Main.screenPosition,
                projectile.Center + v + projectile.velocity * 0.5f - Main.screenPosition
            };
            CustomVertex[] customVertices = new CustomVertex[6];
            customVertices[0] = customVertices[5] = new(pos[0], drawColor, new Vector3(0, 1, 0)); // 柄
            customVertices[1] = new(pos[1], drawColor, new Vector3(0, 0, 0)); // 左上角
            customVertices[2] = customVertices[3] = new(pos[2], drawColor, new Vector3(1, 0, 0)); // 头
            customVertices[4] = new(pos[3], drawColor, new Vector3(1, 1, 0)); // 右下角

            gd.Textures[0] = TextureAssets.Projectile[projectile.type].Value;
            //gd.Textures[0] = TextureAssets.MagicPixel.Value;
            gd.DrawUserPrimitives(PrimitiveType.TriangleList, customVertices, 0, 2);
            //gd.RasterizerState = origin;
        }

        /// <summary>
        /// 绘制拖尾
        /// </summary>
        /// <param name="tex">拖尾贴图</param>
        /// <param name="colorFunc">拖尾颜色</param>
        /// <param name="effect">调用的shader 默认不选择调用,如果调用,则需要让shader处理顶点信息转换到屏幕上</param>
        public void Swing_TrailingDraw(Texture2D tex, Func<float, Color> colorFunc, Effect effect = null)
        {
            if (tex == null || colorFunc == null)
            {
                return;
            }
            SpriteBatch sb = Main.spriteBatch;
            sb.End();
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            DrawTrailing(tex, colorFunc, effect);

            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None,
                Main.Rasterizer, null, Main.Transform);
        }

        private void DrawTrailing(Texture2D tex, Func<float, Color> colorFunc, Effect effect)
        {
            List<CustomVertex> customVertices = new();
            int length = oldVels.Length;
            for (int i = 0; i < length; i++)
            {
                Vector2 vel = oldVels[i];
                if (vel == default)
                {
                    break;
                }

                float factor = (float)i / length;
                Color drawColor = colorFunc.Invoke(factor); // 获取绘制颜色

                Vector2 pos = projectile.Center;
                if (effect == null)
                {
                    pos -= Main.screenPosition;
                }

                customVertices.Add(new(pos, drawColor, new Vector3(factor, 0, 0)));
                customVertices.Add(new(pos + vel, drawColor, new Vector3(factor, 1, 0)));
            }
            if (customVertices.Count > 2)
            {
                List<CustomVertex> vertices = GenerateTriangle(customVertices);
                GraphicsDevice gd = Main.graphics.GraphicsDevice;
                gd.Textures[0] = tex;

                effect?.CurrentTechnique.Passes[0].Apply();
                gd.DrawUserPrimitives(PrimitiveType.TriangleList, vertices.ToArray(), 0, vertices.Count / 3);
            }
        }

        public static Matrix Effect_GetScreenMatrix()
        {
            var projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
            var model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
            return model * projection;
        }
        public static List<CustomVertex> GenerateTriangle(List<CustomVertex> customs)
        {
            List<CustomVertex> triangleList = new();
            for (int i = 0; i < customs.Count - 2; i += 2)
            {
                triangleList.Add(customs[i]);
                triangleList.Add(customs[i + 2]);
                triangleList.Add(customs[i + 1]);

                triangleList.Add(customs[i + 1]);
                triangleList.Add(customs[i + 2]);
                triangleList.Add(customs[i + 3]);
            }
            return triangleList;
        }
    }
}
