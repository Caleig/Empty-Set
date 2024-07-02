using EmptySet.Common.Players;
using EmptySet.Common.Systems;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Ranged;
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
/// 充能弓
/// </summary>
public class ChargedBow : ModItem
{
    private int EnergyCost = 9;
    //private int _count = 1;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Charged Bow");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 62;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.NormalSpeed;
        Item.useAnimation = UseSpeedLevel.NormalSpeed;
        Item.autoReuse = true;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.damage = 11;
        Item.knockBack = KnockBackLevel.None;
        Item.crit = 4-4;
        Item.value = Item.sellPrice(0, 0, 3, 0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item5;

        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 8f;
        Item.useAmmo = AmmoID.Arrow;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
        if(Main.mouseRight)
        {
            var myEnergier = player.GetModPlayer<EnergyPlayer>();
            if (myEnergier.Consume(EnergyCost))
            {
                SoundEngine.PlaySound(SoundID.Item75, player.position);
                Projectile.NewProjectile(source, position, velocity.SafeNormalize(Vector2.One)*13f, ModContent.ProjectileType<ChargedArrowProj>(), damage, knockback, Main.myPlayer);
            }
        }
        return false;
    }

    public override Vector2? HoldoutOffset() => new Vector2(-5f, 0);
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<EnergyPlayer>().CanConsume(EnergyCost);
    public override void AddRecipes() => CreateRecipe()
        .AddRecipeGroup(MyRecipeGroup.Get(MyRecipeGroupId.CopperOrTinBow))
        .AddIngredient<MetalFragment>(10)
        .AddIngredient<ChargedCrystal>(2)
        .AddTile(TileID.Anvils)
        .Register();

}