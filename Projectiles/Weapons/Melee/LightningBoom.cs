using EmptySet.Extensions;
using Microsoft.Build.Construction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EmptySet.Projectiles.Weapons.Melee
{
	public class LightningBoom : ModProjectile
	{
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.tileCollide = false;

            Projectile.timeLeft = 20;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Dust dust = Dust.NewDustPerfect(Projectile.Center, DustID.WitherLightning, Vector2.Zero, 0, Color.White, 1);
                dust.noGravity = true;
            }
            
        }
    }
}