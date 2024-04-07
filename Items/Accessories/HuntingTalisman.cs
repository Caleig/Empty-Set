using EmptySet.Common.Systems;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 猎手护符
/// </summary>
public class HuntingTalisman : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.value = Item.sellPrice(0, 0, 35, 0);
        Item.rare = ItemRarityID.Blue;
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
        .AddIngredient(ItemID.Sapphire,2)//蓝玉
        .AddIngredient(ItemID.Chain, 3)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.SliverOrTungsten))//9银/钨锭 Tungsten
        .AddTile(TileID.Anvils)
        .Register();
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Throwing) += 0.1f;
    }
}