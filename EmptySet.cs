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
        public Effect theCoreOfChaosEffect;//�������Ч��
        public Effect ulcerPupilEffect;//����ͫ��Effect
        public static Effect FrozenCoreDustEffect;//����֮������
        public Effect offsetEff, BigTentacle, Bloom;
        public Effect Lens;//͸��Ч��

        //ScreenShaderData
        public TheCoreOfChaosScreenShaderData theCoreOfChaosScreenShaderData;
        public UlcerPupilScreenShaderData ulcerPupilScreenShaderData;

        //ͼƬ�ز�
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

            //ͼƬ�ز�
            FrozenCoreDust = Assets.Request<Texture2D>("Dusts/FrozenCoreDust").Value;
            close = Assets.Request<Texture2D>("Assets/Textures/close");

            //��չ�����
            SkyManager.Instance["TheCoreOfChaosSky"] = new TheCoreOfChaosSky();
            SkyManager.Instance["TheCoreOfChaosSky"].Load();
            SkyManager.Instance["FrozenCoreSky"] = new FrozenCoreSky();
            SkyManager.Instance["FrozenCoreSky"].Load();

            //Filters
            Filters.Scene["EmptySet:TheCoreOfChaosScreen"] = new Filter(theCoreOfChaosScreenShaderData, EffectPriority.Medium);// ���������Ļ
            Filters.Scene["EmptySet:TheCoreOfChaosScreen"].Load();
            Filters.Scene["EmptySet:UlcerPupilScreen"] = new Filter(ulcerPupilScreenShaderData, EffectPriority.Medium);// ����ͫ��
            Filters.Scene["EmptySet:UlcerPupilScreen"].Load();

            Terraria.Graphics.Effects.On_FilterManager.EndCapture += Test;//ԭ����Ƴ�������󲿷֡����˾�������������render��֤������ԭ���ͻ
            Terraria.Graphics.Effects.On_FilterManager.EndCapture += LensRender;
            Main.OnResolutionChanged += Main_OnResolutionChanged;
            base.Load();
        }

        public override void Unload()
        {
            Terraria.Graphics.Effects.On_FilterManager.EndCapture -= Test;//ж��ʱ˳���Ƴ�
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

            /*gd.SetRenderTarget(render);//���ó��Լ���RenderTarget���л���
            gd.Clear(Color.Transparent);//��͸�����
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            //����
            sb.Draw(ModContent.Request<Texture2D>("EmptySet/Assets/Textures/RedPoint").Value, new Vector2(800, 500), new Rectangle(0, 0, 50, 50), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            sb.End();*/

            gd.SetRenderTarget(Main.screenTarget);//������ĻrenderTarget
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            sb.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);//�����Լ���rendertarget
            //sb.Draw(render, Vector2.Zero, Color.White);//�����Լ���rendertarget
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
            Bloom.Parameters["m"].SetValue(0.75f);//ȡ���ȳ���mֵ�Ĳ���
            Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            Main.spriteBatch.End();

            //����
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Bloom.Parameters["uScreenResolution"].SetValue(new Vector2(Main.screenWidth, Main.screenHeight));
            Bloom.Parameters["uRange"].SetValue(2.5f);
            Bloom.Parameters["uIntensity"].SetValue(0.94f);
            for (int i = 0; i < 3; i++)//����ʹ������RenderTarget2D�����ж��ģ��
            {
                Bloom.CurrentTechnique.Passes["GlurV"].Apply();//����
                graphicsDevice.SetRenderTarget(Main.screenTarget);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);

                Bloom.CurrentTechnique.Passes["GlurH"].Apply();//����
                graphicsDevice.SetRenderTarget(render);
                graphicsDevice.Clear(Color.Transparent);
                Main.spriteBatch.Draw(Main.screenTarget, Vector2.Zero, Color.White);
            }
            Main.spriteBatch.End();

            //���ӵ�ԭͼ��
            graphicsDevice.SetRenderTarget(Main.screenTarget);
            graphicsDevice.Clear(Color.Transparent);
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);//Additive��ģ����Ĳ��ּӵ�Main.screenTarget��
            Main.spriteBatch.Draw(Main.screenTargetSwap, Vector2.Zero, Color.White);
            Main.spriteBatch.Draw(render, Vector2.Zero, Color.White);
            Main.spriteBatch.End();
        }//�ⷢ��

        //͸��
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
                    Vector2 screenResolution = new Vector2(Main.screenWidth, Main.screenHeight);//�ֱ���
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
        //�ڷֱ��ʸ���ʱ���ؽ�render��ֹĳЩbug
        private void Main_OnResolutionChanged(Vector2 obj)
        {
            render = new RenderTarget2D(Main.graphics.GraphicsDevice, Main.screenWidth, Main.screenHeight);
        }


        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
            // ��ȡID
            EmptySetMessageType msgType = (EmptySetMessageType)reader.ReadInt32();
            switch (msgType)
            {
                case EmptySetMessageType.SpawnBossOnPoint:
                    // ��ȡ����
                    int x = reader.ReadInt32();
                    int y = reader.ReadInt32();
                    int NPCtype = reader.ReadInt32();
                    int targetPlayer = reader.ReadInt32();
                    // ����ڷ��������յ��ģ����ǾͰ�x^2����ȥ
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NPC.SpawnBoss(x, y, NPCtype, targetPlayer);
                        Console.WriteLine("��������");
                        //ModPacket packet = GetPacket();
                        //packet.Write((int)EmptySetMessageType.SpawnBossOnPoint);
                        //packet.Write(x * x);

                        // ���ظ�������
                        //packet.Send(whoAmI, -1);
                        // ���Ƿ���������
                        // packet.Send(-1, -1);
                        // ���Ƿ��������ˣ����˷�����
                        // packet.Send(-1, whoAmI);
                    }
                    else
                    {
                        // �������ǾͰ����ֵ��ӡ����
                        //Main.NewText(x);
                    }
                    break;
                case EmptySetMessageType.PlacePortal:
                    int id = EmptySetWorld.PortalList.Count;
                    int PortalTileX = reader.ReadInt32();
                    int PortalTileY = reader.ReadInt32();
                    string portalName = "������" + id;
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