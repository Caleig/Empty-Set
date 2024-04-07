using System.Linq;
using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using EmptySet.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.NoType;
/// <summary>
/// 极寒冰晶弹幕
/// </summary>
public class 极寒冰晶Proj : ModProjectile
{
    private int Timer { get; set; } = 0;

    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
        Projectile.width = 38;
        Projectile.height = 38;
        Projectile.timeLeft = 2;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        //Projectile.friendly
    }

    private int frame = 0;
    public override void AI()
    {
        Projectile.DirectionalityRotation(0.13f);
        frame++;
        if (frame == 30)
        {
            frame = 0;
            var npcs = Main.npc.Where((x) =>
                Vector2.Distance(x.position, Projectile.position) < 400 &&
                x.friendly == false &&
                x.active
            );
            var npc = npcs.OrderBy(x => Vector2.Distance(x.position, Projectile.position)).ToList();
            if (npc.Count > 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.position,
                    (npc[0].position - Projectile.position) *
                    0.03f,
                    ProjectileID.FrostBoltStaff, 30, Utils.KnockBackLevel.BeLower, Projectile.owner);
            }
        }

    }
}
