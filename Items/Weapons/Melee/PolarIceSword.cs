using Microsoft.Xna.Framework;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 极地冰刀
/// </summary>
public class PolarIceSword : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 54;
        Item.height = 58;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 19;
        Item.knockBack = KnockBackLevel.Normal;
        Item.crit = 22;
        Item.value = Item.sellPrice(0,1,0,0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.Katana)
        .AddIngredient(ItemID.Shiverthorn, 2)
        .AddIngredient(ItemID.IceBlock, 12)
        .AddTile(TileID.Anvils)
        .Register();
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (Main.GameUpdateCount % 3 == 0)
        {
        Dust d = Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.IceTorch);
        d.velocity = Vector2.Zero;
        d.noGravity = true;
        }
    }
}
