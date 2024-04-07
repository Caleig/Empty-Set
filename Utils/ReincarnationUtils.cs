using Terraria;
using Terraria.GameContent.Creative;

namespace EmptySet.Utils;

public static class EmptySetUtils
{
    /// <summary>
    /// 在不同难度世界下控制弹幕伤害
    /// </summary>
    /// <param name="damage">实际要造成的伤害</param>
    /// <returns></returns>
    public static int ScaledProjDamage(int damage)
    {
        const float inherentHostileProjMultiplier = 2;
        float worldDamageMultiplier = Main.GameModeInfo.IsJourneyMode ? 
            CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs : 
            Main.GameModeInfo.EnemyDamageMultiplier;
        return (int)(damage / inherentHostileProjMultiplier / worldDamageMultiplier);
    }
    /// <summary>
    /// 在不同难度世界下控制接触伤害
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public static int ScaledNPCDamage(int damage)
    {
        float worldDamageMultiplier = Main.GameModeInfo.IsJourneyMode ?
            CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs :
            Main.GameModeInfo.EnemyDamageMultiplier;
        return (int)(damage / worldDamageMultiplier);
    }
    /// <summary>
    /// 在不同难度世界下控制npc最大生命值
    /// </summary>
    /// <param name="maxlife"></param>
    /// <returns></returns>
    public static int ScaledNPCMaxLife(int maxlife)
    {
        float worldNPCMaxLifeMultiplier = Main.GameModeInfo.IsJourneyMode ?
            CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>().StrengthMultiplierToGiveNPCs :
            Main.GameModeInfo.EnemyMaxLifeMultiplier;
        return (int)(maxlife / worldNPCMaxLifeMultiplier);
    }
}