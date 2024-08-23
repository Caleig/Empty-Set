using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;

namespace EmptySet.Items.Materials;
/// <summary>
/// 熔火碎块
/// </summary>
public class MoltenDebris : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        ItemID.Sets.IsAMaterial[Item.type]= true;
    }
    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 34;
        Item.height = 34;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 0, 0, 90);
    }
}