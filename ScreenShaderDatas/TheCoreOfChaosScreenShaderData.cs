using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace EmptySet.ScreenShaderDatas
{
    public class TheCoreOfChaosScreenShaderData : ScreenShaderData
    {
        public float mode = 0;

        public TheCoreOfChaosScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName) { }

        public override void Apply()
        {
            base.Shader.Parameters["mode"].SetValue(mode);
            base.Apply();
        }
    }
}
