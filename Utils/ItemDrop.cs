using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace EmptySet.Utils;

public static class ItemDrop
{
    /// <summary>
    /// 创建物品掉落规则
    /// </summary>
    /// <param name="itemId">物品ID</param>
    /// <param name="chance">掉落概率</param>
    /// <param name="min">掉落物品的最小值</param>
    /// <param name="max">掉落物品的最大值，注意：不用加一</param>
    /// <param name="chanceDenominator">掉落概率分母</param>
    /// <returns></returns>
    public static IItemDropRule GetItemDropRule(int itemId, int chance, int min = 1, int max = 1, int chanceDenominator = 100)
        => new CommonDrop(itemId: itemId, chanceDenominator: chanceDenominator, amountDroppedMinimum: min,
            amountDroppedMaximum: max, chanceNumerator: chance);
    /// <summary>
    /// 创建物品掉落规则
    /// </summary>
    /// <typeparam name="Item">要掉落的物品类型</typeparam>
    /// <param name="chance">掉落概率</param>
    /// <param name="min">掉落物品的最小值</param>
    /// <param name="max">掉落物品的最大值，注意：不用加一</param>
    /// <param name="chanceDenominator">掉落概率分母</param>
    /// <returns></returns>
    public static IItemDropRule GetItemDropRule<Item>(int chance, int min = 1, int max = 1, int chanceDenominator = 100) where Item : ModItem
        => new CommonDrop(itemId: ModContent.ItemType<Item>(), chanceDenominator: chanceDenominator, amountDroppedMinimum: min,
            amountDroppedMaximum: max, chanceNumerator: chance);
}