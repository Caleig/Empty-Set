using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class DersilyaEffect : ModPlayer
{
    public int UseTimer { get; set; } = 0; //使用 一组的间隔 计时器
    public override void PreUpdate()
    {
        if (UseTimer != 0) UseTimer--;
    }
}