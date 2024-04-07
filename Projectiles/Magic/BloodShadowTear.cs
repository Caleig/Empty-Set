using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

/// <summary>
/// 血影泪(血影空洞杖弹幕)
/// </summary>
public class BloodShadowTear : ModProjectile
{
    private int ok;
    private bool isUp;
    private bool isGetTarget=false;
    private bool isChanging = false;
    private NPC target;
    private Vector2 vel;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Blood Shadow Tear");
    }
    public override void SetDefaults()
    {
        Projectile.width = 22;
        Projectile.height = 30;

        Projectile.friendly = true;

        Projectile.DamageType = DamageClass.Magic;

        Projectile.timeLeft = 6 * EmptySet.Frame;
        Projectile.alpha = 255;

        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
    }

    public override bool? CanHitNPC(NPC target) => Projectile.timeLeft < 300;
    public override void AI()
    {
        float step = 16;
        //Projectile.position = new Vector2(Projectile.position.X, Projectile.position.Y + (isUp ? ok : -ok));

        if (Projectile.timeLeft > 300)
        {
            Projectile.alpha -= Main.rand.Next(1, 5);
        }
        else if (Projectile.timeLeft == 300)
        {
            Projectile.alpha = 0;
        }
        //渐变显色阶段
        else
        {
            //变化完后进行攻击
            if (isGetTarget)
            {
                if (ok > step) return;
                Projectile.velocity = vel/ step * ok;
                ok++;
            }
            //索敌后的变化
            else if (isChanging)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch).noGravity=true;
                ok++;
                if (ok == 45)
                {
                    isChanging = false;
                    isGetTarget = true;
                    ok = 1;
                    vel = target.position - Projectile.position;
                    vel = vel.SafeNormalize(Vector2.One);
                    vel *= 8f;
                    Projectile.rotation = vel.ToRotation()+MathHelper.PiOver2;
                }
            }
            //上下移动寻敌
            else
            {
                ok++;
                if (ok == 30)
                {
                    isUp = !isUp;
                    ok = 0;
                    Projectile.velocity += new Vector2(0, (isUp ? 3 : -3));
                }

                Projectile.velocity *= 0.87f;
                var npcs = Main.npc.Where((x) => 
                    Vector2.Distance(x.position, Projectile.position) < 400 &&
                    x.friendly == false &&
                    x.active 
                );
                var npc = npcs.OrderBy(x => Vector2.Distance(x.position, Projectile.position)).ToList();
                if (npc.Count > 0)
                {
                    ok = 0;
                    target = npc[0];
                    isChanging = true;
                }

            }
        }
    }

}
