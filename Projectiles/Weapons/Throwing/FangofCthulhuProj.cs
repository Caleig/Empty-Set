using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;

/// <summary>
/// 克苏鲁之牙弹幕
/// </summary>
public class FangofCthulhuProj : ModProjectile
{
    private int fr = 0;
    public override void SetDefaults()
    {
        Projectile.width = 40;
        Projectile.height = 12;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(10 * EmptySet.Frame);
        Projectile.penetrate = 1 + 1;
        Projectile.aiStyle = 1;
    }

    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
        dust1.noGravity = true;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);


    }
        public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
    public override void ModifyDamageHitbox(ref Rectangle hitbox)
    {
        hitbox.X += 4;
        hitbox.Y += 4;
        hitbox.Width -= 7;
        hitbox.Height -= 7;
    }
}
