using EmptySet.Projectiles.Throwing;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 恶魔投矛
/// </summary>
public class SatanicPiercer : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Satanic Piercer");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 13, 0);
        Item.width = 44;
        Item.height = 44;
        Item.damage = 20;
        Item.knockBack = 5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 24;
        Item.useAnimation = 24;
        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<SatanicPiercerProj>();
        Item.shootSpeed = 10f;
    }
}