using EmptySet.Projectiles.Weapons.Special;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Yoyo;

/// <summary>
/// 恒灰悠悠球弹幕
/// </summary>
public class EternityAshYoyoProj : ModProjectile
{
    private int _timer = 0;

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.WoodYoyo);
        Projectile.width = 16; //已精确测量
        Projectile.height = 16;
    }

    public override void AI()
    {
        var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
        dust.noGravity = true;
        dust.noLight = false;
        if (_timer == (int)(0.5 * EmptySet.Frame))
        {
            _timer = 0;
            Projectile.NewProjectile(new EntitySource_Parent(Projectile), Projectile.Center.X, Projectile.Center.Y, 0, 0,
                ModContent.ProjectileType<EternityAshEnergy>(), Projectile.damage / 2, Projectile.knockBack,
                Projectile.owner);
        }
        else
            _timer++;
    }
}