using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using EmptySet.Projectiles.Weapons.Throwing;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Throwing;

/// <summary>
/// 克苏鲁之牙
/// </summary>
public class FangofCthulhu : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 300;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 0, 15);

        Item.width = 20;
        Item.height = 50;

        Item.damage = 20;
        Item.crit = 4 - 4;
        Item.maxStack = 999;

        Item.knockBack = 1;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 20;
        Item.useAnimation = 20;

        Item.DamageType = DamageClass.Throwing;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.autoReuse = false;
        Item.consumable = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<FangofCthulhuProj>();
        Item.shootSpeed = 9f;
    }
}
