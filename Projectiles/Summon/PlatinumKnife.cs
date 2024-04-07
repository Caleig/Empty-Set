using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Summon;

public class PlatinumKnife : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Self-defense knife");

    }

    public sealed override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.CopperShortswordStab);
        Projectile.width = 50;
        Projectile.height = 50;
        Projectile.tileCollide = false; // ���ʹ����ɵش�����ש

        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Summon;
    }
}
