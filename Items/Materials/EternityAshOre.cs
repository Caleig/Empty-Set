using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 恒灰矿
/// </summary>
public class EternityAshOre : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Eternity Ash Ore");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 15, 0);
        Item.maxStack = 999;
    }
}