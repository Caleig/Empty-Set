using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Whip;

/// <summary>
/// 凝胶鞭弹幕
/// </summary>
public class GelWhipProj : ModProjectile
{
    public override string Texture => "Terraria/Images/Projectile_0";

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Gel Whip");
        ProjectileID.Sets.IsAWhip[Type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.width = 25;
        Projectile.height = 25;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.SummonMeleeSpeed;
        Projectile.timeLeft = 600;
        Projectile.alpha = 0;
        Projectile.light = 0.5f;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.aiStyle = 165;
        Projectile.ownerHitCheck = true;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = -1;
        Projectile.WhipSettings.Segments = 16;
        Projectile.WhipSettings.RangeMultiplier = 1f;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.damage = (int)(Projectile.damage * 0.67f);
    }
    public override bool? CanCutTiles()
    {
        Projectile.WhipPointsForCollision.Clear();
        Projectile.FillWhipControlPoints(Projectile, Projectile.WhipPointsForCollision);
        Vector2 value = new Vector2((float)Projectile.width * Projectile.scale / 2f, 0f);
        for (int i = 0; i < Projectile.WhipPointsForCollision.Count; i++)
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Terraria.Utils.PlotTileLine(Projectile.WhipPointsForCollision[i] - value, Projectile.WhipPointsForCollision[i] + value, (float)Projectile.height * Projectile.scale, new (DelegateMethods.CutTiles));
        }
        return base.CanCutTiles();
    }
    public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
        Projectile.WhipPointsForCollision.Clear();
        Projectile.FillWhipControlPoints(Projectile, Projectile.WhipPointsForCollision);
        for (int n = 0; n < Projectile.WhipPointsForCollision.Count; n++)
        {
            Point point = Projectile.WhipPointsForCollision[n].ToPoint();
            projHitbox.Location = new Point(point.X - projHitbox.Width / 2, point.Y - projHitbox.Height / 2);
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
        }
        return false;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        List<Vector2> list = new List<Vector2>();
        Projectile.FillWhipControlPoints(Projectile, list);
        Texture2D value = TextureAssets.Projectile[Projectile.type].Value;
        Rectangle rectangle = value.Frame(1, 1, 0, 0, 0, 0);
        Vector2 origin = new Vector2((float)(rectangle.Width / 2f), 2f);
        Vector2 value2 = list[0];
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (i == 0)
            {
                value = ModContent.Request<Texture2D>("EmptySet/Projectiles/Whip/GelWhipProj_handle").Value;
            }
            else if (i == list.Count - 2)
            {
                value = ModContent.Request<Texture2D>("EmptySet/Projectiles/Whip/GelWhipProj_edge").Value;
            }
            else
            {
                value = ModContent.Request<Texture2D>("EmptySet/Projectiles/Whip/GelWhipProj_section").Value;
            }
            Vector2 vector = list[i];
            Vector2 vector2 = list[i + 1] - vector;
            float rotation = vector2.ToRotation() - MathHelper.PiOver2;
            Color color = Lighting.GetColor(value2.ToTileCoordinates());
            Main.spriteBatch.Draw(value, value2 - Main.screenPosition, null, color, rotation, origin, 1, SpriteEffects.None, 0f);
            value2 += vector2;
        }
        return false;
    }
}