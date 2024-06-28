
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Magic
{
    public class ScarletExplosiveBomb : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
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
            Item.rare = ItemRarityID.Green;
            Item.useTurn = false;
            Item.value = Item.sellPrice(0, 0, 21, 0);
            Item.value = Item.buyPrice(0, 0, 63, 0);
            Item.damage = 35;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<Projectiles.Magic.ScarletExplosiveBomb>();
            Item.UseSound = SoundID.Item20;
            Item.staff[Item.type] = true;
            Item.mana = 12;
            base.SetDefaults();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for(int i = 0; i < 20; i++)
            {
                Dust.NewDust(player.Center, 20, 20, DustID.RedsWingsRun, 0, 0, 0, Color.OrangeRed, 1f);
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}