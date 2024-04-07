using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Extensions;
using EmptySet.Models;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee
{
    public class GloriousBrokener : ModProjectile
    {
        private int Len = 35;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glorious Brokener");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = Len;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        
        public override void SetDefaults()
        {
            Projectile.width = 158;
            Projectile.height = 138;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 4 * EmptySet.Frame;
            Projectile.alpha = 0;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Player player = Main.player[Projectile.owner];
            if (Projectile.spriteDirection < 0) //左 -1 | 右 1
                Projectile.rotation -= 0.3f;
            else
                Projectile.rotation += 0.3f;
            float between = Vector2.Distance(Main.MouseWorld, player.Center);
            float time = between / 10f;
            Projectile.localAI[0]++;
            if(Projectile.localAI[0] > time)
            {
                Projectile.localAI[1]++;
                if(Projectile.localAI[1] > 120)
                {
                    Projectile.Kill();
                }
                Projectile.velocity.X = 0.001f * Projectile.direction;
                Projectile.velocity.Y = 0;
            }
            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
            for (int i = 0; i < 2; i++)
            {
                var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
                dust1.noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new Color(251, 242, 54);
            var list = new List<VertexInfo>();
            for (int i = 1; i < Len; i++)
            {
                if (Projectile.oldPos[i] == Vector2.Zero) continue;
                Vector2 oldCenter = Projectile.oldPos[i] + new Vector2(Projectile.width / 2f, Projectile.height / 2f);
                Vector2 pos = (Projectile.oldPos[i] + new Vector2(Projectile.spriteDirection == 1 ? 94 : 32, 14) - oldCenter).RotatedBy(Projectile.oldRot[i]) + oldCenter;

                var factor = i / (float)Projectile.oldPos.Length;
                var w = MathHelper.Lerp(1f, 0.05f, factor);

                list.Add(new(
                    pos - Main.screenPosition + new Vector2(0, 10),
                    color,
                    new Vector3(i / (float)(Len - 1), 0, 1 - (i / (Len - 1)))
                ));
                list.Add(new(
                    pos - Main.screenPosition + new Vector2(0, Projectile.height + 10 - i * Projectile.height / Len).RotatedBy(Projectile.oldRot[i] + MathHelper.PiOver4 * Projectile.spriteDirection),
                    color,
                    new Vector3(i / (float)(Len - 1), 1, 1 - (i / (Len - 1)))
                ));
            }
            
            Effect GeneralTrailing = EmptySet.Instance.Assets.Request<Effect>("Assets/Effects/GeneralTrailing", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
            GeneralTrailing.CurrentTechnique.Passes["Trailing"].Apply();
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, GeneralTrailing);

            Main.graphics.GraphicsDevice.Textures[0] = EmptySet.Instance.Assets.Request<Texture2D>("Assets/Textures/GeneralTrailingFlame", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;

            if (list.Count >= 3)
                Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, list.ToArray(), 0, list.Count - 2);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin();
            
            return true;
        }
    }
}