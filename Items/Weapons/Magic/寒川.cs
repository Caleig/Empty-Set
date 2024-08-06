using System;
using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Magic;
using EmptySet.Projectiles.Ranged;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic;

/// <summary>
/// 寒川
/// </summary>
public class 寒川 : ModItem
{
    public override void SetStaticDefaults()
    {

        Item.staff[Item.type] = true;
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 10, 0, 0);

        //Item.width = 40;
        //Item.height = 36;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = UseSpeedLevel.FastSpeed + 2;
        Item.useAnimation = UseSpeedLevel.FastSpeed + 2;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 31;
        Item.knockBack = KnockBackLevel.BeHigher;
        Item.crit = 4;
        Item.UseSound = SoundID.Item20;
        Item.mana = 15;

        Item.autoReuse = true;
        Item.noMelee = true;


        Item.shoot = ProjectileID.FrostBoltStaff;//ModContent.ProjectileType<寒霜矢>();
        Item.shootSpeed = 10f;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
            var offset_y = 40;
            var offset_x = 40;
            var rand = Main.rand;
            var pos = position;
            for (int i = 0; i < 4; i++)
            {
                if (Main.MouseScreen.X + Main.screenPosition.X - player.position.X < 0)
                    pos = new Vector2(pos.X + rand.Next(offset_x), pos.Y - offset_y / 2f + rand.Next(offset_y));
                else
                    pos = new Vector2(pos.X - rand.Next(offset_x), pos.Y - offset_y / 2f + rand.Next(offset_y));


                Projectile.NewProjectile(source, pos, velocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
        if (Main.mouseRight)
        {
            Projectile.NewProjectile(source, position, velocity/2, ModContent.ProjectileType<MoonProj>(), damage, KnockBackLevel.BeLower, player.whoAmI);
            return false;
        }
        return false;
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<寒川锭>(), 30)
        .AddIngredient(ItemID.FrostStaff) //寒霜法杖
        .AddIngredient(ItemID.FrostCore,2) //寒霜核
        .AddTile(TileID.MythrilAnvil)
        .AddTile(TileID.HeavyWorkBench)
        .Register();
}
