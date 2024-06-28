using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using System;
using Terraria.GameContent;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Terraria.DataStructures;
using EmptySet.Common.Systems;
using FighterSickle;
using Mono.Cecil;

namespace EmptySet.Projectiles.Weapons.Melee
{
    public class StarLightProj2: ModProjectile
    {
        public SwingHelper swingHelper;
        public Player player;
        public bool InUse;
        public override string Texture => "EmptySet/Items/Weapons/Melee/StarLight";
        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.Size = new(72);
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            if (player == null)
            {
                player = Main.player[Projectile.owner];
                swingHelper = new(Projectile, 8);
            }
            if (player.HeldItem.type != ModContent.ItemType<Items.Weapons.Melee.StarLight>())
            {
                Projectile.Kill();
                return;
            }
            Lighting.AddLight(Projectile.Center, 1, 1, 1);
            Projectile.timeLeft = 2;
            if (player.controlUseItem)
            {
                InUse = true;
            }
            if (InUse)
            {
                Projectile.hide = false;

                swingHelper.ProjFixedPlayerCenter(player, 0, true);
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center);
                switch ((int)Projectile.ai[0])
                {
                    
                    case 0:
                        {
                            swingHelper.Change(-Vector2.UnitY.RotatedBy(-0.8), new Vector2(1, 0.7f), 0.2f);
                            Projectile.ai[1] += 0.3f;
                            
                            
                            if (Projectile.ai[1] > MathHelper.Pi + MathHelper.PiOver2)
                            {
                                Projectile.ai[0]++;
                                Projectile.ai[1] = 0;
                                Projectile.hide = true;
                                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, velocity * 15, ModContent.ProjectileType<StarLightProj>(), 26, 5, Projectile.owner);
                                InUse = false;
                               
                                return;
                            }
                            break;
                        }
                    case 1:
                        {
                            Projectile.hide = false;
                            swingHelper.Change_Lerp(-Vector2.UnitX, 0.1f, new(1, 0.4f), 1f, 0.7f, 0.5f);
                                Projectile.ai[0]++;
                            break;
                        }
                    case 2:
                        {
                            Projectile.ai[1] += 0.3f;
                           
                            if (Projectile.ai[1] > MathHelper.Pi * 3)
                            {
                                Projectile.ai[0]++;
                                Projectile.ai[1] = 0;
                                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, velocity * 15, ModContent.ProjectileType<StarLightProj>(), 26, 5, Projectile.owner);
                                InUse = false;
                                return;
                            }
                            break;
                        }
                    case 3:
                        {
                            swingHelper.Change(Vector2.UnitY.RotatedBy(-0.2), new Vector2(1, 0.7f), 0f);
                            Projectile.ai[1] -= 0.3f;
                           
                            if (Projectile.ai[1] < -MathHelper.Pi - MathHelper.PiOver2)
                            {
                                Projectile.ai[0]++;
                                Projectile.ai[1] = 0;
                                Projectile.NewProjectile(Entity.GetSource_FromAI(), Projectile.Center, velocity * 15, ModContent.ProjectileType<StarLightProj>(), 26, 5, Projectile.owner);
                                Projectile.Kill();
                                InUse = false;
                                return;
                            }
                            break;
                        }
                    default:
                        Projectile.Kill();
                        InUse = false;
                        Projectile.ai[0] = Projectile.ai[1] = 0;
                        break;
                }
                swingHelper.SwingAI(140, player.direction, Projectile.ai[1]);
            }
            else
            {
                Projectile.ai[0] = Projectile.ai[1] = 0;
                swingHelper.ProjFixedPlayerCenter(player);
                Projectile.hide = true;
                Projectile.position.X -= player.width * player.direction;
                Projectile.rotation = Vector2.UnitY.RotatedBy(-0.2 * player.direction).ToRotation();
            }
        }
        public override bool? CanDamage() => swingHelper != null;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float r = 0;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, Projectile.Center + Projectile.velocity,
                Projectile.width, ref r);
        }
        public override bool ShouldUpdatePosition() => false;
        public override bool PreDraw(ref Color lightColor)
        {
            if (swingHelper != null && InUse)
            {
                swingHelper.Swing_Draw(lightColor);
                swingHelper.Swing_TrailingDraw(TextureAssets.Extra[201].Value, (f) =>
                {
                    Color color = Color.Lerp(Color.Red, Color.OrangeRed, f);
                    color.A = 0;
                    return color;
                });
                return false;
            }
            return base.PreDraw(ref lightColor);
        }
    }
}

