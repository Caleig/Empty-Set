using EmptySet.NPCs.MiniBoss.ThunderstormEye;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.NPCs.Boss.EarthShaker;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Boss.EarthShaker;

namespace EmptySet.Items.Consumables;

/// <summary>
/// 巡游信标
/// </summary>
public class Paradebuoy : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
    }

    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 26;
        Item.maxStack = 20;
        Item.value = Item.sellPrice(0, 0, 15, 0);
        Item.rare = ItemRarityID.Blue;
        Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
        Item.useTime = UseSpeedLevel.NormalSpeed + 2;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.consumable = true;
    }

    public override bool CanUseItem(Player player) =>
        player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<EarthShaker>());

    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            int type = ModContent.NPCType<EarthShaker>();

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnBoss((int)player.position.X, (int)player.position.Y - 50 * 16, type, player.whoAmI);
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
            }
        }
        Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Top, new Microsoft.Xna.Framework.Vector2(0, -1)*14f, ModContent.ProjectileType<ChargedCrystal2Projectile>(), 0, 0);
        return true;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<Crystalfragments>(), 5)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.CopperOrTin), 4)
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.IronOrLead), 6)
        .AddTile(TileID.Anvils)
        .Register();
}
