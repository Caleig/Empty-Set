using EmptySet.Common.Abstract.Items;
using EmptySet.Items.Accessories;
using EmptySet.Items.Materials;
using EmptySet.Items.Weapons.Throwing;
using EmptySet.Utils;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 极川之核宝藏匣
/// </summary>
public class FrozenCoreChest : BossBag
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
        // DisplayName.SetDefault("Treasure Bag (Frozen Core)");
    }
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<寒川碎块>(), 100,70,110));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<冰川之破>(), 9));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<苦寒吊坠>(), 50));
        itemLoot.Add(ItemDrop.GetItemDropRule<ChillCrystal>(50));
        itemLoot.Add(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100,23,31));
    }
}