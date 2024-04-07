using Microsoft.Xna.Framework;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Weapons.Special;
using EmptySet.Utils;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 光耀破碎者
/// </summary>
public class GloriousBrokener : ModItem
{
    private int colorSelector = 0;
    public override void SetStaticDefaults()
    {
       CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 11,11,11);

        Item.width = 86;//OK
        Item.height = 74;

        Item.damage = 63;
        Item.crit = 19;
        Item.scale = 0.9f;


        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = KnockBackLevel.Normal;
        Item.useAnimation = UseSpeedLevel.SlowSpeed;
        Item.useTime = UseSpeedLevel.SlowSpeed;
        Item.UseSound = SoundID.Item71; //镰刀

        Item.DamageType = DamageClass.Melee;

        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<HolyWave>();
        Item.shootSpeed = 7f;
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    { 
        //var red = Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height, DustID.RedTorch,0,0,0,default,2.5f); 
        //red.noGravity = true;
        //for (int i = 0; i < 2; i++) 
        //{ 
        var dust = Dust.NewDustDirect(hitbox.TopLeft(), hitbox.Width, hitbox.Height,
            colorSelector == 0 ? DustID.Torch : colorSelector == 1 ? DustID.RedTorch : DustID.Shadowflame, 0, 0, 0,
            default, 2.5f);
        dust.noGravity = true;
        colorSelector = colorSelector == 2 ? 0 : colorSelector + 1;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool CanUseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 11f;
                Item.shoot = ProjectileID.None;
                Projectile.NewProjectile(Entity.GetSource_FromAI(), player.Center, velocity, ModContent.ProjectileType<Projectiles.Melee.GloriousBrokener>(), 63, KnockBackLevel.Normal, player.whoAmI);
        }
        else
        {
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 7f;
            Item.shoot = ModContent.ProjectileType<HolyWave>();
            Projectile.NewProjectile(Entity.GetSource_FromAI(), player.Center, velocity, ModContent.ProjectileType<FelShadowSickle>(), 63, KnockBackLevel.Normal, player.whoAmI);
        }
        return true;
    }
    

    public override void AddRecipes() => CreateRecipe()
        .AddIngredient(ItemID.SoulofNight)
        .AddIngredient(ItemID.SoulofLight)
        .AddIngredient(ItemID.HallowedBar,3)//神圣锭
        .AddIngredient(ModContent.ItemType<EternityAshSickle>(), 1)
        .AddIngredient(ModContent.ItemType<FelShadowBar>(),3)//邪影锭
        .AddTile(TileID.MythrilAnvil)
        .Register();
}
