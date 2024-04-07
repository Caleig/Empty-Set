
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Dusts
{
    class LeavesPoisoningDust : ModDust
    {
		public override void OnSpawn(Dust dust)
		{
			dust.velocity *= 0.4f; // 将尘埃的起始速度乘以0.4，使其减速
			dust.noGravity = true; // 使尘埃没有重力。
			dust.noLight = false; // 使尘埃不发光。
			dust.scale *= 0.5f; // 将尘埃的初始规模乘以1.5倍。
		}

		public override bool Update(Dust dust)
		{ // 尘埃是活跃时每帧调用
			dust.position += dust.velocity;
			dust.rotation += dust.velocity.X * 0.15f;
			dust.scale *= 0.99f;

			float light = 0.35f * dust.scale;

			Lighting.AddLight(dust.position, light, light, light);

			if (dust.scale < 0.5f)
			{
				dust.active = false;
			}

			return false; // 返回false以防止默认行为。
		}
	}
}
