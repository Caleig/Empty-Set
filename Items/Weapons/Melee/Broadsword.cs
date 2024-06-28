using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;
using System;
using Microsoft.Xna.Framework.Input;
using SteelSeries.GameSense.DeviceZone;
using Terraria.GameContent;
using ReLogic.Content;

namespace EmptySet.Items.Weapons.Melee
{
    public class Broadsword : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // DisplayName.SetDefault("broad sword");
            /* Tooltip.SetDefault("刀把上镶嵌的水晶发着红光\n"+
                                    "右键向前冲刺并附带伤害\n" +
                                            "左键蓄力攻击"); */
        }
        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 12;
            Item.shootSpeed = 0f;
            Item.useAnimation = 12;
            Item.useStyle = 5;
            Item.noUseGraphic = true;
            Item.knockBack = 6;
            Item.value = 10000;
            Item.rare = 2;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.BTLcCharging>();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 5)
                .AddIngredient(ItemID.Wood, 3)
                .AddIngredient(ModContent.ItemType<Items.Materials.ImpurityMagicCrystal>())
                .AddTile(TileID.Anvils)
                .Register();                           
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if(player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<Buffs.Speedfaster>()))
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.Effects.Moswordver>();
                Item.shootSpeed = 7f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.BTLcCharging>();
                Item.shootSpeed = 0f;
            }
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if(player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<Buffs.Speedfaster>()))
            {
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}