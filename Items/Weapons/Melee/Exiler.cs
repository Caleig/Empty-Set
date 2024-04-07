using EmptySet.Common.Effects.Item;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;
/// <summary>
/// 流放者
/// </summary>
public class Exiler : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 60;
        Item.height = 60;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 39;
        Item.knockBack = KnockBackLevel.Normal;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 3, 60, 0);
        Item.rare = ItemRarityID.Orange;
        Item.UseSound = SoundID.Item1;
    }

    public override bool AltFunctionUse(Player player)
    {
        player.GetModPlayer<ExileEffect>().dashAccessoryEquipped = true;
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.BoneSword)
        .AddIngredient(ModContent.ItemType<PolarIceSword>())
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.GoldBroadsword))
        .AddTile(TileID.DemonAltar)
        .Register();
}
