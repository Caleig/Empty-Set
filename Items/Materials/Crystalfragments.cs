using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 碎晶
/// </summary>
public class Crystalfragments : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.White;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 0, 1);
        Item.maxStack = 999;
    }
}
