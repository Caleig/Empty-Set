using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Extensions;
using EmptySet.Models;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Ranged
{
    public class LightRangerStar : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Glorious Brokener");
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.timeLeft = 600;
            Projectile.height = 24;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.extraUpdates = 3;
            tex = ModContent.Request<Texture2D>("EmptySet/Projectiles/Ranged/LightRangerStar").Value;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 15; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, 10, 10, DustID.Torch, 0, 0, 0, new Color(251, 242, 54), 1.6f);
            }
        }
        private Vector2[] oldPosi = new Vector2[8];
        private Vector2[] oldVec = new Vector2[8];
        private int frametime = 0;
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            
            
            NPC target = null;
            Player player = Main.player[Projectile.owner];
            if (Main.time % 2 == 0)    //每两帧记录一次（打一次点）
            {
                for (int i = oldVec.Length - 1; i > 0; i--) //你应该知道为什么这里要写int i = oldVec.Length - 1
                {
                    oldPosi[i] = oldPosi[i - 1];
                }
                oldPosi[0] = Projectile.Center;
            }
            frametime++;
            float distanceMax = 200f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                            distanceMax = currentDistance;
                            target = npc;
                    }
                }
            }
            if(target != null)
            {
                var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 10f;
                Projectile.velocity = (targetVel + Projectile.velocity * 6) / 7f;
            }
            base.AI();
        }
        private Texture2D tex;
        public override bool PreDraw(ref Color lightColor)
        {
            var Len = 20;
            var list = new List<VertexInfo>();
            for (int i = 1; i < Len; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;
                var pos = Projectile.oldPos[i];
                pos = new Vector2(pos.X + Projectile.width / 2f, pos.Y + Projectile.height / 2f);
                /*+ Projectile.oldRot[i].ToRotationVector2() * 1f*/
                list.Add(new(
                    pos - Main.screenPosition + Projectile.oldRot[i].ToRotationVector2() * 4f * ((Len - 1 - (float)i) / (Len - 1)),
                    Color.Yellow * (float)Math.Sqrt(((Len - 1) - (float)i) / (Len - 1)),
                    new Vector3(i / (float)(Len - 1), 0, 1 - (i / (Len - 1)))
                ));
                list.Add(new(
                    pos - Main.screenPosition - Projectile.oldRot[i].ToRotationVector2() * 4f * ((Len - 1 - (float)i) / (Len - 1)),
                    Color.Yellow * (float)Math.Sqrt(((Len - 1) - (float)i) / (Len - 1)),
                    new Vector3(i / (float)(Len - 1), 1, 1 - (i / (Len - 1)))
                ));
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
            Main.graphics.GraphicsDevice.Textures[0] = TextureAssets.MagicPixel.Value;
            if (list.Count >= 3)
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            return true;
        }
        public override void PostDraw(Color lightColor)
        {
            for (int i = oldPosi.Length - 1; i > 0; i--)
            {
                if (oldPosi[i] != Vector2.Zero)
                {
                    Main.spriteBatch.Draw(tex, oldPosi[i] - Main.screenPosition,
                        null,
                        Color.White * 1 * (1 - .3f * i), 
                        (oldPosi[i - 1] - oldPosi[i]).ToRotation() + (float)(0.5 * MathHelper.Pi), tex.Size() * .5f,
                        1 * (1 - .02f * i), 
                        SpriteEffects.None, 
                        0);  //如果贴图不在你所期望的方向上，你应该知道你要做什么
                    //试试修改这里的.05f与.02f，想想它们意味着什么
                }
            }
        }
    }
}