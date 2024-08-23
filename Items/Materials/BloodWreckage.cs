using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 血骸
/// </summary>
public class BloodWreckage : ModItem
{
    public override void SetStaticDefaults()
    {

        //Tooltip.SetDefault("\"Broken crystals composed of blood\"");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "\"由血组成的碎晶体\"");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 20;
        Item.height = 38;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0,1,0,0);
    }
}