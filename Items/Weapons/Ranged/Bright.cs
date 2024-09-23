using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
using EmptySet.Tiles;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Ranged;

/// <summary>
/// 明
/// </summary>
public class Bright : ModItem
{
    private int EnergyCost = 5;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 80;
        Item.height = 28;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 40;
        Item.useAnimation = 40;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 192;
        Item.knockBack = 8;
        Item.crit = 30;
        Item.value = Item.sellPrice(0, 9, 9, 9);
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item36;

        Item.shoot = ProjectileID.Bullet;
        Item.shootSpeed = 18f;
        Item.useAmmo = AmmoID.Bullet;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) => !Main.rand.NextBool();
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SniperRifle)
        .AddIngredient(ModContent.ItemType<Clamour>(), 1)
        .AddIngredient(ModContent.ItemType<SoulOfPolymerization>(), 2)
        .AddTile(ModContent.TileType<FusioninstrumentTile>())
        .Register();
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
        if (Main.mouseLeft)
        if (type == ProjectileID.Bullet)//火枪子弹
        {
            SoundEngine.PlaySound(SoundID.Item36, player.position);
            Projectile.NewProjectileDirect(source,position,velocity*7/4, ProjectileID.MoonlordBullet, damage, knockback,Item.whoAmI);
            return false;
        }
        if(Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
            {
                Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.One)*20f, ProjectileID.MagicMissile, damage, knockback, Main.myPlayer);
            }
        }

        return true;
    }
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
    public override Vector2? HoldoutOffset() => new Vector2(-12f, 0);
}
