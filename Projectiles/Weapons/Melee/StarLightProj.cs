using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Weapons.Melee;

/// <summary>
/// 星华弹幕
/// </summary>
public class StarLightProj : ModProjectile
{
    int i;
    float Accelerate = 5f;
    private Texture2D tex;
    private Vector2[] oldPosi = new Vector2[8];
    private Vector2[] oldVec = new Vector2[8];
    public override void SetDefaults()
    {
        Projectile.width = (int)(24 * Projectile.scale); //已精确测量
        Projectile.height = (int)(50 * Projectile.scale);
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = (int)(5.5 * EmptySet.Frame);
        //Projectile.Center = new Vector2(-25, 25);

        Projectile.tileCollide = false;
        tex = ModContent.Request<Texture2D>("EmptySet/Projectiles/Weapons/Melee/StarLightProj").Value;
    }

    //public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    //{
    //    if (crit) 
    //        target.AddBuff(BuffID.OnFire, 5 * EmptySet.Frame);
    //}

    public override void AI()
    {
        //Dust.NewDust(,,,DustID.RainbowTorch)
        //Projectile.rotation += 0.04f * Projectile.velocity.Length();
        
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch);//Direct
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.OrangeTorch);
        //Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch);
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(0f);
        if (Accelerate <= 18f)
        {
            Accelerate += 0.3f;
        }
        Projectile.velocity = Vector2.Normalize(Projectile.velocity) * Accelerate;
        if (Main.time % 2 == 0)    //每两帧记录一次（打一次点）
        {
            for (int i = oldVec.Length - 1; i > 0; i--) //你应该知道为什么这里要写int i = oldVec.Length - 1
            {
                oldPosi[i] = oldPosi[i - 1];
            }
            oldPosi[0] = Projectile.Center;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Player Player = Main.player[Projectile.owner];
        /*
        target.AddBuff(ModContent.BuffType<MonsterHeaven>(), 300);
        Player.AddBuff(3, 300);
        Player.AddBuff(3, 300);
        Player.AddBuff(3, 300);
        Player.moveSpeed = 1.6f;
        Player.accRunSpeed = 1.3f;
        */
        for (int i = 0; i < 30; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.height, Projectile.height, DustID.RedTorch);//Direct
            Dust.NewDust(Projectile.position, Projectile.height, Projectile.height, DustID.OrangeTorch);
        }
        target.AddBuff(BuffID.OnFire, 180);
        base.OnHitNPC(target, hit, damageDone);
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (int i = 0; i < 30; i++)
        {
            Dust.NewDust(Projectile.position, Projectile.height, Projectile.height, DustID.RedTorch);//Direct
            Dust.NewDust(Projectile.position, Projectile.height, Projectile.height, DustID.OrangeTorch);
        }
        return base.OnTileCollide(oldVelocity);
    }
    public override void PostDraw(Color lightColor)
    {
        for (int i = oldPosi.Length - 1; i > 0; i--)
        {
            if (oldPosi[i] != Vector2.Zero)
            {
                Main.spriteBatch.Draw(tex, oldPosi[i] - Main.screenPosition, null, Color.White * 1 * (0.6f - .2f * i), (oldPosi[i - 1] - oldPosi[i]).ToRotation(), tex.Size() * .5f, 1 * (1 - .05f * i), SpriteEffects.None, 0);  //如果贴图不在你所期望的方向上，你应该知道你要做什么
                                                                                                                                                                                                                                                              //试试修改这里的.05f与.02f，想想它们意味着什么
            }
        }

        base.PostDraw(lightColor);
    }
}

