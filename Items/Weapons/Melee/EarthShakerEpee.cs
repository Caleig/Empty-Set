using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 撼地者重剑
/// </summary>
public class EarthShakerEpee : ModItem
{
    private int EnergyCost = 5;
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 80;
        Item.height = 80;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = UseSpeedLevel.VerySlowSpeed;
        Item.useTime = UseSpeedLevel.VerySlowSpeed;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 30;
        Item.knockBack = KnockBackLevel.Normal+1f;
        Item.crit = 4-4;
        Item.value = Item.sellPrice(0, 0, 5, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shoot = ModContent.ProjectileType<EarthShakerEpeeProj>();
        Item.shootSpeed = 10f;
    }


    public override void AddRecipes() => CreateRecipe()
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.CopperOrBroadSword))
        .AddIngredient<MetalFragment>(15)
        .AddIngredient<ChargedCrystal>(3)
        .AddTile(TileID.Anvils)
        .Register();
    public override bool CanUseItem(Player player)
    {
        //bool Melee()
        //{
        //    Item.noMelee = true;
        //    Item.noUseGraphic = true;

        //    Item.useStyle = ItemUseStyleID.Swing;
        //    Item.DamageType = DamageClass.Melee;
        //    Item.useTime = UseSpeedLevel.SlowSpeed;
        //    Item.useAnimation = UseSpeedLevel.SlowSpeed;
        //    Item.ammo = AmmoID.None;
        //    Item.mana = 0;
        //    //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SuperGoProj2>(), damage, knockback, Item.whoAmI);
        //    //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VirtualJungleSickle>(), damage, knockback, Item.whoAmI);
        //    return true;
        //}

        //bool Shoot()
        //{
        //    Item.noMelee = true;
        //    Item.noUseGraphic = true;
        //    Item.useStyle = ItemUseStyleID.Shoot;
        //    Item.DamageType = DamageClass.Ranged;
        //    Item.useTime = UseSpeedLevel.SuperFastSpeed;
        //    Item.useAnimation = UseSpeedLevel.SuperFastSpeed;
        //    Item.ammo = AmmoID.Arrow;
        //    Item.mana = 0;
        return true;
        //}


    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
                Projectile.NewProjectile(source, position+new Vector2(0,-12), velocity, type, damage, knockback, Main.myPlayer);

        }
        return false;

    }

    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
    public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
    {
        if (Main.rand.NextBool(3)) 
        {
            Dust.NewDustDirect(new Vector2(0, -120).RotatedBy(player.itemRotation + MathHelper.ToRadians(20 * player.direction)) + player.Center, 2, 2, DustID.Electric).noGravity = true;
        }
        base.UseItemHitbox(player, ref hitbox, ref noHitbox);
    }
}
