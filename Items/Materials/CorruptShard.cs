using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 深谙碎晶
/// </summary>
public class CorruptShard : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Corrupt Shard");

        //Tooltip.SetDefault("\"Broken crystals composed of blood\"");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "\"由血组成的碎晶体\"");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 28;
        Item.height = 28;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0,0,70,0);
    }
}