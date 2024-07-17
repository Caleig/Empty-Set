using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Flails;

/// <summary>
/// 暗水晶
/// </summary>
internal class DarkCrystal : ModProjectile
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("暗水晶");
    }
    public override void SetDefaults()
    {
        Projectile.width = 20;
        Projectile.height = 20;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.light = 0.5f;
        Projectile.timeLeft = 5 * EmptySet.Frame;
        Projectile.tileCollide = false;

        Projectile.DamageType = DamageClass.Melee;
        Projectile.ignoreWater = false;
        Projectile.aiStyle = -1;
    }
    public override void AI()
    {
        Projectile.rotation += 0.5f * Projectile.direction;
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, 0, 0, 0, Color.Purple);
        dust1.noGravity = true;
        dust1.noLight = false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 5; i++)
            Dust.NewDustDirect(target.position, target.width, target.height, DustID.PurpleTorch).noGravity = false;
    }
}
