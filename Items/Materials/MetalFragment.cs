using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 破碎金属碎片
/// </summary>
public class MetalFragment : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 30;
        Item.height = 32;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 0, 50);
    }
}