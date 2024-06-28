using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using Terraria.GameContent.Creative;

namespace EmptySet.Items.Weapons.Magic
{
    public class DemonHand : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("demon hand book");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            /* Tooltip.SetDefault("“这本书似乎封印着一个恶魔，不过谁在乎呢？好用就行”\n"
                                +"那只看起来像柴犬的兽人如是说到"); */
            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Green;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.damage = 30;
            Item.shootSpeed = 2f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.DemonHand>();
            Item.UseSound = SoundID.Item46;
            Item.mana = 15;
            Item.scale = 1f;
            Item.crit = 5;
            Item.knockBack = 7;
            Item.noUseGraphic = true;
            base.SetDefaults();
        }
        public override void AddRecipes()
        {
            base.AddRecipes();
            CreateRecipe()
                .AddIngredient(ItemID.DemoniteBar, 10)
                .AddIngredient(ItemID.ShadowScale, 10)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}