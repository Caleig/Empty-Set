using Microsoft.Xna.Framework;
using EmptySet.NPCs.Boss.LavaHunter;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 熔岩微光灯
/// </summary>
public class LavaShimmerLamp : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
    }

    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 30;
        Item.maxStack = 1;
        Item.UseSound = SoundID.Item105;
        Item.value = Item.sellPrice(0, 1, 60, 0);
        Item.rare = ItemRarityID.Green;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useStyle = ItemUseStyleID.RaiseLamp;
        Item.consumable = false;
        Item.holdStyle = ItemHoldStyleID.HoldLamp;
        Item.accessory = true;
        Item.scale = 0.7f;
    }

    public override bool CanUseItem(Player player) =>
        player.ZoneUnderworldHeight && !NPC.AnyNPCs(ModContent.NPCType<LavaHunter>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            int type = ModContent.NPCType<LavaHunter>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
                NPC.SpawnBoss((int)(player.position.X + 1200), (int)(player.position.Y + 1200), type, Main.myPlayer);
            else
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
        }
        return true;
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        Lighting.AddLight(player.Center, TorchID.Crimson);
        base.UpdateAccessory(player, hideVisual);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Fireblossom, 2)
        .AddIngredient(ItemID.HellstoneBar, 5)
        .AddIngredient(ItemID.Obsidian, 30)
        .AddTile(TileID.Hellforge)
        .Register();

    public override void HoldItem(Player player)
    {
        base.HoldItem(player);
        if (!player.ZoneUnderworldHeight) 
        {
            Lighting.AddLight(player.Center + new Vector2(player.direction == -1 ? -23 : 10, 5), TorchID.Crimson);
            if (Main.rand.NextBool(20)) 
            {
                int dust = Dust.NewDust(player.Center + new Vector2(player.direction == -1 ? -23 : 10, 5), 10, 10, DustID.CrimsonTorch);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn = 1f;
            }
        }
    }
}
