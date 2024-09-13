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
/// 仲裁巨剑
/// </summary>
public class ArbitralSword : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Arbitral Sword");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 15, 0, 0);

        Item.width = 88;
        Item.height = 88;

        Item.damage = 77;
        Item.crit = 4;

        Item.knockBack = KnockBackLevel.BeLower;
        Item.useAnimation = 37;
        Item.useTime = 37;
        Item.useStyle = ItemUseStyleID.Swing;
        //Item.scale = 0.8f;

        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;

        Item.UseSound = SoundID.Item105;
        Item.shoot = ModContent.ProjectileType<arbitrationProj>();
        Item.shootSpeed = 17f;
    }

    //斩击弹幕会持续存在0.5秒（斩击行的话放大一点，原来的2倍就差不多了）

    public override bool AltFunctionUse(Player player) => player.GetModPlayer<DersilyaEffect>().UseTimer == 0;
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {

        base.MeleeEffects(player, hitbox);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        {

            if (Main.MouseScreen.X + Main.screenPosition.X - player.position.X > 0)
            {
                Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity, type, damage, knockback, Item.whoAmI);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<arbitration>(), damage, knockback, player.whoAmI);
            }
            else
            {
                Projectile.NewProjectile(source, position + new Vector2(0, -4), velocity, ModContent.ProjectileType<arbitrationProj2>(), damage, knockback, Item.whoAmI);
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<arbitration2>(), damage, knockback, player.whoAmI);
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
        //.AddIngredient(ModContent.ItemType<SunplateBlade>())
        .AddIngredient(ItemID.SoulofLight, 4) //光明之魂
        .AddIngredient(ModContent.ItemType<TungstenSteelSword>(), 1)
        .AddIngredient(ItemID.CrystalShard, 10)
        .AddIngredient(ItemID.HallowedBar, 10) //神圣锭  ItemID.LightShard 光明碎块
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
