using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Graphics;

namespace EmptySet
{
    public abstract class BTLcProj : ModProjectile
    {
        protected int State
        {
            get {return (int)Projectile.ai[0];}
            set {Projectile.ai[0] = value;}
        }
        protected int Timer
        {
            get { return (int)Projectile.ai[1];}
            set { Projectile.ai[1] = value; }
        }
        protected virtual void SwitchState(int state)
        {
            State = state;
        }

        protected static void vertex(int Len, Color color, Vector2 pos, string texpos, int m)
        {
            var list = new List<CustomVertexInfo>();
            for (int i = 0; i < Len; i++)
            {
                list.Add(new(
                    pos - Main.screenPosition + new Vector2(0, m),
                    color,
                    new Vector3(0, 0, 1)
                ));
                list.Add(new(
                    pos - Main.screenPosition + new Vector2(0, -m),
                    color,
                    new Vector3(0, 1, 1)
                ));
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null);
            Main.graphics.GraphicsDevice.Textures[0] = EmptySet.Instance.Assets.Request<Texture2D>(texpos, ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            if (list.Count >= 3)
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
        }
        public struct CustomVertexInfo : IVertexType
        {
            private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
            {
                new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
                new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
                new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
            });
            /// <summary>
            /// 绘制位置(世界坐标)
            /// </summary>
            public Vector2 Position;
            /// <summary>
            /// 绘制的颜色
            /// </summary>
            public Color Color;
            /// <summary>
            /// 前两个是纹理坐标，最后一个是自定义的
            /// </summary>
            public Vector3 TexCoord;

            public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
            {
                this.Position = position;
                this.Color = color;
                this.TexCoord = texCoord;
            }

            public VertexDeclaration VertexDeclaration => _vertexDeclaration;
        }
    }
}