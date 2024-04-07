using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class IssloosEffect : ModPlayer
{
    public int UseTimer { get; set; } = 0; //使用 一组的间隔 计时器
    public int SpaceTimer { get; set; } = 0; //使用 一组间的 计时器

    public override void PreUpdate()
    {
        if (SpaceTimer != 0) SpaceTimer--;
        if (UseTimer != 0) UseTimer--;
    }
}