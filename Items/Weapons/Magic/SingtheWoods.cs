using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;

namespace EmptySet.Items.Weapons.Magic
{
    public class SingtheWoods : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Sing the Woods");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            /* Tooltip.SetDefault("绿色能量环绕在周围\n" + 
                            "“吸收自然精华”"); */
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.noMelee = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.damage = 6;
            Item.shootSpeed = 25f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.SingtheWoods>();
            Item.UseSound = SoundID.Item20;
            Item.staff[Item.type] = true;
            Item.mana = 2;
            Item.scale = 1f;
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(620, 20)
                .AddIngredient(331, 5)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
        public override Vector2? HoldoutOffset() 
        {
		    return new Vector2(0f, 5f);
		}
    }
}