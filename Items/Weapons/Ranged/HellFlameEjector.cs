using EmptySet.Utils;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 狱火喷射器
/// </summary>
public class HellFlameEjector : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 94;
        Item.height = 26;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.DamageType = DamageClass.Ranged;
        Item.knockBack = KnockBackLevel.None;
        Item.damage = 27;
        Item.crit = 4;
        Item.value = Item.sellPrice(0, 0, 2, 35);
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse = true;
        Item.noMelee = true;

        Item.useAmmo = AmmoID.Gel;
        Item.shoot = ProjectileID.Flames;
        Item.shootSpeed = 8;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => Main.rand.NextFloat() >= 0.31f;

    public override Vector2? HoldoutOffset() => new Vector2(-6f, 0f);
}
