using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using EmptySet.Projectiles.Summon.LavaHunterProj;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 焚炎獠牙
/// </summary>
public class FlameFang : ModItem
{
    internal int projectile = 0;
    public override void SetStaticDefaults()
    {
    }
    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 42;
        Item.value = Item.sellPrice(0, 2, 0, 0);
        Item.rare = ItemRarityID.Orange;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Throwing) += 0.05f;
        player.GetDamage(DamageClass.Summon) += 0.05f;
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.GetDamage(DamageClass.Magic) += 0.05f;
        player.GetDamage(DamageClass.Ranged) += 0.05f;

    }
}