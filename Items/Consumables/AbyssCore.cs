using EmptySet.Biomes;
using EmptySet.Items.Materials;
using EmptySet.NPCs.Boss.腐化水晶;
using EmptySet.NPCs.Boss.血影屠戮者;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 暗渊晶核
/// </summary>
public class AbyssCore : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; 
    }

    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 38;
        Item.maxStack = 20;
        Item.value = Item.sellPrice(0,0,0,0);
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = false;
    }

    public override bool CanUseItem(Player player) => (player.ZoneCorrupt || player.ZoneCrimson || player.InModBiome<TheCoreOfChaosBiome>()) &&
                                                      player.ZoneOverworldHeight &&
                                                      !NPC.AnyNPCs(ModContent.NPCType<腐化水晶>()) &&
                                                      !NPC.AnyNPCs(ModContent.NPCType<血影屠戮者>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);
            int type = player.ZoneCorrupt? ModContent.NPCType<腐化水晶>() : player.ZoneCrimson ? ModContent.NPCType<血影屠戮者>() : -1;
            if (player.InModBiome<TheCoreOfChaosBiome>()) 
            {
                type = WorldGen.crimson ? ModContent.NPCType<腐化水晶>() : ModContent.NPCType<血影屠戮者>();
            }

            if (type == -1) return false;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnBoss((int) player.position.X, (int) player.position.Y - 25 * 16, type, player.whoAmI);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }
        return true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.CrystalShard, 15)
        .AddIngredient<FelShadowBar>(3)
        .AddIngredient(ItemID.SoulofNight)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
