using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Throwing;
/// <summary>
/// 阴云磁刃弹幕
/// </summary>
public class CloudsShurikenProj : ModProjectile
{


    public override void SetDefaults()
    {
        Projectile.width = 46;
        Projectile.height = 46;//54
        Projectile.friendly = true;
        Projectile.penetrate = 2 + 1;
        Projectile.timeLeft = (int)(3 * EmptySet.Frame);


        Projectile.DamageType = DamageClass.Throwing;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.aiStyle = -1;
    }


    public override void AI()
    {
        Projectile.rotation += 0.2f * Projectile.direction;
        Projectile.velocity *= 0.99f;
        var dust1 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver, 0, 0, 0, Color.Cyan);
        dust1.noGravity = true;
        dust1.noLight = false;
        var dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BlueFlare);
        dust2.noGravity = true;
        dust2.noLight = false;

        Player player = Main.player[Projectile.owner];
        float distanceMax = 120f;
        NPC target = null;
        foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                            distanceMax = currentDistance;
                            target = npc;
                    }
                }
            }
            if(target != null)
            {
                var targetVel = Vector2.Normalize(target.position - Projectile.Center) * 5f;
                Projectile.velocity = (targetVel + Projectile.velocity * 4) / 5f;
            }
    }
}
