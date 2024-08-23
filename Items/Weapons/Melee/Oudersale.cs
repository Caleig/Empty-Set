using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Audio;

namespace EmptySet.Items.Weapons.Melee
{
    public class Oudersale : ModItem
    {
        short shootcount = 0;
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0, 20, 0, 0);

            Item.width = 102;
            Item.height = 88;

            Item.damage = 60;
            Item.crit = 11 - 4;

            Item.knockBack = KnockBackLevel.Normal;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            //Item.scale = 0.8f;

            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;

            Item.UseSound = SoundID.Item71;
            Item.shoot = ModContent.ProjectileType<OudersaleProj>();
            Item.shootSpeed = 9f;
        }
        //public override bool AltFunctionUse(Player player)
        //{
        //    //player.GetModPlayer<OudersaleEffect>().Enable();
        //    return true;
        //}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (shootcount > 2)
            {
                const int NumProjectiles = 2; // The humber of projectiles that this gun will shoot.


                for (int i = 0; i < NumProjectiles; i++)
                {
                    // Rotate the velocity randomly by 30 degrees at max.
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));

                    // Decrease velocity randomly for nicer visuals.
                    newVelocity *= 1f;

                    // Create a projectile.
                    Projectile.NewProjectileDirect(source, position, newVelocity, ModContent.ProjectileType<OudersaleProj2>(), damage / 2, knockback, player.whoAmI);

                }
                shootcount = 0;
            }
            shootcount++;
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<OudersaleProj>(), damage, knockback, player.whoAmI);
            //if (Main.mouseLeft)
            if (player.direction > 0)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Melee.OuderSale>(), damage, knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Projectiles.Melee.OuderSale2>(), damage, knockback, player.whoAmI);
            }
            
            
            //if (Main.mouseRight)
            //{
            //    Projectile.NewProjectile(source, position, Vector2.Zero, ModContent.ProjectileType<OudersaleDashProj>(),
            //        2*damage, knockback, Item.whoAmI);

            //    // 右键:右键进行蓄力，蓄力时间为3秒，蓄力完成后玩家身上会迸出血色粒子，松开右键进行一次横向穿怪冲刺加斩击，斩击的伤害为武器面板的2倍，
            //    // 冲刺距离为40格图格，冲刺时全程无敌，冲刺结束后回复玩家80点生命值，如果冲刺时击中敌怪的话，还会给予4秒的无敌帧，使用后进入25秒的冷却状态，冷却状态下无法使用右键攻击，左键不受影响
            //}


            //var age = (float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X);
            //var offset = //3倍为默认
            //    ((float)Math.Atan2((Main.MouseWorld - player.Center).Y, (Main.MouseWorld - player.Center).X) +
            //     0 * MathHelper.Pi / 36f).ToRotationVector2(); //90
            //var offset1 = ((age + 90 * MathHelper.Pi / 36f) * new Vector2(1, 0).Length()).ToRotationVector2() * 8;
            //var offset2 = ((age - 90 * MathHelper.Pi / 36f) * new Vector2(-1, 0).Length()).ToRotationVector2() * 8;

            //Projectile.NewProjectile(source, position + offset2, offset * 9, type, damage, knockback, player.whoAmI);
            return false;
        }

        //public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit) =>
        //    target.AddBuff(BuffID.CursedInferno, crit ? 15 * Reincarnation.Frame : 3 * Reincarnation.Frame);

        public override void AddRecipes() => CreateRecipe()
            .AddIngredient<BloodSickle>()
            .AddIngredient<FelShadowBar>(5)
            .AddIngredient<BloodShadow>(2)
            .AddIngredient(1508, 2)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}