using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using EmptySet.Common.Systems;
using EmptySet.Projectiles.Held;
using EmptySet.Utils;
using Mono.Cecil;
using EmptySet.Projectiles.Melee;
using EmptySet.Extensions;
using Terraria.Audio;

namespace EmptySet.Projectiles.Weapons.Melee
{
    internal class HeroGiantSword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 28;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;

        }

        Player player;
        //弹幕一共要旋转多少度
        private float TotalRadians = MathHelper.ToRadians(250);
        private int time = 30;
        //这次弹幕的垂直缩放量
        private float VerticalScaling = 1;
        //调整弹幕到玩家中心的距离
        private float Pro2PlrDis = 75;
        //玩家到鼠标的向量，基准向量
        private Vector2 Mouse2Player = Vector2.UnitX;
        //玩家到鼠标的向量鱼x轴的角度
        private float Mouse2PlayerRadi = 0;
        //弹幕在玩家的左侧还是右侧,1向右，-1向左
        private int Pdir = 1;
        //初始向量
        private Vector2 startVector;
        //每次更新角度后的初始向量
        private Vector2 UpdatedVector;

        private Vector2 v1 = Vector2.Zero;
        private Vector2 v2 = Vector2.Zero;
        private float Rot = 0;
        private Effect ef2;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int rand = Main.rand.Next(1, 5);
            if (rand == 1)
            {
                target.velocity = Vector2.Zero;
            }
            base.OnHitNPC(target, hit, damageDone);
        }
        public override void AI()
        {
            Projectile.localAI[0]++;

            player = Main.player[Projectile.owner];
            if (Projectile.localAI[1] == 0)
            {
                time = Projectile.timeLeft;
                Mouse2Player = Main.MouseWorld - player.Center;
                Mouse2PlayerRadi = (float)Math.Atan2(Mouse2Player.Y, Mouse2Player.X);
                Pdir = Math.Sign(Main.mouseX - player.Center.X + Main.screenPosition.X);
                if (Pdir == -1)
                {
                    startVector = Vector2.UnitX.RotatedBy(-(MathHelper.Pi - TotalRadians / 2)) * Pro2PlrDis;
                }
                else
                {
                    startVector = Vector2.UnitX.RotatedBy(-TotalRadians / 2) * Pro2PlrDis;
                }
                VerticalScaling = Main.rand.NextFloat(1f, 1f);
                Projectile.localAI[1] = 1;
            }
            if (Pdir == -1)
            {
                Projectile.spriteDirection = -1;
                UpdatedVector = startVector.RotatedBy(-TotalRadians / time * Projectile.localAI[0]);
                v1 = new Vector2(UpdatedVector.X, UpdatedVector.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi + MathHelper.Pi) - new Vector2(Projectile.width / 2, Projectile.height / 2);
                v2 = Projectile.Center - player.Center;
                Rot = (float)(Math.Atan2(v2.Y, v2.X) + MathHelper.Pi * 3 / 4);
            }
            else
            {
                UpdatedVector = startVector.RotatedBy(TotalRadians / time * Projectile.localAI[0]);
                v1 = new Vector2(UpdatedVector.X, UpdatedVector.Y * VerticalScaling).RotatedBy(Mouse2PlayerRadi) - new Vector2(Projectile.width / 2, Projectile.height / 2);
                v2 = Projectile.Center - player.Center;
                Rot = (float)(Math.Atan2(v2.Y, v2.X) + MathHelper.PiOver4);
            }
            Projectile.position = player.Center + v1;
            Projectile.rotation = Rot;
            Projectile.velocity = v2.RotatedBy(MathHelper.PiOver2) / v2.Length();
            player.headRotation = Rot;
            player.headVelocity = Projectile.velocity;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(player.HasBuff(ModContent.BuffType<Buffs.Lightning>()))
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), (target.Center + new Vector2(0, -700)), Vector2.Zero,
                ModContent.ProjectileType<Projectiles.Weapons.Melee.Lightning>(), 90, 0, Projectile.owner);
                Projectile projectile = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.Center, Vector2.Zero,
                    ModContent.ProjectileType<Projectiles.Magic.Explosion4>(), Projectile.damage, 1f, Projectile.owner);
                projectile.DamageType = DamageClass.Magic;
                projectile.width = projectile.height = (int)(120 * Projectile.scale);
                projectile.usesLocalNPCImmunity = false;
                projectile.timeLeft = 5;
                projectile.Center = target.Center;
                for (int i = 0; i < 50; i++)
                {
                    Dust.NewDust((target.position - new Vector2(60, 60)), 120, 120, DustID.YellowStarDust);
                }
                SoundEngine.PlaySound(SoundID.Item122);

            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
