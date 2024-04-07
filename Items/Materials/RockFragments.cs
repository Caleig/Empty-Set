using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 岩能碎块
/// </summary>
public class RockFragments : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Rock Fragments");

        //Tooltip.SetDefault("\"Broken crystals composed of blood\"");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "\"\"");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 30;
        Item.height = 28;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0,0,30,0);
    }
}