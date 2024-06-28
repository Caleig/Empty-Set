using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Creative;

namespace EmptySet.Items.Weapons.Magic
{
    public class ApprenticeStaves : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Apprentice Staves");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            // Tooltip.SetDefault("通过操控魔法粒子直接进行攻击");
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.noMelee = true;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 0, 21, 0);
            Item.value = Item.buyPrice(0, 0, 63, 0);
            Item.damage = 35;
            Item.shootSpeed = 20f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.Magic1>();
            Item.UseSound = SoundID.Item20;
            Item.staff[Item.type] = true;
            Item.mana = 5;
            Item.scale = 1.3f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.PalmWood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.BorealWood, 5)
                .AddIngredient(ModContent.ItemType<Items.Materials.MagicCrystal>(), 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(0f, 5f);
		}
    }
}