using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.TEST;

/// <summary>
/// 注意事项
/// </summary>
public class Precautions : ModItem
{
    public override void SetStaticDefaults()
    {


        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 1;
        Item.width = 30;
        Item.height = 32;
        Item.value = Item.sellPrice(0,0,23,6);
    }
}
