using Microsoft.Xna.Framework;
using EmptySet.Extensions;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Magic;

/// <summary>
/// 逐光邪奏曲弹幕
/// </summary>
public class HolyTuneProj : ModProjectile
{
    private int whirlpool = 0;
    private int book = 0;
    private int timer = 0;
    int musicNotes = 6;
    //public override string Texture => "Terraria/Images/Projectile_0";

    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 8; //帧图1
        
        //Main.proj(Projectile.type, new DrawAnimationVertical(3, 8));
        // DisplayName.SetDefault("HolyTune");
    }
    public override void SetDefaults()
    {
        Projectile.width = 62;
        Projectile.height = 50;
        //Projectile.aiStyle = 1; 
        Projectile.friendly = true; 
        Projectile.hostile = false; 
        Projectile.DamageType = DamageClass.Magic; 
        Projectile.penetrate = -1; 
        Projectile.timeLeft = 2;
        //Projectile.alpha = 0; 
        //Projectile.light = 0; 
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        //Projectile.frame = 8;
        //Projectile.frameCounter=0
        //
    }
    public override void AI()
    {
        Projectile.FrameControl();
        var owner = Main.player[Projectile.owner];
        Projectile.velocity = Vector2.Zero;
        Projectile.Center = owner.Center + new Vector2(0, -100);
        Vector2 pos = owner.Center + new Vector2(0, -100);

        if (owner.channel && owner.statMana >= 17)
        {
            //if (!Main.projectile[whirlpool].active || Main.projectile[whirlpool].type != ModContent.ProjectileType<PursuitOfLightWhirlpool>())
            //{
            //    whirlpool = Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, Vector2.Zero, ModContent.ProjectileType<PursuitOfLightWhirlpool>(), 0, 0, Projectile.owner);
            //}

            //Main.projectile[whirlpool].timeLeft = 10;
            //Main.projectile[whirlpool].Center = pos;
            //Main.projectile[book].timeLeft = 10;
            //Main.projectile[book].Center = pos;
            Projectile.timeLeft = 10;

            timer++;

            // 从玩家到达鼠标位置的单位向量
            Vector2 unit = Vector2.Normalize(Main.MouseWorld - owner.Center);
            // 随机角度
            float rotaion = unit.ToRotation();
            // 调整玩家转向以及手持物品的转动方向
            owner.direction = Main.MouseWorld.X < owner.Center.X ? -1 : 1;
            owner.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * owner.direction, rotaion.ToRotationVector2().X * owner.direction);
            owner.itemTime = 2;
            owner.itemAnimation = 2;


            if (timer >= owner.HeldItem.useTime)
            {
                timer = 0;
                Vector2 velocity = (Main.MouseWorld - pos).SafeNormalize(Vector2.Zero) * 7;
                //Vector2 v1 = velocity.RotatedBy(MathHelper.ToRadians(35));
                //Vector2 v2 = velocity.RotatedBy(MathHelper.ToRadians(-35));

                //Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, velocity, Main.rand.Next(76, 79), Projectile.damage, Projectile.knockBack, Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, v1, Main.rand.Next(76, 79), Projectile.damage, Projectile.knockBack, Projectile.owner);
                //Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, v2, Main.rand.Next(76, 79), Projectile.damage, Projectile.knockBack, Projectile.owner);

                var _int = Main.rand.Next(4) switch
                {
                    0 => Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, velocity * 1.5f,ModContent.ProjectileType<RuneSmall>(), Projectile.damage/2, Projectile.knockBack, Projectile.owner),
                    1 => Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, velocity , ModContent.ProjectileType<RuneMiddle1>(), Projectile.damage , Projectile.knockBack, Projectile.owner),
                    2 => Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, velocity , ModContent.ProjectileType<RuneMiddle2>(), Projectile.damage, Projectile.knockBack, Projectile.owner),
                    _ => Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, velocity * 0.75f, ModContent.ProjectileType<RuneBig>(), Projectile.damage*2, Projectile.knockBack, Projectile.owner)
                };


                owner.statMana -= 17;

                float num = (float)Main.mouseX + Main.screenPosition.X - owner.position.X;
                float num12 = (float)Main.mouseY + Main.screenPosition.Y - owner.position.Y;
                float num16 = (float)Math.Sqrt(num * num + num12 * num12);
                float num17 = (float)Main.screenHeight / Main.GameViewMatrix.Zoom.Y;
                num16 /= num17 / 2f;
                if (num16 > 1f)
                {
                    num16 = 1f;
                }
                num16 = num16 * 2f - 1f;
                if (num16 < -1f)
                {
                    num16 = -1f;
                }
                if (num16 > 1f)
                {
                    num16 = 1f;
                }
                num16 = (float)Math.Round(num16 * (float)musicNotes);
                num16 = (Main.musicPitch = num16 / (float)musicNotes);
                SoundStyle type = SoundID.Item26;
                SoundEngine.PlaySound(type, owner.position);
                //如果有知道playnote变成了什么的程序，记得把这个改成那个东西，这里只是用58代替而已
            }
        }

        if (!owner.channel || owner.dead)
        {
            Projectile.Kill();
        }
    }
}
public abstract class Rune : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 28;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = 3 + 1;
        Projectile.timeLeft = 7 *EmptySet.Frame;
    }

    public override void AI() => Projectile.DirectionalityRotation(0.35f);
}
/// <summary>
/// 传入伤害需*2
/// </summary>
public class RuneBig : Rune
{
    public override void SetDefaults()
    {
        base.SetDefaults();
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.Next(10) < 3)
        {
            Main.player[Projectile.owner].HealEffect(5);
            Main.player[Projectile.owner].statLife += 5;
        }
    }
    public override void AI()
    {
        base.AI();
    }
}
public class RuneMiddle1 : Rune
{
    public override void SetDefaults()
    {
        base.SetDefaults();
    }
}
public class RuneMiddle2 : Rune
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.width = 24;
        Projectile.height = 24;
    }
}
/// <summary>
/// 传入伤害需/2 射速调块 加特效
/// </summary>
public class RuneSmall : Rune
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.penetrate = 5 + 1;

    }
}