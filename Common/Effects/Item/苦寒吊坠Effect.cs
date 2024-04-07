using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class 苦寒吊坠Effect : ModPlayer
{
    public bool IsEnabled { get; private set; } = false;
    public void Enable() => IsEnabled = true;
    private int duration = 3 * EmptySet.Frame;
    public override void OnHurt(Player.HurtInfo info)
    {
        if (IsEnabled)
            Player.AddBuff(BuffID.Frostburn, duration);
    }

    public override void ResetEffects()
    {
        if (IsEnabled) IsEnabled = false;
    }
}