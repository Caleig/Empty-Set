using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using FighterSickle;
using Terraria.GameContent;
using rail;
using EmptySet.Projectiles.Melee;
using EmptySet.Common.Effects.Item;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Extensions;
using Terraria.Audio;

namespace EmptySet.Projectiles.Weapons.Melee
{
    public class Charging : ModProjectile
    {
        float d;
        float r = 0;
        float r2;
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.Size = new(0);
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.damage = 0;
            Projectile.timeLeft = 2;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.Center = player.Center;
            Dust dust = Dust.NewDustDirect((Main.player[Projectile.owner].position + new Vector2(9, 0)), 16, 15, DustID.YellowStarDust);
            dust.velocity.Y = -10;
            if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.HeroGiantSword>() && Main.mouseRight)
            {
                Projectile.localAI[0]++;
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                
            }
            if (Projectile.localAI[0] == 300)
            {
                for (int i = 0; i < 50; i++)
                {

                    r += MathHelper.TwoPi / 50;
                    d = 14;
                    Vector2 velocity = new Vector2((float)Math.Cos(r), (float)Math.Sin(r)) * 10f;
                    Dust dust2 = Dust.NewDustPerfect(player.Center, DustID.YellowStarDust, velocity, 0, Color.White, 2f);
                    dust2.noGravity = true;
                }
                player.GetModPlayer<LightningEffect>().Lightning = true;
                //player.AddBuff(ModContent.BuffType<Buffs.Lightning>(), 180);
                Projectile.Kill();
                SoundEngine.PlaySound(SoundID.Item93);

            }
            base.AI();
        }
        public override void OnKill(int timeLeft)
        {
            Projectile.localAI[0] = 0;
            base.OnKill(timeLeft);
        }
        public override void PostDraw(Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("EmptySet/Projectiles/Weapons/Melee/HeroGiantSword").Value, (player.position + new Vector2(-57, -35)) - Main.screenPosition, null, Color.White, -MathHelper.ToRadians(45),
                Vector2.Zero, 1, SpriteEffects.None, 0f);
            base.PostDraw(lightColor);
        }
    }
}