
using EmptySet.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Projectiles.Boss.JungleHunter
{
    class PoisonousGogProjectile:ModProjectile
    {
		public override string Texture => "Terraria/Images/Projectile_0";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("毒物");
		}
		public override void SetDefaults()
		{
			Projectile.width = 50;
			Projectile.height = 50;
			//Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.hostile = true; // Can the projectile deal damage to the player?
									   //Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
			Projectile.penetrate = 5; // 射弹能穿透多少怪物。(下面的OnTileCollide也会减少弹跳穿透)
			Projectile.timeLeft = 60 * 7; // 弹丸的有效时间(60 = 1秒，所以600 = 10秒)
			Projectile.alpha = 255; // 射弹的透明度，255为完全透明。(aiStyle 1快速淡入投射物)如果你没有使用淡入的aiStyle，请确保删除这个。你会奇怪为什么你的投射物是隐形的。
			//Projectile.light = 0.5f; // 发射体周围发射出多少光
			Projectile.ignoreWater = false; // 水会影响弹丸的速度吗?
			Projectile.tileCollide = false; // 弹丸能与瓦片碰撞吗?
			Projectile.extraUpdates = 1; // 如果你想让投射物在一帧内更新多次，设置为0以上
				
			//AIType = ProjectileID.Bullet; // 就像默认的Bullet一样
		}

        public override void AI()
        {
			Projectile.velocity = Projectile.velocity*0.95f;
			Dust dust = Main.dust[Dust.NewDust(Projectile.position, 80, 50, DustID.GreenTorch)];
			dust.noGravity = true;
			dust.fadeIn = 1.5f;
			dust.scale = 1.63f;
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(ModContent.BuffType<LeavesPoisoning>(), 9 * 60);
		}
	}
}
