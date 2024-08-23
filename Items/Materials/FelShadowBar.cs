using EmptySet.Common.Systems;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 邪影锭
/// </summary>
public class FelShadowBar : ModItem
{
    public override void SetStaticDefaults()
    {

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.material = true;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.maxStack = 999;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SoulofNight, 2) //暗影之魂
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilBar),2)
        .AddIngredient<EternityAshBar>()
        .AddTile(TileID.MythrilAnvil)
        .Register();
}