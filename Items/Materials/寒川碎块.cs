using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 寒川碎块
/// </summary>
public class 寒川碎块 : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Eternity Ash Ore");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.LightRed;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 30, 0);
        Item.maxStack = 999;
    }
}