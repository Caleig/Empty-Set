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
        Projectile.width = 18;
        Projectile.height = 18;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.light = 0.5f;
        Projectile.timeLeft = 5 * EmptySet.Frame;
        Projectile.tileCollide = false;
    }
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        for (int i = 0; i < 5; i++)
            Dust.NewDustDirect(target.position, target.width, target.height, DustID.PurpleTorch).noGravity = false;
    }
}