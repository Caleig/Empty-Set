using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using EmptySet.Common.Systems;

namespace EmptySet.Projectiles.Melee
{
    internal class 测试武器弹幕1 : ModProjectile
    {
        private int Len = 70;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("测试武器弹幕1");
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = Len;
        }
        public override void SetDefaults()
        {
            Projectile.width = 110;
            Projectile.height = 110;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            
        }

        Player player;
        //弹幕一共要旋转多少度
        private float TotalRadians = MathHelper.ToRadians(250);
        private int time = 30;
        //这次弹幕的垂直缩放量
        private float VerticalScaling = 1f;
        //调整弹幕到玩家中心的距离
        private float Pro2PlrDis = 120;
        //玩家到鼠标的向量，基准向量
        private Vector2 Mouse2Player = Vector2.UnitX;
        //玩家到鼠标的向量鱼x轴的角度
        private float Mouse2PlayerRadi = 0;
        //弹幕在玩家的左侧还是右侧,1向右，-1向左
        private int Pdir = 1;
        //初始向量
        private Vector2 startVector;
        //每次更新角度后的初始向量
        private Vector2 UpdatedVector;

        private Vector2 v1 = Vector2.Zero;
        private Vector2 v2 = Vector2.Zero;
        private float Rot = 0;
        private Effect ef2;

        public override void AI()
        {
            Projectile.localAI[0]++;

            player = Main.player[Projectile.owner];
            if (Projectile.localAI[1] == 0) 
            {
                time = Projectile.timeLeft;
                Mouse2Player = Main.MouseWorld - player.Center;
                Mouse2PlayerRadi = (float)Math.Atan2(Mouse2Player.Y, Mouse2Player.X);
                Pdir = Math.Sign(Main.mouseX - player.Center.X + Main.screenPosition.X);
                if (Pdir == -1)
                {
                    startVector = Vector2.UnitX.RotatedBy(-(MathHelper.Pi - TotalRadians / 2)) * Pro2PlrDis;
                }
                else 
                {
                    startVector = Vector2.UnitX.RotatedBy(-TotalRadians / 2) * Pro2PlrDis;
                }
                VerticalScaling = Main.rand.NextFloat(Main.rand.NextFloat(0.3f, 1f), 1f);
                Projectile.localAI[1] = 1;
            }
            if (Pdir == -1)
            {
                Projectile.spriteDirection = -1;
                UpdatedVector = startVector.RotatedBy(-TotalRadians / time * Projectile.localAI[0]);
                v1 = new Vector2(UpdatedVector.X, UpdatedVector.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi+ MathHelper.Pi) - new Vector2(Projectile.width / 2, Projectile.height / 2);
                v2 = Projectile.Center - player.Center;
                Rot = (float)(Math.Atan2(v2.Y, v2.X)+MathHelper.Pi*3/4);
            }
            else 
            {
                UpdatedVector = startVector.RotatedBy(TotalRadians / time * Projectile.localAI[0]);
                v1 = new Vector2(UpdatedVector.X, UpdatedVector.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi) - new Vector2(Projectile.width/2, Projectile.height/2);
                v2 = Projectile.Center - player.Center;
                Rot = (float)(Math.Atan2(v2.Y, v2.X) + MathHelper.PiOver4);
            }
            Projectile.position = player.Center + v1;
            Projectile.rotation = Rot;
            Projectile.velocity = v2.RotatedBy(MathHelper.PiOver2) / v2.Length();
        }

        public override void PostDraw(Color lightColor)
        {
            List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
            ef2 = (Effect)ModContent.Request<Effect>("EmptySet/Assets/Effects/Trail").Value;
            Vector2 oldVector1;
            Vector2 oldVector2;
            Vector2 DrawUpdatedVector;
            Color color = new Color(50, 200, 20);
            int widti = 70;
            int oldPosLength = 0;
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) break;
                oldPosLength++;
            }
                // 把所有的点都生成出来，按照顺序
            for (int i = 1; i < Projectile.oldPos.Length; ++i)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) break;



                if (Pdir == -1) 
                {
                    oldVector1 = startVector.RotatedBy(-TotalRadians / time * (Projectile.localAI[0] - i)) ;

                    oldVector2 = startVector.RotatedBy(-TotalRadians / time * (Projectile.localAI[0] - i + 1)) ;
                    DrawUpdatedVector = new Vector2(oldVector1.X, oldVector1.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi + MathHelper.Pi);
                }
                else 
                {
                    oldVector1 = startVector.RotatedBy(TotalRadians / time * (Projectile.localAI[0] - i));

                    oldVector2 = startVector.RotatedBy(TotalRadians / time * (Projectile.localAI[0] - i + 1));
                    DrawUpdatedVector = new Vector2(oldVector1.X, oldVector1.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi);
                }


                var normalDir = oldVector2 - oldVector1;

                normalDir = Vector2.Normalize(new Vector2(-normalDir.Y, normalDir.X)).RotatedBy(Mouse2PlayerRadi);

                var factor = i / (float)oldPosLength;
                color = Color.Lerp(Color.White, Color.Red, factor);
                float h = 0;

                var w = MathHelper.Lerp(1f, 0.05f, factor + h);

                Player player = Main.player[Projectile.owner];

                bars.Add(new CustomVertexInfo(player.Center + DrawUpdatedVector + normalDir * widti, color, new Vector3((float)Math.Sqrt(factor + h), 1, w)));
                bars.Add(new CustomVertexInfo(player.Center + DrawUpdatedVector + normalDir * -widti, color, new Vector3((float)Math.Sqrt(factor + h), 0, w)));

                if (!Main.gamePaused && Main.rand.Next(15) == 1)
                {
                    int dust = Dust.NewDust(player.Center + DrawUpdatedVector + normalDir * -widti * Main.rand.NextFloat(Main.rand.NextFloat(-0.5f, 0.8f), 0.8f) * player.direction, 0, 0, DustID.Flare, 0f, 0f, 0, default(Color), Main.rand.NextFloat(1.5f, 2.5f));
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity = DrawUpdatedVector.RotatedBy(Math.PI / 2d * player.direction) / DrawUpdatedVector.Length();
                }
            }

            List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();

            if (bars.Count > 2)
            {
                if (Pdir == -1)
                {
                    var vertex1 = new CustomVertexInfo(Projectile.position.RotatedBy(Projectile.rotation, Projectile.Center), Color.White, new Vector3(0, 0, 1));
                    var vertex2 = new CustomVertexInfo((Projectile.position + new Vector2(Projectile.width, Projectile.height)).RotatedBy(Projectile.rotation, Projectile.Center), Color.White, new Vector3(0, 1, 1));

                    triangleList.Add(bars[0]);
                    triangleList.Add(vertex1);
                    triangleList.Add(bars[1]);

                    triangleList.Add(vertex1);
                    triangleList.Add(vertex2);
                    triangleList.Add(bars[0]);
                }
                else 
                {
                    var vertex1 = new CustomVertexInfo((Projectile.position + new Vector2(Projectile.width, 0)).RotatedBy(Projectile.rotation, Projectile.Center), Color.White, new Vector3(0, 0, 1));
                    var vertex2 = new CustomVertexInfo((Projectile.position + new Vector2(0, Projectile.height)).RotatedBy(Projectile.rotation, Projectile.Center), Color.White, new Vector3(0, 1, 1));

                    triangleList.Add(bars[0]);
                    triangleList.Add(vertex1);
                    triangleList.Add(bars[1]);

                    triangleList.Add(vertex1);
                    triangleList.Add(vertex2);
                    triangleList.Add(bars[0]);
                }

                // 按照顺序连接三角形
                for (int i = 0; i < bars.Count - 2; i += 2)
                {
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
                ef2.Parameters["uTransform"].SetValue(model * projection);
                ef2.Parameters["uTime"].SetValue(-(float)Main.time * 0.03f);
                Main.graphics.GraphicsDevice.Textures[0] = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/heatmapRedBeta").Value;
                Main.graphics.GraphicsDevice.Textures[1] = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/ComingGhostD0").Value;
                Main.graphics.GraphicsDevice.Textures[2] = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/ForgeWaveLight").Value;
                Main.graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
                Main.graphics.GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
                Main.graphics.GraphicsDevice.SamplerStates[2] = SamplerState.PointWrap;
                //Main.graphics.GraphicsDevice.Textures[0] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[1] = Main.magicPixel;
                //Main.graphics.GraphicsDevice.Textures[2] = Main.magicPixel;

                ef2.CurrentTechnique.Passes[0].Apply();


                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count/3);

                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                Main.spriteBatch.End();
                Main.spriteBatch.Begin();
            }
        }
    }
}
