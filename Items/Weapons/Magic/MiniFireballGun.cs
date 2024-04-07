using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Magic;
using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 微型火球枪
/// </summary>
public class MiniFireballGun:ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Mini Fireball Gun");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 54;
        Item.height = 22;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Magic;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.damage = 42;
        Item.mana = 9;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 2, 35);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.channel = true;
        Item.shoot = ModContent.ProjectileType<MiniFireballGunProj>();
        Item.shootSpeed = 8;
    }

    public override Vector2? HoldoutOffset() => new Vector2(-6f, 0f);
}
