using EmptySet.Common.Systems;
using EmptySet.Projectiles.Boss.FrozenCore;
using EmptySet.ScreenShaderDatas;
using EmptySet.Skies.BossSkies;
using EmptySet.Skies;
using log4net;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EmptySet
{
	public class EmptySet : Mod
	{
        static ILog log;
        static bool debug = true;
        public const int Frame = 60;
        internal static EmptySet Instance;

        public static bool bloomActive = false;

        //render
        public static RenderTarget2D render;
        public static RenderTarget2D lensRender;

        //Effect
        public Effect theCoreOfChaosEffect;//混沌核心效果
        public Effect ulcerPupilEffect;//溃烂瞳孔Effect
        public static Effect FrozenCoreDustEffect;//极川之核粒子
        public Effect offsetEff, BigTentacle, Bloom;
        public Effect Lens;//透镜效果

        //ScreenShaderData
        public TheCoreOfChaosScreenShaderData theCoreOfChaosScreenShaderData;
        public UlcerPupilScreenShaderData ulcerPupilScreenShaderData;

        //图片素材
        public static Texture2D FrozenCoreDust;
        public static ReLogic.Content.Asset<Texture2D> close;


        public override void Load()
        {
            Instance = this;

            //Effect
            Bloom = Assets.Request<Effect>("Assets/Effects/Bloom1", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            theCoreOfChaosEffect = Assets.Request<Effect>("Assets/Effects/TheCoreOfChaosScreen", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            ulcerPupilEffect = Assets.Request<Effect>("Assets/Effects/UlcerPupilScreen", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            FrozenCoreDustEffect = Assets.Request<Effect>("Assets/Effects/FrozenCoreDust", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            Lens = Assets.Request<Effect>("Assets/Effects/Lens", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            //ScreenShaderData
            theCoreOfChaosScreenShaderData = new TheCoreOfChaosScreenShaderData(new Ref<Effect>(theCoreOfChaosEffect), "TheCoreOfChaosScreen");
            ulcerPupilScreenShaderData = new UlcerPupilScreenShaderData(new Ref<Effect>(ulcerPupilEffect), "UlcerPupilScreen");

            //图片素材
            FrozenCoreDust = Assets.Request<Texture2D>("Dusts/FrozenCoreDust").Value;
            close = Assets.Request<Texture2D>("Assets/Textures/close");

            //天空管理器
            SkyManager.Instance["TheCoreOfChaosSky"] = new TheCoreOfChaosSky();
            SkyManager.Instance["TheCoreOfChaosSky"].Load();
            SkyManager.Instance["FrozenCoreSky"] = new FrozenCoreSky();
            SkyManager.Instance["FrozenCoreSky"].Load();

            //Filters
            Filters.Scene["EmptySet:TheCoreOfChaosScreen"] = new Filter(theCoreOfChaosScreenShaderData, EffectPriority.Medium);// 混沌核心屏幕
            Filters.Scene["EmptySet:TheCoreOfChaosScreen"].Load();
            Filters.Scene["EmptySet:UlcerPupilScreen"] = new Filter(ulcerPupilScreenShaderData, EffectPriority.Medium);// 溃烂瞳孔
            Filters.Scene["EmptySet:UlcerPupilScreen"].Load();

            Terraria.Graphics.Effects.On_FilterManager.EndCapture += Test;//原版绘制场景的最后部分——滤镜。在这里运用render保证不会与原版冲突
            Terraria.Graphics.Effects.On_FilterManager.EndCapture += LensRender;
            Main.OnResolutionChanged += Main_OnResolutionChanged;
            base.Load();
        }

        public override void Unload()
        {
            Terraria.Graphics.Effects.On_FilterManager.EndCapture -= Test;//卸载时顺利移除
            Terraria.Graphics.Effects.On_FilterManager.EndCapture -= LensRender;
            Main.OnResolutionChanged -= Main_OnResolutionChanged;
            base.Unload();
        }

        private void Test(Terraria.Graphics.Effects.On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            GraphicsDevice gd = Main.instance.GraphicsDevice;
            SpriteBatch sb = Main.spriteBatch;

            if (render == null) render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
            if (gd == null) return;

            gd.SetRenderTarget(Main.screenTargetSwap);
            //gd.Clear(Color.Transparent);
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            sb.End();

            /*gd.SetRenderTarget(render);//设置成自己的RenderTarget进行绘制
            gd.Clear(Color.Transparent);//用透明清除
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //绘制
            sb.Draw(ModContent.Request<Texture2D>("EmptySet/Assets/Textures/RedPoint").Value, new Vector2(800, 500), new Rectangle(0, 0, 50, 50), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            sb.End();*/

            gd.SetRenderTarget(Main.screenTarget);//换回屏幕renderTarget
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);//绘制自己的rendertarget
            //sb.Draw(render, Vector2.Zero, Color.White);//绘制自己的rendertarget
            sb.End();

            if (bloomActive)
                UseBloom(gd);
            orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
        private void UseBloom(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(Main.screenTargetSwap);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            graphicsDevice.SetRenderTarget(render);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.CurrentTechnique.Passes[0].Apply();
            Bloom.Parameters["m"].SetValue(0.75f);//取亮度超过m值的部分
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //处理
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.Parameters["uScreenResolution"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
            Bloom.Parameters["uRange"].SetValue(2.5f);
            Bloom.Parameters["uIntensity"].SetValue(0.94f);
            for (int i = 0; i < 3; i++)//交替使用两个RenderTarget2D，进行多次模糊
            {
                Bloom.CurrentTechnique.Passes["GlurV"].Apply();//横向
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);

                Bloom.CurrentTechnique.Passes["GlurH"].Apply();//纵向
                graphicsDevice.SetRenderTarget(render);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            }
            Main.spriteBatch.End();

            //叠加到原图上
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);//Additive把模糊后的部分加到Main.screenTarget里
            Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);
            Main.spriteBatch.End();
        }//外发光

        //透镜
        private void LensRender(Terraria.Graphics.Effects.On_FilterManager.orig_EndCapture orig, FilterManager self, RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Color clearColor)
        {
            lensRender = Main.screenTargetSwap;
            foreach (Projectile proj in Main.projectile)
            {
                if (proj.active && proj.type == ModContent.ProjectileType<ExplodeProjectile>())
                {
                    Main.instance.GraphicsDevice.SetRenderTarget(lensRender);
                    Main.instance.GraphicsDevice.Clear(Color.Transparent);
                    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                    Lens.CurrentTechnique.Passes[0].Apply();
                    Vector2 screenResolution = new Vector2(Main.screenWidth, Main.screenHeight);//分辨率
                    Lens.Parameters["uScreenResolution"].SetValue(screenResolution);
                    Lens.Parameters["pos"].SetValue((proj.Center - Main.screenPosition) / screenResolution);
                    Lens.Parameters["intensity"].SetValue(10);
                    Lens.Parameters["range"].SetValue(0.1f);
                    Lens.Parameters["radius"].SetValue(proj.ai[0] * 15);
                    Main.spriteBatch.Draw(lensRender == Main.screenTarget ? Main.screenTargetSwap : Main.screenTarget, Vector2.Zero, Color.White);
                    Main.spriteBatch.End();

                    lensRender = lensRender == Main.screenTarget ? Main.screenTargetSwap : Main.screenTarget;
                }
            }
            if (lensRender == Main.screenTarget)
            {
                Main.instance.GraphicsDevice.SetRenderTarget(lensRender);
                Main.instance.GraphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
                Main.spriteBatch.End();
            }

            orig(self, finalTexture, screenTarget1, screenTarget2, clearColor);
        }
        //在分辨率更改时，重建render防止某些bug
        private void Main_OnResolutionChanged(Vector2 obj)
        {
            render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
            // 读取ID
            EmptySetMessageType msgType = (EmptySetMessageType)reader.ReadInt32();
            switch (msgType)
            {
                case EmptySetMessageType.SpawnBossOnPoint:
                    // 读取参数
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    int NPCtype = reader.ReadInt32();
                    int targetPlayer = reader.ReadInt32();
                    // 如果在服务器上收到的，我们就把x^2发回去
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NPC.SpawnBoss(x, y, NPCtype, targetPlayer);
                        Console.WriteLine("服务器端");
                        //ModPacket packet = GetPacket();
                        //packet.Write((int)EmptySetMessageType.SpawnBossOnPoint);
                        //packet.Write(x * x);

                        // 发回给发送者
                        //packet.Send(whoAmI, -1);
                        // 这是发给所有人
                        // packet.Send(-1, -1);
                        // 这是发给所有人，除了发送者
                        // packet.Send(-1, whoAmI);
                    }
                    else
                    {
                        // 否则我们就把这个值打印出来
                        //Main.NewText(x);
                    }
                    break;
                case EmptySetMessageType.PlacePortal:
                    int id = EmptySetWorld.PortalList.Count;
                    int PortalTileX = reader.ReadInt32();
                    int PortalTileY = reader.ReadInt32();
                    string portalName = "传送门" + id;
                    EmptySetWorld.PortalList.Add(new PortalTag(id, PortalTileX, PortalTileY, portalName));
                    break;
                case EmptySetMessageType.KillPortal:
                    int KillPortalIndex = reader.ReadInt32();
                    EmptySetWorld.PortalList.RemoveAt(KillPortalIndex);
                    break;
            }
        }
        internal static void DebugText(object message, string classname = "EmptySet", int type = 0)
        {
            if (debug)
            {
                if (Main.gameMenu)
                {
                    log = LogManager.GetLogger(classname);
                    log.Debug(message);
                }
                else
                {
                    if (type == 1)
                    {
                        log = LogManager.GetLogger(classname);
                        log.Debug(message);
                    }
                    Main.NewText(message);
                }
            }
        }
    }
}