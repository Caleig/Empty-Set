using EmptySet.NPCs.MiniBoss.ThunderstormEye;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 风暴信标
/// --多人联机 X
/// </summary>
public class StormBeacon : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 18;
        Item.maxStack = 20;
        Item.value = Item.sellPrice(0, 0, 26, 0);
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useStyle = ItemUseStyleID.HoldUp;
        //Item.consumable = true;
    }

    public override bool CanUseItem(Player player) =>
        player.ZoneOverworldHeight && player.ZoneRain && !NPC.AnyNPCs(ModContent.NPCType<ThunderstormEye>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            int type = ModContent.NPCType<ThunderstormEye>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, type);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }

        return true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Cloud, 25)
        .AddIngredient(ItemID.RainCloud, 10)
        .AddIngredient(ItemID.SunplateBlock, 20)
        //.AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.EvilBar), 3)
        .AddTile(TileID.Anvils)
        .Register();
}