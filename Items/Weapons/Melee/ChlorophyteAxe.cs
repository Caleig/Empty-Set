using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 苍翠钺
/// </summary>
public class ChlorophyteAxe : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Lime;
        Item.value = Item.sellPrice(0, 5, 15, 0);

        Item.width = 54;
        Item.height = 46;

        Item.damage = 110;
        Item.crit = 20;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 7;
        Item.useAnimation = 36;
        Item.useTime = 36;
        Item.UseSound = SoundID.Item1; //镰刀

        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<ChlorophyteAxeProj>();
        Item.shootSpeed = 12f;

    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<SoulOfPolymerization>(), 1)
        .AddIngredient(ItemID.ChlorophyteSaber)
        .AddIngredient(ItemID.ChlorophyteClaymore)
        .AddTile(TileID.MythrilAnvil)
        .Register();
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.GameUpdateCount % 3 == 0)
        {
        Dust d = Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.Ichor, 0, 0, 0, Color.Lime);
        d.velocity = Vector2.Zero;
        d.noGravity = true;
        }
    }
}



