using Microsoft.Xna.Framework;
using EmptySet.Items.Weapons.Magic;
using System;
using EmptySet.Common.Effects.Item;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Held
{
    internal class LoudLavaFlowHeldProj : ModProjectile
    {
        public override string Texture => "Terraria/Images/Projectile_0";
		Vector2 vector = Vector2.Zero;
		public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Loud Lava Flow");
        }
        public override void SetDefaults()
		{
			Projectile.width = 0;
			Projectile.height = 0;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 2;
			Projectile.alpha = 0;
			Projectile.light = 0;
			Projectile.ignoreWater = false;
			Projectile.tileCollide = false;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
		}

		public override void AI()
		{
            int count = 15;
            int time = 90;

			Player player = Main.player[Projectile.owner];

			if (player.HeldItem.type == ModContent.ItemType<LoudLavaFlow>() && Main.mouseRight)
			{
				Projectile.localAI[0]++;
				Projectile.timeLeft = 2;

				// 从玩家到达鼠标位置的单位向量
				Vector2 unit = Vector2.Normalize(Main.MouseWorld - player.Center);
				// 随机角度
				float rotaion = unit.ToRotation();
				// 调整玩家转向以及手持物品的转动方向
				player.direction = Main.MouseWorld.X < player.Center.X ? -1 : 1;
				player.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * player.direction,
					rotaion.ToRotationVector2().X * player.direction);
				// 玩家保持物品使用动画
				player.itemTime = 2;
				player.itemAnimation = 2;
                player.GetModPlayer<LoudLavaFlowEffect>().IsDouble = true;
			}
			
			if (Projectile.localAI[0] == 180) SoundEngine.PlaySound(SoundID.Item4, player.position);

			if (Projectile.localAI[0] >= 180)
			{
                player.GetModPlayer<LoudLavaFlowEffect>().IsDouble = false;
				player.itemTime = time;
				Projectile.timeLeft = time;
				if (player.HeldItem.type == ModContent.ItemType<LoudLavaFlow>() && (!Main.mouseRight || Projectile.localAI[1] > 0))
				{
					Projectile.ai[0]++;
					if (Projectile.ai[0] >= Projectile.localAI[1] * time / count)
					{
						if (Projectile.localAI[1] == 0) vector = Vector2.Normalize(Main.MouseWorld - player.Center) * 7;
						Projectile.localAI[1]++;
						float radians = (vector.X >= 0 ? 1 : -1) * MathHelper.ToRadians(-67.5f + Projectile.localAI[1] * 135 / count);
						Projectile.NewProjectile(Projectile.GetSource_FromAI(), player.Center, vector.RotatedBy(radians), ProjectileID.Flames, Projectile.damage, Projectile.knockBack, Projectile.owner);
					}
				}
			}

			if (Projectile.localAI[1] >= count) Projectile.Kill();
		}
		public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
	}
}
