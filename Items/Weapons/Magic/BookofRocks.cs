using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Items.Weapons.Magic
{
    public class BookofRocks : ModItem
    {
        public override void SetDefaults()
        {
            Item.noMelee = true;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = 5;
            Item.width = 40;
            Item.height = 40;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Blue;
            Item.useTurn = false;
            Item.value = Item.sellPrice(1, 12, 0, 0);
            Item.value = Item.buyPrice(1, 12, 0, 0);
            Item.damage = 30;
            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BookofRocks>();
            Item.UseSound = SoundID.Item20;
            Item.mana = 20;
            Item.scale = 1f;
            Item.crit = 20;
            Item.knockBack = 6;
            base.SetDefaults();
        }
    }
}