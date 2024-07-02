using EmptySet.Items.Accessories;
using EmptySet.Projectiles.Whip;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Projectile
{
    public class FlameFangEffect : GlobalProjectile
    {
        public override void ModifyHitNPC(Terraria.Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Main.myPlayer];
            if (projectile.aiStyle == 165 && Main.rand.Next(1, 11) <= 3 && player.armor.Any(x => x.type == ModContent.ItemType<FlameFang>()))
            {
                Terraria.Projectile projectile2 = Terraria.Projectile.NewProjectileDirect(projectile.GetSource_FromAI(), target.position, Vector2.Zero,
                    ModContent.ProjectileType<Explosion>(), projectile.damage, 1f, projectile.owner);
                projectile2.DamageType = DamageClass.Summon;
                projectile2.width = projectile.height = (int)(80 * projectile.scale);
                projectile2.usesLocalNPCImmunity = false;
                projectile2.timeLeft = 5;
                projectile2.Center = new Vector2(target.Hitbox.X, target.Hitbox.Y);
            }
            base.ModifyHitNPC(projectile, target, ref modifiers);
        }
    }
}
