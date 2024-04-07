using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;
/// <summary>
/// 林翠投矛弹幕
/// </summary>
public class JungleThrowingSpearProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 13;//26;
        Projectile.height = 13;//62;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = (int)(6.5 * EmptySet.Frame);
        Projectile.aiStyle = 1;
        DrawOffsetX = -6;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        //if (Main.rand.Next(100) < 100)
        target.AddBuff(BuffID.Poisoned, 5 * EmptySet.Frame);
    }

    public override void AI()
    {
        int dust = Dust.NewDust(Projectile.Hitbox.Location.ToVector2() + new Vector2(4, 0), 3, 3, DustID.JungleSpore);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].fadeIn = 0.8f;
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
        return base.OnTileCollide(oldVelocity);
    }
}
