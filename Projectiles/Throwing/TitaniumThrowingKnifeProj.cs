using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Throwing;

/// <summary>
/// 钛金飞刀
/// </summary>
public class TitaniumThrowingKnifeProj : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 32;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
    }
    public override void AI() => Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
}
public class TitaniumThrowingKnifeProj2 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 26; //已精确测量
        Projectile.height = 26;
        Projectile.friendly = true;
        Projectile.timeLeft = (int)(9 * EmptySet.Frame);
    }
    
    public override void AI()
    {
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
        //Projectile.velocity *= 0.87f;
        var npcs = Main.npc.Where((x) =>
            Vector2.Distance(x.position, Projectile.position) < 200 &&
            x.friendly == false &&
            x.active
        );
        var npc = npcs.OrderBy(x => Vector2.Distance(x.position, Projectile.position)).ToList();
        if (npc.Count > 0)
        {
            var len = Projectile.velocity.Length();
            var np = npc[0].position;
            var pp = Projectile.position;
            var v2 = new Vector2(np.X - pp.X, np.Y - pp.Y);
            var v2p = Projectile.velocity;
            var v2f = (v2 * 0.015f + v2p).SafeNormalize(Vector2.One);
            Projectile.velocity = v2f*len;
        }
    }
}