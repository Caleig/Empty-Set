using EmptySet.Common.Effects.Item;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 苦寒吊坠
/// </summary>
public class 苦寒吊坠 : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Ulcer Pupil");
        //Tooltip.SetDefault("Increases armor penetration by 10\n" +
        //                  "\"This thing is disgusting.\"");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 36;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<苦寒吊坠Effect>().Enable();
        player.statDefense += 10;
        player.endurance += 0.03f;
        player.GetDamage(DamageClass.Generic) += 0.1f;
    }
}