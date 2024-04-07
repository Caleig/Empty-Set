using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Dusts
{
    /// <summary>
    /// 极川之核聚集粒子
    /// </summary>
    internal class FrozenCoreDust : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.noLight = false;
            dust.noGravity = true;
            base.OnSpawn(dust);
        }

        public override bool Update(Dust dust)
        {
            dust.scale -= 0.005f;
            dust.position += dust.velocity;
            Lighting.AddLight(dust.position, TorchID.Blue);
            if (dust.scale < 0.25f)
                dust.active = false;
            return false;
        }
        public override Color? GetAlpha(Dust dust, Color lightColor)
        {
            Texture2D texture = EmptySet.FrozenCoreDust;
            Vector2 drawOrigin = new Vector2(0, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone, EmptySet.FrozenCoreDustEffect);
            EmptySet.FrozenCoreDustEffect.CurrentTechnique.Passes["Edge"].Apply();
            Main.spriteBatch.Draw(texture, dust.position - Main.screenPosition, null, Color.White, dust.rotation, drawOrigin, dust.scale, SpriteEffects.None, 0);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            return Color.Transparent;
        }
    }
}
