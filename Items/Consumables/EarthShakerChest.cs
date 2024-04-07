using EmptySet.Common.Abstract.Items;
using EmptySet.Items.Accessories;
using EmptySet.Items.Materials;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.Items.Weapons.Throwing;
using EmptySet.NPCs.Boss.EarthShaker;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 大地震撼者宝藏匣
/// </summary>
public class EarthShakerChest : BossBag
{
    public override void SetStaticDefaults()
    {
    }
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<MetalFragment>(), 100,30,40));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<ChargedCrystal>(), 100,20,30));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<ShakingEarthAmulet>(), 100));
        itemLoot.Add(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 2, 3));
    }
}