using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Whip;
using EmptySet.Items.Accessories;
using EmptySet.Buffs;

namespace EmptySet.Common.Effects.Item
{
    public class FlameFangEffect : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Main.myPlayer];
            if(projectile.aiStyle == 165 && Main.rand.Next(1, 11) <= 3 && player.armor.Any(x => x.type == ModContent.ItemType<FlameFang>()))
            {
                Projectile projectile2 = Projectile.NewProjectileDirect(projectile.GetSource_FromAI(), target.position, Vector2.Zero,
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
