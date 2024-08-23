using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Materials;

/// <summary>
/// 血影
/// </summary>
public class BloodShadow : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Blood Shadow");

        //Tooltip.SetDefault("The quintessence of ray clouds");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "雷云之精华");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;

        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
        ItemID.Sets.AnimatesAsSoul[Item.type] = true;
        ItemID.Sets.ItemIconPulse[Item.type] = true; 
        ItemID.Sets.ItemNoGravity[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.width = 40;
        Item.height = 48;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 70, 0, 0);
    }

    public override Color? GetAlpha(Color lightColor) => Color.Lerp(lightColor, Color.White, 0.8f);
}