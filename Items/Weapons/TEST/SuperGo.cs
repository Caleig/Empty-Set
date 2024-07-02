using Microsoft.Xna.Framework;
using EmptySet.Projectiles.TEST;
using EmptySet.Utils;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.TEST;

/// <summary>
/// mode debugger
/// </summary>
public class SuperGo : ModItem
{
    private int mode = 0;
    private int ModeMax = 3;
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Super Go");
        //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Super Go");
        //Tooltip.SetDefault("Right click change mode");
        //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "右击切换模式");
        //CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        //var t = Language.ActiveCulture.Name switch
        //{
        //    "zh-Hans" => "测试物品",
        //    _ => "It does nothing when equipped with BurnHell Talisman."
        //};

        var t = mode switch
        {
            0 => "Test Item(Sword)",
            1 => "Test Item(Bow)",
            2 => "Test Item(Book)",
            _=> "Test Item"
        };

        //tooltips.Add(new TooltipLine(Mod, "ItemName", t));
        var item = tooltips.Find(x => x.Name == "ItemName");
        if(item is not null)
            item.Text = t;

    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 3, 0, 0);

        Item.width = 17;
        Item.height = 17;

        Item.scale = 4f;
        Item.crit = 0;
        Item.damage = 999999;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.useAnimation = UseSpeedLevel.FastSpeed;

        Item.DamageType = DamageClass.Generic;
        Item.useStyle = ItemUseStyleID.Swing;
        

        Item.autoReuse = true;

        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.shootSpeed = 8f;
    }
    public override bool AltFunctionUse(Player player)
    {
        mode = mode + 1 == ModeMax ? 0 : mode + 1;
        return true;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse ==2)
            Item.UseSound = SoundID.Item4;
        else
            Item.UseSound = SoundID.Item1; //挥剑

        bool p = mode switch
        {
            0 => Melee(),
            1 => Shoot(),
            _ => Magic(),

        };
        return p;
        bool Melee()
        {
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.DamageType = DamageClass.Melee;
            Item.useTime = UseSpeedLevel.SlowSpeed;
            Item.useAnimation = UseSpeedLevel.SlowSpeed;
            Item.ammo = AmmoID.None;
            Item.mana = 0;
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SuperGoProj2>(), damage, knockback, Item.whoAmI);
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VirtualJungleSickle>(), damage, knockback, Item.whoAmI);
            return true;
        }

        bool Shoot()
        {
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = UseSpeedLevel.SuperFastSpeed;
            Item.useAnimation = UseSpeedLevel.SuperFastSpeed;
            Item.ammo = AmmoID.Arrow;
            Item.mana = 0;
            return true;
        }

        bool Magic()
        {
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.DamageType = DamageClass.Magic;
            Item.useTime = UseSpeedLevel.FastSpeed;
            Item.useAnimation = UseSpeedLevel.FastSpeed;
            Item.mana = 3;
            return true;
        }
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseRight) 
            return false;
        bool p = mode switch
        {
            0 => Melee(),
            1 => Shoot(),
            _ => Magic(),

        };

        bool Melee()
        {
            //Item.noMelee = true;
            //Item.noUseGraphic = true;

            //Item.useStyle = ItemUseStyleID.Swing;
            //Item.DamageType = DamageClass.Melee;
            //Item.useTime = UseSpeedLevel.SlowSpeed;
            //Item.useAnimation = UseSpeedLevel.SlowSpeed;
            //Item.ammo = AmmoID.None;
            //Item.mana = 0;
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SuperGoProj2>(), damage, knockback, Item.whoAmI);
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SuperGoProj2>(), damage, knockback, Item.whoAmI);
            //Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<VirtualJungleSickle>(), damage, knockback, Item.whoAmI);
            return false;
        }

        bool Shoot()
        {
            //Item.noMelee = true;
            //Item.noUseGraphic = true;
            //Item.useStyle = ItemUseStyleID.Shoot;
            //Item.DamageType=DamageClass.Ranged;
            //Item.useTime = UseSpeedLevel.SuperFastSpeed;
            //Item.useAnimation = UseSpeedLevel.SuperFastSpeed;
            //Item.ammo = AmmoID.Arrow;
            Projectile.NewProjectile(source, position, velocity, ProjectileID.JestersArrow, damage, knockback, Item.whoAmI);

            return false;
        }

        bool Magic()
        {
            //Item.noUseGraphic = true;
            //Item.noMelee = true;
            //Item.useStyle = ItemUseStyleID.Shoot;
            //Item.DamageType = DamageClass.Magic;
            //Item.useTime = UseSpeedLevel.FastSpeed;
            //Item.useAnimation = UseSpeedLevel.FastSpeed;
            //Item.mana = 3;
            Projectile.NewProjectile(source, position, velocity, ProjectileID.Leaf, damage, knockback, Item.whoAmI);
            return false;
        }

        return false;
    }
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.PlatinumCoin,999)
        .Register();
}