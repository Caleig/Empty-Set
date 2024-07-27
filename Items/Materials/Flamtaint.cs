using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 堕烬
/// </summary>
public class Flamtaint : ModItem
{
    public override void SetStaticDefaults()
    {


        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;

        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        ItemID.Sets.ItemIconPulse[Item.type] = true; 
        ItemID.Sets.ItemNoGravity[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 30;
        Item.height = 30;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 1, 5, 0);
    }

    public override Color? GetAlpha(Color lightColor) => Color.Lerp(lightColor, Color.White, 0.8f);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<EternityAshBar>(1)
        .AddIngredient<BloodWreckage>(1)
        .AddIngredient<DarkBrokenCrystal>(2)
        .AddIngredient<MoltenDebris>(2)
        .AddTile(TileID.Autohammer)
        .Register();
}
