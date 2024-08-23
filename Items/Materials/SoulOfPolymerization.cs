using Microsoft.Xna.Framework;
using Terraria;
using EmptySet.Tiles;
using EmptySet.Utils;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 聚合之魂
/// </summary>
public class SoulOfPolymerization : ModItem
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
        Item.width = 42;
        Item.height = 42;
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 2, 36, 0);
    }

    public override Color? GetAlpha(Color lightColor) => Color.Lerp(lightColor, Color.White, 0.8f);

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SoulofSight)
        .AddIngredient(ItemID.SoulofFright)
        .AddIngredient(ItemID.SoulofMight)
        .AddIngredient(ItemID.SoulofNight)
        .AddIngredient(ItemID.SoulofLight)
        .AddTile(ModContent.TileType<FusioninstrumentTile>())
        .Register();
}
