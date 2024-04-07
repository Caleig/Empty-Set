using rail;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 起点匣子
/// </summary>
public class StarterBox : ModItem
{
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 34;
        Item.height = 34;
        Item.rare = ItemRarityID.Gray;
        Item.value = 0;
    }
    public override bool CanRightClick() => true;

    public override void RightClick(Player player)
    {
        var source = player.GetSource_OpenItem(Type);
        player.QuickSpawnItem(source, ItemID.TungstenPickaxe);
        player.QuickSpawnItem(source, ItemID.TungstenAxe);
        player.QuickSpawnItem(source, ItemID.TungstenHammer);
        player.QuickSpawnItem(source, ItemID.TungstenWatch);
        player.QuickSpawnItem(source, ItemID.Aglet);
        player.QuickSpawnItem(source, ItemID.LifeCrystal);
        player.QuickSpawnItem(source, ItemID.IceTorch, 50);
        player.QuickSpawnItem(source, ItemID.SilverCoin, 10);
        player.QuickSpawnItem(source, ModContent.ItemType<Items.Accessories.TheAsylumoftheStarGod>());
    }
}