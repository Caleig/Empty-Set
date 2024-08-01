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

namespace EmptySet.Projectiles.Weapons.Melee
{
    public class Charging : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.Size = new(50);
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.timeLeft = 300;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Dust dust = Dust.NewDustDirect((Main.player[Projectile.owner].position + new Vector2(9, 0)), 16, 15, DustID.YellowStarDust);
            dust.velocity.Y = -10;
            if (player.HeldItem.type == ModContent.ItemType<Items.Weapons.Melee.HeroGiantSword>() && Main.mouseRight)
            {
                Projectile.localAI[0]++;
                Projectile.timeLeft = 2;
                player.itemTime = 2;
                player.itemAnimation = 2;
                
            }
            if(Projectile.localAI[0] == 300)
            {
                player.AddBuff(ModContent.BuffType<Buffs.Lightning>(), 180);
                Projectile.localAI[0] = 0;
                Projectile.Kill();
            }
            base.AI();
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