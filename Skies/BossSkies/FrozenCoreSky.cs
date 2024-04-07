using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.NPCs.Boss.FrozenCore;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace EmptySet.Skies.BossSkies
{
    internal class FrozenCoreSky : CustomSky
    {
        private bool isActive;
        private float opacity;
        int timer, timer2 = 0;
        Texture2D CommonSkyBackground;
        Texture2D Aurora;
        Texture2D Star;
        Color _inColor;
        Color auroraColor;

        public override void Activate(Vector2 position, params object[] args)
        {
            isActive = true;
        }

        public override void Deactivate(params object[] args)
        {
            isActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            timer++;
            timer2 = (int)Math.Floor(timer / 30f);
            CommonSkyBackground = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/CommonSkyBackground").Value;
            Aurora = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/Aurora").Value;
            Star = ModContent.Request<Texture2D>("EmptySet/Assets/Textures/Star").Value;
            auroraColor = Main.LocalPlayer.ZoneSnow ? new Color(150, 150, 150, 0) : new Color(50, 50, 50, 0);
            if (maxDepth >= float.MaxValue && minDepth < float.MaxValue)
            {
                spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * opacity);
                spriteBatch.Draw(CommonSkyBackground, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(0, 100, 255, 255) * opacity);
                spriteBatch.Draw(Star, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(255, 255, 255, 0) * opacity);
                for (int i = 0; i < Math.Ceiling((float)Main.screenWidth / (float)Aurora.Width) + 1; i++) 
                {
                    spriteBatch.Draw(Aurora, new Rectangle(Aurora.Width * i - timer2, 0, Aurora.Width, Main.screenHeight), null, auroraColor * opacity, 0, new Vector2(0, 0), SpriteEffects.None, 0);
                }
                if (timer2 >= Aurora.Width) timer = 0;
            }

        }

        public override bool IsActive()
        {
            return isActive || opacity > 0f;
        }

        public override void Reset()
        {
            isActive = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<FrozenCore>())) isActive = false;

            if (isActive && opacity < 1f)
            {
                opacity += 0.02f;
                return;
            }
            if (!isActive && opacity > 0f)
            {
                opacity -= 0.02f;
            }
        }

        /// <summary>
        /// 云透明度
        /// </summary>
        /// <returns></returns>
        public override float GetCloudAlpha()
        {
            return 1f - opacity;
        }

        public override Color OnTileColor(Color inColor)
        {
            _inColor = new Color(255, 255, 255);
            return new Color(Vector4.Lerp(inColor.ToVector4(), _inColor.ToVector4(), opacity * 0.5f));
        }
    }
}
