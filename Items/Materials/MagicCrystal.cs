using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace EmptySet.Items.Materials
{
    public class MagicCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Magic Crystal");
            // Tooltip.SetDefault("闪烁着微光");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.width = 40;
            Item.height = 40;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.value = Item.buyPrice(0, 0, 15, 0);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>(), 4)
                .Register();
        }
    }
}