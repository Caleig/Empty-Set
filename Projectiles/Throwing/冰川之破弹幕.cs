using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.Utils;
using EmptySet.Extensions;
using Terraria.ID;

namespace EmptySet.Projectiles.Throwing;
/// <summary>
/// 冰川之破弹幕
/// </summary>
public class 冰川之破弹幕 : ModProjectile
{
    public override string Texture => "EmptySet/Items/Weapons/Throwing/冰川之破";
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Tungsten Steel Card");

    }
    public override void SetDefaults()
    {
        Projectile.width = 62;
        Projectile.height = 62;
        Projectile.extraUpdates = 1;
        Projectile.penetrate = 1;
        Projectile.scale = 0.5f;

        Projectile.DamageType = DamageClass.Throwing;
        Projectile.timeLeft = 10 * EmptySet.Frame;
        
        Projectile.tileCollide = false;
        Projectile.friendly = true;
    }

    public override void AI()
    {
        Projectile.DirectionalityRotation(0.13f);

        var npcs = Main.npc.Where((x) =>
            Vector2.Distance(x.position, Projectile.position) < 1000 &&
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
            var v2f = (v2 * 0.00046f + v2p).SafeNormalize(Vector2.One);
            Projectile.velocity = v2f * len;
        }
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.LetExplodeWith(4, () =>
        {
            for (int i = 0; i < 20; i++)
            {
                var d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Snow);
                d.noGravity = true;
            }
        },default,true,false);
        Projectile.Kill();
    }
}
