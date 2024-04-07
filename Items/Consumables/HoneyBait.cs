using EmptySet.Extensions;
using EmptySet.NPCs.Boss.JungleHunter;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 蜜汁饵食
/// --多人联机 可能的位置问题?
/// </summary>
public class HoneyBait : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Honey Bait");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; 
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 58;
        Item.maxStack = 20;
        Item.value = Item.sellPrice(0, 0, 40, 0);
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = true;
    }

    public override bool CanUseItem(Player player) =>
        player.ZoneJungle && !NPC.AnyNPCs(ModContent.NPCType<JungleHunterHead>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            int type = ModContent.NPCType<JungleHunterHead>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnBoss((int)(player.position.X + 1200), (int)(player.position.Y + 1200), type, Main.myPlayer);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
        }
        return true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.JungleSpores, 3)
        .AddIngredient(ItemID.BottledHoney, 2)
        .AddIngredient(ItemID.Moonglow, 1)
        .AddTile(TileID.WorkBenches)
        .Register();
}
