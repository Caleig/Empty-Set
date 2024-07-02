using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Common.Effects.Item;
using EmptySet.Projectiles.Melee.Issloos;
using EmptySet.Utils;
using ReLogic.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 伊希鲁斯
/// </summary>
public class Issloos : ModItem
{
    int count = 0;
    int index = 0;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Issloos");
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = UseSpeedLevel.FastSpeed + 2;
        Item.useAnimation = UseSpeedLevel.FastSpeed + 2;
        Item.DamageType = DamageClass.Melee;
        Item.scale = 0.9f;
        Item.damage = 98;
        Item.knockBack = KnockBackLevel.BeHigher;
        Item.crit = 13;
        Item.value = Item.sellPrice(0, 23, 6, 0);
        Item.rare = ItemRarityID.Lime;
        Item.UseSound = SoundID.Item71;
        Item.autoReuse = true;
        
        Item.shoot = ModContent.ProjectileType<IssloosProj>();
        Item.shootSpeed = 12f;
    }
    public override bool AltFunctionUse(Player player) => player.GetModPlayer<IssloosEffect>().UseTimer == 0;
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        var effect = player.GetModPlayer<IssloosEffect>();
        if (Main.mouseLeft)
        {
            Item.useTime = UseSpeedLevel.FastSpeed + 2;
            Item.useAnimation = UseSpeedLevel.FastSpeed + 2;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        }
        if(effect.UseTimer==0 && Main.mouseRight)
        {
            if (count == 5 || effect.SpaceTimer == 0) 
                count = 0;
            effect.SpaceTimer = 180;

            Item.useTime = UseSpeedLevel.SlowSpeed - 2;
            Item.useAnimation = UseSpeedLevel.SlowSpeed - 2;

            count = count switch
            {
                0 => SpawnProj1(),
                1 => SpawnProj2(),
                2 => SpawnProj3(),
                3 => SpawnProj4(),
                4 => SpawnProj5(),
                _ => 5
            };

            int SpawnProj1()
            {
                var startPos = player.Center + new Vector2(-player.direction * 150, -150);
                var vel = (Main.MouseWorld - startPos).SafeNormalize(Vector2.Zero) * 12;
                index = Projectile.NewProjectile(source, startPos, vel, ModContent.ProjectileType<VirtualJungleSickle>(), damage, knockback, player.whoAmI);
                Main.projectile[index].spriteDirection = player.direction;
                Main.projectile[index].ai[0] = player.Center.X;
                Main.projectile[index].ai[1] = player.Center.Y;
                return 1;
            }
            int SpawnProj2()
            {
                var startPos = player.Center + new Vector2(-player.direction * 150, 0);
                var vel = (Main.MouseWorld - startPos).SafeNormalize(Vector2.Zero) * 12;
                index = Projectile.NewProjectile(source, startPos, vel, ModContent.ProjectileType<VirtualEternityAshSickle>(), damage, knockback, player.whoAmI);
                Main.projectile[index].spriteDirection = player.direction;
                return 2;
            }
            int SpawnProj3()
            {
                var startPos = player.Center + new Vector2(-player.direction * 150, 150);
                var vel = (Main.MouseWorld - startPos).SafeNormalize(Vector2.Zero) * 12;
                index = Projectile.NewProjectile(source, startPos, vel, ModContent.ProjectileType<VirtualGloriousBrokener>(), damage, knockback, player.whoAmI);
                Main.projectile[index].spriteDirection = -player.direction;
                Main.projectile[index].ai[0] = player.Center.X;
                Main.projectile[index].ai[1] = player.Center.Y;
                return 3;
            }
            int SpawnProj4()
            {
                SpawnProj1();
                SpawnProj2();
                SpawnProj3();
                return 4;
            }
            int SpawnProj5()
            {
                effect.UseTimer = 15 * 60;
                var startPos = Main.MouseWorld;
                var vel = Vector2.Zero;
                index = Projectile.NewProjectile(source, startPos, vel, ModContent.ProjectileType<VirtualIssloosProj>(), damage, 0, player.whoAmI);
                Main.projectile[index].spriteDirection = player.direction;
                return 5;
            }
        }

        return false;
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
        int t = player.GetModPlayer<IssloosEffect>().UseTimer;
        if (t != 0) 
        {
            Color c = Color.White;
            Vector2 pos = position + new Vector2(50,44);
            float sca = 0.7f;
            if (player.HeldItem.type == Type) 
            {
                c = Color.Black;
                pos = position + new Vector2(58,46);
                sca = 0.8f;
            }
            spriteBatch.DrawString(FontAssets.ItemStack.Value, (t / 60+1).ToString(), pos, c, 0, origin, sca, SpriteEffects.None, 0);
        }
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ModContent.ItemType<GloriousBrokener>())
        //.AddIngredient(ModContent.ItemType<FlamtaintBar>(), 3)//恒灰锭  ->  堕烬锭
        .AddIngredient(ItemID.ChlorophyteBar,7)//叶绿锭
        .AddIngredient(ItemID.BrokenHeroSword)
        .AddTile(TileID.LihzahrdAltar)//丛林蜥蜴祭坛
        .Register();
}
