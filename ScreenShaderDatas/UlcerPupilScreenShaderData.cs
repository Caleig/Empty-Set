using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;


namespace EmptySet.ScreenShaderDatas
{
    public class UlcerPupilScreenShaderData : ScreenShaderData
    {
        public float multiplier = 1.25f;
        public UlcerPupilScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName)
        {
        }
        public override void Apply()
        {
            base.Shader.Parameters["multiplier"].SetValue(multiplier);
            base.Apply();
        }
    }
}
