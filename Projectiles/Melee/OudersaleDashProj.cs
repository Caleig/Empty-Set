using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// 血镰冲刺弹幕
/// </summary>
public class OudersaleDashProj : ModProjectile
{
    //public override string Texture => "EmptySet/Projectiles/Melee/OudersaleProj";
    private Player player => Main.player[Projectile.owner];

    private int Timer
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }

    private bool Execute { get; set; } = false;

    public override void SetDefaults()
    {
        Projectile.width = 102;
        Projectile.height = 88;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.alpha = 255;
        Projectile.timeLeft = (int)(7 * EmptySet.Frame);
    }
    public override void AI()
    {
        var pos = player.position + new Vector2(-44, 120);

        if (Timer == 0)
        {
            Projectile.spriteDirection = -player.direction;
        }
        Timer++;
        if (player.dead)
        {
            Projectile.timeLeft = 1;
        }
        if (Main.mouseRight)
        {
            //Projectile.spriteDirection = player.velocity.X > 0 ? -1 : player.velocity.X > 0 ? Projectile.spriteDirection : 1;
            
            if (player.velocity.X > 0 && player.velocity.Length() > 0.01f)
                Projectile.spriteDirection = -1;
            if (player.velocity.X < 0 && player.velocity.Length() > 0.01f)
                Projectile.spriteDirection = 1;

            Projectile.velocity = Vector2.Zero;
            Projectile.position = pos;
            Projectile.DirectionalityRotationSet(MathHelper.ToRadians(+227f));
            Projectile.alpha-= Main.rand.Next(1,3);
            if (Timer >= 180)
            {
                Projectile.alpha = 0;
                Dust.NewDustDirect(player.position, player.width, player.height, DustID.RedTorch).noGravity = true;
            }
        }
        else if(!Execute)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            Execute = true;
        }

        if (Execute)
        {//MathHelper.ToRadians(+227f)
            //var v = new Vector2(-44, 120);
            //v=v.Length().

            //Projectile.velocity = player.position+ (new Vector2(10,0).ToRotation() +MathHelper.ToRadians(Timer)).ToRotationVector2()*6f;
            //Projectile.DirectionalityRotation(0.03f);
            //写转向
            Projectile.Kill();
        }



        //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        //Projectile.DirectionalityRotation(0.3f);

        //Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
        //    DustID.RedTorch).noGravity = true;
        //if (Projectile.timeLeft > 5 * EmptySet.Frame)
        //{

        //}
        //else
        //{
        //    Projectile.velocity = Vector2.Zero;
        //    Projectile.alpha += 1;
        //}

    }
    public override bool ShouldUpdatePosition()
    {
        //if (Execute)
        //{
        //    Projectile.position =
        //    Projectile.DirectionalityRotationSet()
        //}
        return false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        //if (crit)
        //{
        //    int range = 500;
        //    var posList = new Vector2[]
        //    {
        //        new(range, 0),
        //        new(-range, 0),
        //        new(0, range),
        //        new(0, -range),
        //    };

        //    foreach (var v2 in posList)
        //    {
        //        Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.position + v2,
        //            -v2.SafeNormalize(Vector2.One) * 8f, ModContent.ProjectileType<BloodSickleProj>(), Projectile.damage,
        //            Projectile.knockBack, Projectile.owner, 1);
        //    }
        //}
    }
}