using EmptySet.Common.Abstract.Items;
using EmptySet.Items.Placeable;
using EmptySet.Items.Accessories;
using EmptySet.Items.Materials;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.Items.Weapons.Throwing;
using EmptySet.Utils;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 熔岩游猎者宝藏匣
/// </summary>
public class LavaHunterChest : BossBag
{
    public override void SetStaticDefaults()
    {
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<FlameFang>(), 33));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<LavaCutter>(), 33));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<MiniFireballGun>(), 33));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<HellFlameEjector>(), 33));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<LoudLavaFlow>(), 10));
        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<MoltenDebris>(), 100,7,11));
        itemLoot.Add(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100, 6, 10));
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<LavaHunterChestPlaced>(),1)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
