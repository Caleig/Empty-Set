using EmptySet.Common.Systems;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// ±¯¾çµõ×¹
/// </summary>
public class TragicPendant: ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 34;
        Item.value = Item.sellPrice(0, 3, 10, 0);
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
    }
    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {
        var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<Graypendant>());
        if (hasIt)
        {
            return false;
        }
        else
            return true;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Sapphire, 2)//À¶Óñ
        .AddIngredient(ItemID.Chain, 3)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.SliverOrTungsten))//9Òø/ÎÙ¶§ Tungsten
        .AddTile(TileID.Anvils)
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Throwing) += 0.1f;
    }
}
