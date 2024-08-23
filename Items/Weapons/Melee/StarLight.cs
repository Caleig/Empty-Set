using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 星华
/// </summary>
public class StarLight : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Star Light");
    CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 10, 75);

        Item.width = 72; //已精确测量
        Item.height = 72;

        Item.damage = 26;
        Item.crit = 3;
        Item.scale = 0.8f;

        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = KnockBackLevel.BeLower;
        Item.useAnimation = UseSpeedLevel.FastSpeed;
        Item.useTime = UseSpeedLevel.FastSpeed;
        Item.UseSound = SoundID.Item1;
        Item.noUseGraphic = true;
        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;

        Item.shoot = ModContent.ProjectileType<StarLightProj2>();
        Item.shootSpeed = 15f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire3, 3 * EmptySet.Frame);
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        
    }
    public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
    {
        Player player = drawPlayer;
        Texture2D texture = ModContent.Request<Texture2D>("EmptySet/Projectiles/Weapons/Melee/StarLightProj").Value;
        Vector2 pos = player.Center - Main.screenPosition;
        if (player.channel)
        {
            if (player.direction > 0)
            {
                Main.spriteBatch.Draw(texture, new Vector2(pos.X - 10, pos.Y + 7), null, Microsoft.Xna.Framework.Color.White, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            else
            {
                Main.spriteBatch.Draw(texture, new Vector2(pos.X + 10, pos.Y + 7), null, Microsoft.Xna.Framework.Color.White, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0f);
            }
        }
        base.DrawArmorColor(drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
    }

    //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //{
    //    //Dust.NewDust(position, Item.width, Item.height, DustID.RedTorch);//Direct
    //    //Dust.NewDust(position, Item.width, Item.height, DustID.OrangeTorch);
    //    return true;
    //}
    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.FallenStar, 3)
        .AddIngredient(ItemID.MeteoriteBar, 5)
        .AddTile(TileID.Anvils) //铁砧
        .Register();
}