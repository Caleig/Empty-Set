using Terraria;

namespace EmptySet.Extensions;
public static class PlayerExtensions
{
    /// <summary>
    /// 当前玩家生命值是否低于目标百分比
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="percent">目标百分比</param>
    /// <returns></returns>
    public static bool GetLifeStatus(this Player player, float percent) =>
        player.statLife < player.statLifeMax * percent;
    /// <summary>
    /// 当前玩家生命值是否低于目标百分比
    /// </summary>
    /// <param name="player">玩家</param>
    /// <param name="percent">目标百分比</param>
    /// <returns></returns>
    public static bool GetLifeStatus(this Player player, double percent)
        => player.statLife < player.statLifeMax * percent;
}
