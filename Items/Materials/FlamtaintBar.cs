using Terraria.GameContent.Creative;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 堕烬锭
/// </summary>
public class FlamtaintBar : ModItem
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
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0,2,0,0);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.ChlorophyteBar)
        .AddIngredient<BloodWreckage>(2)
        .AddIngredient<MoltenDebris>(3)
        .AddIngredient<DarkBrokenCrystal>(4)
        .AddTile(TileID.Autohammer)
        .Register();

}