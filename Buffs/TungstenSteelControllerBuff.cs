using EmptySet.Projectiles.Summon;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Buffs
{
    /// <summary>
    /// 钨钢无人机Buff
    /// </summary>
    internal class TungstenSteelControllerBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("钨钢无人机");
            // Description.SetDefault("文案里面没写");

            Main.buffNoSave[Type] = true; // 当你退出世界时，这个buff不会保存
            Main.buffNoTimeDisplay[Type] = true; // 剩余的时间不会显示在这个buff上
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // 如果召唤物存在，重置增益时间，否则移除玩家的增益
            if (player.ownedProjectileCounts[ModContent.ProjectileType<TungstenSteelControllerProj>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
