using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace EmptySet.Items.Materials
{
    public class ImpurityMagicCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Impurity Magic Crystal");
            // Tooltip.SetDefault("从一种未知生命体身上取下的水晶");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.value = Item.buyPrice(0, 0, 3, 0);
        }
    }
}