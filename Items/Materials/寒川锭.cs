using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 寒川锭
/// </summary>
public class 寒川锭 : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Eternity Ash Bar");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.material = true;
        Item.value = Item.sellPrice(0, 0, 55, 0);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<寒川碎块>(), 3)
        //.AddTile(TileID.Furnaces) //熔炉
        .Register();
}