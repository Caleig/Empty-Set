using EmptySet.Common.Abstract.Items;
using EmptySet.Items.Placeable;
using EmptySet.Items.Accessories;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.Utils;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 丛林游猎者宝藏匣
/// </summary>
public class JungleHunterChest : BossBag
{
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
    }
    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        /*
         * ItemDropRule.OneFromOptions(1, 
                ModContent.ItemType<Decline>(), 
                ModContent.ItemType<ForestNecklace>(), 
                ModContent.ItemType<DesertedTomahawk>(), 
                ModContent.ItemType<Items.Weapons.Magic.Wither>())
         */
        itemLoot.Add(ItemDropRule.OneFromOptions(1,
                ModContent.ItemType<ForestNecklace>(),
                ModContent.ItemType<DesertedTomahawk>()));

        itemLoot.Add(ItemDrop.GetItemDropRule(ModContent.ItemType<FangsNecklace>(), 100));
        itemLoot.Add(ItemDrop.GetItemDropRule(ItemID.GoldCoin, 100,2,4));
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<JungleHunterChestPlaced>(),1)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
