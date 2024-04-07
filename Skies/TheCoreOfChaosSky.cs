using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace EmptySet.Skies
{
    internal class TheCoreOfChaosSky : CustomSky
    {
        private bool isActive;
        private float opacity;

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
            if (maxDepth >= 0 && minDepth < 0)
            {
                spriteBatch.Draw(ModContent.Request<Texture2D>("EmptySet/Skies/TheCoreOfChaosSky").Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Color(255, 255, 255, 10));
                spriteBatch.Draw(ModContent.Request<Texture2D>("EmptySet/Assets/Textures/GreenSun").Value, new Rectangle(Main.screenWidth / 2 - 500, -500, 1000, 1000), new Color(0, 255, 0, 0));
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
            return 0;
        }
        /// <summary>
        /// 方块边缘上的光
        /// </summary>
        /// <param name="inColor"></param>
        /// <returns></returns>
        public override Color OnTileColor(Color inColor)
        {
            inColor = new Color(255, 0, 0);
            return base.OnTileColor(inColor);
        }
    }
}
