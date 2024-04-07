using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Common.Effects.Item;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Melee;
using EmptySet.Utils;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 德塞拉亚
/// </summary>
public class Dersilya : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 20, 0, 0);

        Item.width = 70;
        Item.height = 70;

        Item.damage = 52;
        Item.crit = 4;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useAnimation = UseSpeedLevel.FastSpeed - 1;
        Item.useTime = UseSpeedLevel.FastSpeed - 1;
        Item.useStyle = ItemUseStyleID.Swing;
        //Item.scale = 0.8f;

        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<BigShadowBall>();
        Item.shootSpeed = 12f;
    }

    //斩击弹幕会持续存在0.5秒（斩击行的话放大一点，原来的2倍就差不多了）

    public override bool AltFunctionUse(Player player) => player.GetModPlayer<DersilyaEffect>().UseTimer == 0;
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        
        base.MeleeEffects(player, hitbox);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        if (Main.mouseLeft)
        {
            if(Main.MouseScreen.X + Main.screenPosition.X - player.position.X > 0)
            {
                Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity, type, damage, knockback, Item.whoAmI);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<der>(), damage, knockback, player.whoAmI);
            }
            else
                Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity, ModContent.ProjectileType<BigShadowBall2>(), damage, knockback, Item.whoAmI);
        }

        if (Main.mouseRight)
        {
            if (player.GetModPlayer<DersilyaEffect>().UseTimer == 0)
            {
                var dic = Main.MouseScreen.X + Main.screenPosition.X - player.position.X > 0;

                var vel = (Main.MouseScreen + Main.screenPosition) - player.position;

                SoundEngine.PlaySound(SoundID.Item71, position);
                ///+new Vector2(35,55)
                /// (dic? new Vector2(0, 30) : new Vector2(0, -30))
                Projectile.NewProjectile(source, player.Center , velocity,
                    ModContent.ProjectileType<DersilyaProj>(), damage * 3, knockback, Item.whoAmI,
                    vel.ToRotation());
                player.GetModPlayer<DersilyaEffect>().UseTimer = 7 * EmptySet.Frame;
            }
        }

        return false;
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        Player player = Main.player[Item.playerIndexTheItemIsReservedFor];
        int t = player.GetModPlayer<DersilyaEffect>().UseTimer;
        if (t != 0)
        {
            Color c = Color.White;
            Vector2 pos = position + new Vector2(15, 13);
            float sca = 0.7f;
            if (player.HeldItem.type == Type)
            {
                c = Color.Black;
                pos = position + new Vector2(20, 18);
                sca = 0.8f;
            }
            spriteBatch.DrawString(FontAssets.ItemStack.Value, (t / 60 + 1).ToString(), pos, c, 0, origin, sca, SpriteEffects.None, 0);
        }
    }

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient<LastingShadow>()
        .AddIngredient<CorruptionEpee>()
        .AddIngredient<CorruptShard>(4)
        .AddIngredient(ItemID.ChlorophyteBar,7)
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
