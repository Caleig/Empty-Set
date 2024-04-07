using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Flails;

/// <summary>
/// 深谙连枷
/// </summary>
public class CorruptionFlailsProj : ModProjectile
{
    private int tick = 0;
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("深谙连枷");
    }

    public override void SetDefaults()
    {
        Projectile.width = 34;
        Projectile.height = 34;
        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.DamageType = DamageClass.Melee;

        Projectile.netImportant = true;
        Projectile.aiStyle = 15;
        Projectile.usesLocalNPCImmunity = true;
        Projectile.localNPCHitCooldown = 10;
    }
    public override void AI()
    {
        Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleTorch);
        if (!Main.player[Projectile.owner].channel)
        {
            //Main.player[Projectile.owner]
            if (tick >= 10)
            {
                tick = 0;
                var npcs = Main.npc.Where((x) =>
                    Vector2.Distance(x.position, Projectile.position) < 450 &&
                    x.friendly == false &&
                    x.active
                ).OrderBy(x => Vector2.Distance(x.position, Projectile.position)).ToList();
                if (npcs.Count > 0)
                {
                    var vel = npcs[0].position - Projectile.position;
                    vel = vel.SafeNormalize(Vector2.One);
                    vel *= 10f;

                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, vel,
                        ModContent.ProjectileType<DarkCrystal>(), Projectile.damage, Projectile.knockBack,
                        Projectile.owner);
                    //Projectile.rotation = vel.ToRotation() + MathHelper.PiOver2;
                }
            }
            tick++;
        }
        else
            tick = 0;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
            ////Vector2 offset = new Vector2();
            //double angle = Main.rand.NextDouble() * 2d * Math.PI;
            ////offset.X += (float)(Math.Sin(angle) * dist);
            ////offset.Y += (float)(Math.Cos(angle) * dist);

            ////Vector2 position = target.Center + offset - new Vector2(4, 4);
            ////Vector2 velocity = Vector2.Normalize(target.Center - Projectile.position).RotatedBy(angle) * 25;
            //var vel =Vector2.One.RotatedBy(angle) * 20;

            //Projectile.NewProjectile(Projectile.GetSource_FromAI(), target.Center, vel, ModContent.ProjectileType<DarkCrystal>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
    }

    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("EmptySet/Projectiles/Flails/CorruptionFlailsProj_Chain").Value;
        Vector2 position = Projectile.Center;
        Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
        Rectangle? sourceRectangle = new Rectangle?();
        Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
        float num1 = texture.Height;
        Vector2 vector24 = mountedCenter - position;
        float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
        bool flag = true;
        if (float.IsNaN(position.X) && float.IsNaN(position.Y))
            flag = false;
        if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
            flag = false;
        while (flag)
            if (vector24.Length() < num1 + 1.0)
            {
                flag = false;
            }
            else
            {
                Vector2 vector21 = vector24;
                vector21.Normalize();
                position += vector21 * num1;
                vector24 = mountedCenter - position;
                Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16));
                color2 = Projectile.GetAlpha(color2);
                Main.EntitySpriteDraw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0);
            }
        return true;
    }
}