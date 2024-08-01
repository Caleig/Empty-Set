using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using EmptySet.Projectiles.Melee;

namespace EmptySet.Items.Weapons.Melee
{
    internal class HeroGiantSword : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Player player = Main.LocalPlayer;
            Item.damage = 120;
            Item.DamageType = DamageClass.Melee;
            Item.width = 108;
            Item.height = 108;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.reuseDelay = 40 - (int)((player.GetAttackSpeed(DamageClass.Melee) - 1f) * 40);
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.value = 120000;
            Item.rare = 6;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.Weapons.Melee.HeroGiantSword>();
            Item.shootSpeed = 0;
            Item.crit = 8;
        }
        private int l = 0;
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.mouseRight)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Projectile.NewProjectile(source, position + new Vector2(0, -24), velocity, ModContent.ProjectileType<Projectiles.Weapons.Melee.Charging>(), damage, knockback, player.whoAmI, 0f, 0f);
            }
            else if (Main.mouseLeft)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                if (l % 2 == 0)
                {
                    Projectile.NewProjectile(source, position + new Vector2(0, -24), velocity, type, damage, knockback, player.whoAmI, 0f, 0f);
                }
                else if (l % 2 == 1)
                {
                    Projectile.NewProjectile(source, position + new Vector2(0, -24), velocity, type, damage, knockback, player.whoAmI, 1f, 0f);
                }
                l++;
            }
            
            return false;
        }
    }
}
