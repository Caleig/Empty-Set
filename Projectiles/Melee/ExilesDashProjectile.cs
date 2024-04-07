using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Projectiles.Melee
{
    class ExilesDashProjectile:ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("放逐者冲刺");
		}
		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 72;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.alpha = 0;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = false;
			Projectile.extraUpdates = 1;
			Projectile.timeLeft = 100;
		}

        public override void AI()
        {
			if (Projectile.alpha < 255) Projectile.alpha += 5;
			Projectile.spriteDirection = (int)Projectile.ai[1];
			if (Projectile.ai[1] > 0)
			{
				Projectile.position = Main.player[(int)Projectile.ai[0]].position - new Vector2(5, 17);
			}
			else 
			{
				Projectile.position = Main.player[(int)Projectile.ai[0]].position - new Vector2(30, 17);
			}

			Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror, 0, 0, 150, default(Color), 0.4f);

		}

        public override bool PreDraw(ref Color lightColor)
		{
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// 用不受光线影响的颜色重新绘制投射体
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
	}
}
