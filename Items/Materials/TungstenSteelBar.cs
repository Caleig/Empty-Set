using EmptySet.Common.Systems;
using EmptySet.Tiles;
using EmptySet.Utils;
using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 钨钢锭
/// </summary>
public class TungstenSteelBar : ModItem
{
    public override void SetStaticDefaults()
    {


        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 30;
        Item.height = 24;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0,0,20,0);
    }

    public override void AddRecipes() => CreateRecipe(2)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.SliverOrTungsten))
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.CopperOrTin),2 )
        .AddIngredient<Flamepollen>()
        .AddTile(ModContent.TileType<SteelprocessingstationTile>())
        .Register();
}
