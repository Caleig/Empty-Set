using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using EmptySet.Extensions;
using System.Diagnostics.Metrics;
using Microsoft.CodeAnalysis;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace EmptySet.Projectiles.Melee;

/// <summary>
/// Ó°Çòµ¯Ä»(×ó²à)
/// </summary>
public class arbitration2 : ModProjectile
{
    Vector2[] vec = new Vector2[1];
    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 130;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 22;
        Projectile.scale = 1.5f;
        Projectile.alpha = 0;
    }
    public override void AI()
    {
        Projectile.alpha += 10;
        if (Main.time % 3 == 0)
        {

            vec[0] = Projectile.Center;
        }
        float num8 = Projectile.rotation + Main.rand.NextFloatDirection() * ((float)Math.PI / 2f) * 0.7f;
        Vector2 vector2 = Projectile.Center + num8.ToRotationVector2() * 84f * Projectile.scale;
        Vector2 vector3 = (num8 + Projectile.ai[0] * ((float)Math.PI / 2f)).ToRotationVector2();
        if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
        {
            Dust dust2 = Dust.NewDustPerfect(Projectile.Center + num8.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale), 278, vector3 * 1f, 100, Color.Lerp(Color.Gold, Color.White, Main.rand.NextFloat() * 0.3f), 0.4f);
            dust2.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
            dust2.noGravity = true;
        }
        if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
        {
            Dust.NewDustPerfect(vector2, 43, vector3 * 1f, 100, Color.DarkRed * Projectile.Opacity, 1.2f * Projectile.Opacity);
        }
        Player player = Main.player[Projectile.owner];
        Projectile.Center = player.Center + new Vector2(player.width, player.height / 2);
        Projectile.rotation = player.itemRotation - (MathHelper.PiOver4 / 2);
        base.AI();
    }
}
