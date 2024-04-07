using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 林翠镰
/// </summary>
public class JungleSickle : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Jungle Sickle");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 0, 50);

        Item.width = 54;
        Item.height = 48;

        Item.damage = 27;
        Item.crit = 12;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.UseSound = SoundID.Item71; //镰刀

        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    { 
        if (hit.Crit)
            target.AddBuff(BuffID.Poisoned, 10 * EmptySet.Frame);
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.JungleSpores, 7)
        .AddTile(TileID.WorkBenches)
        .Register();
}