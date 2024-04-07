using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using EmptySet.Projectiles.Melee;

namespace EmptySet.Items.Weapons.Melee
{
    internal class 灼银 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("灼银");
        }

        public override void SetDefaults()
        {
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee; 
            Item.width = 108;
            Item.height = 108;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing; 
            Item.noMelee = true; 
            Item.noUseGraphic = true;
            Item.knockBack = 6;
            Item.value = 20000;
            Item.rare = 5;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<测试武器弹幕1>(); 
            Item.shootSpeed = 8; 
            Item.crit = 8; 
        }
        private int l = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (l % 2 == 0)
            {
                Projectile.NewProjectile(source, position + new Vector2(0, -24), velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            else if (l % 2 == 1)
            {
                Projectile.NewProjectile(source, position + new Vector2(0, -24), velocity, type, damage, knockback, player.whoAmI, 1f, 0f);
            }
            l++;
            return false;
        }
    }
}
