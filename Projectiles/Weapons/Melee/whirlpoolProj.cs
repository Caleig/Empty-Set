using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 雷涡弹幕
/// </summary>
public class whirlpoolProj : ModProjectile
{


    public override void SetDefaults()
    {
        Projectile.width = 38;
        Projectile.height = 38;//54
        Projectile.friendly = true;
        Projectile.penetrate = 4 + 1;
        Projectile.timeLeft = (int)(12 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;
    }


    public override void AI()
    {
        Projectile.rotation += 0.2f * Projectile.direction;
        Projectile.velocity *= 0.985f;
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, 0, 0, 0, Color.Cyan);
        dust1.noGravity = true;
        dust1.noLight = false;
    }
}
