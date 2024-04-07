using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Common;

/// <summary>
/// 设置无敌时间
/// </summary>
/// <remarks>
/// eg: player.GetModPlayer&lt;ImmuneTimeEffect&gt;().SetImmuneTime(15);
/// </remarks>
public class ImmuneTimeEffect : ModPlayer
{
    public bool IsEnabled { get; private set; } = false;

    private int ImmuneTime { get; set; } = 0;

    //public static int immuneTimeAdd = 0;
    //DefenderTalismanEffect.immuneTimeAdd = 23;
    //public void Enable() => IsEnabled = true;
    public void SetImmuneTime(int time)
    {
        if(!IsEnabled) IsEnabled = true;
        ImmuneTime = time;
    }
    public override void ResetEffects()
    {
        if (IsEnabled) IsEnabled = false;
    }
    public override void PostHurt(Player.HurtInfo info)
    {
        if (IsEnabled) 
            Player.immuneTime += ImmuneTime;
    }
}

