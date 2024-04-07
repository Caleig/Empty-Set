using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing;
/// <summary>
/// 钨钢牌弹幕
/// </summary>
public class TungstenSteelCardProj : ModProjectile
{
    public override string Texture => "EmptySet/Items/Weapons/Throwing/TungstenSteelCard";
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Tungsten Steel Card");
    }
    public override void SetDefaults()
    {
        Projectile.width = 18;
        Projectile.height = 30;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = 10 * EmptySet.Frame;
       
        Projectile.penetrate = 2 + 1;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
    }
}
