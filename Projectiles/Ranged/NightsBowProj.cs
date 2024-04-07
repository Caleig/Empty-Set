using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Utils;

namespace EmptySet.Projectiles.Ranged
{

    /// <summary>
    /// 永夜弓控制弹幕
    /// </summary>
    public class NightsBowProj : ModProjectile
    {
        private int MaxTimer
        {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = value; }
        }
        private int Timer
        {
            get { return (int)Projectile.ai[0]; }
            set { Projectile.ai[0] = value; }
        }
        public override string Texture => "Terraria/Images/Projectile_0";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("永夜弓");
        }
        public override void SetDefaults()
        {
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }
        public override bool? CanHitNPC(NPC target) => false;
        public override bool CanHitPlayer(Player target) => false;
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.velocity = Vector2.Zero;
            Projectile.Center = owner.Center;
            if (owner.channel && owner.whoAmI == Main.myPlayer)
            {
                Timer++;
                Projectile.timeLeft = 2;

                // 从玩家到达鼠标位置的单位向量
                Vector2 unit = Vector2.Normalize(Main.MouseWorld - owner.Center);
                // 随机角度
                float rotaion = unit.ToRotation();
                // 调整玩家转向以及手持物品的转动方向
                owner.direction = Main.MouseWorld.X < owner.Center.X ? -1 : 1;
                owner.itemRotation = (float)Math.Atan2(rotaion.ToRotationVector2().Y * owner.direction,
                    rotaion.ToRotationVector2().X * owner.direction);
                // 玩家保持物品使用动画
                owner.itemTime = 2;
                owner.itemAnimation = 2;

                if (Timer >= MaxTimer)
                {
                    if (!owner.HasAmmo(owner.HeldItem))
                    {
                        Projectile.Kill();
                        return;
                    }
                    int projType = 0;
                    float shootSpeed = owner.HeldItem.shootSpeed;
                    int arrowDamage = (int)(Projectile.damage * owner.ActualClassDamage(DamageClass.Ranged));
                    float arrowKnock = Projectile.knockBack;
                    bool canShoot = true;
                    owner.PickAmmo(owner.HeldItem, out projType, out shootSpeed, out arrowDamage, out arrowKnock, out int temp);
                    Timer = 0;
                    if (MaxTimer > 15)
                    {
                        MaxTimer -= MaxTimer / 5;
                        if (MaxTimer <= 15)
                            for (int i = 0; i < 40; i++)
                                Dust.NewDustPerfect(owner.Center, DustID.Shadowflame, Vector2.UnitX.RotatedBy(MathHelper.TwoPi / 40 * i) * 10f, 100, Scale: 2).noGravity = true;
                    }
                    Vector2 startPos = Main.rand.NextVector2Circular(60, 60) + owner.Center;
                    Vector2 vecToMse = Main.MouseWorld - owner.Center;
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), startPos, startPos.SafeNormalize(Vector2.UnitY), ModContent.ProjectileType<NightsBowArrow>(), arrowDamage, arrowKnock, Projectile.owner, vecToMse.X, vecToMse.Y).localAI[1] = 200 / MaxTimer;
                    SoundEngine.PlaySound(SoundID.Item5, owner.position);
                }
            }
            else Projectile.Kill();
        }
    }
}
