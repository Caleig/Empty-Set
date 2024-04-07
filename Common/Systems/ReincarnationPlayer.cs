using Microsoft.Xna.Framework;
using EmptySet.Items.Accessories;
using EmptySet.Items.Consumables;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Buffs;
namespace EmptySet.Common.Systems
{
    public class EmptySetPlayer : ModPlayer
    {

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

        public override void ResetEffects()
        {
            base.ResetEffects();
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Main.myPlayer];
            if (player.armor.Any(x => x.type == ModContent.ItemType<Items.Accessories.ScrletBracer>()) && hit.Crit)
            {
                float damage = hit.Damage * 0.75f;
                Projectile.NewProjectile(null, player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 10, ModContent.ProjectileType<Projectiles.Throwing.ScarletSpike>(), (int)damage, 3, player.whoAmI);
            }
            else if (player.armor.Any(x => x.type == ModContent.ItemType<Items.Accessories.DemonBracer>()) &&hit.DamageType == DamageClass.Throwing)
            {
                bool i = Main.rand.NextBool();
                if(i)
                {
                    float damage = hit.Damage * 0.5f;
                    Projectile.NewProjectile(Entity.GetSource_FromAI(), player.Center, Vector2.Normalize(Main.MouseWorld - player.Center) * 11, ModContent.ProjectileType<Projectiles.Throwing.DemonSpike>(), (int)damage, 3, player.whoAmI);
                }
            }
            else if(player.armor.Any(x => x.type == ModContent.ItemType<Items.Accessories.BeingsWoodenBracer>()) && hit.Crit && hit.DamageType == DamageClass.Throwing)
            {
        
                Projectile.NewProjectile(null, player.Center, new Vector2(0, 0), ModContent.ProjectileType<Projectiles.Buffs.SingtheWoods>(), 0, 8, player.whoAmI);
            }
            if (player.armor.Any(x => x.type == ModContent.ItemType<FlameFang>()) && hit.DamageType == DamageClass.Throwing)
            {
                target.AddBuff(BuffID.OnFire, 300);
            }
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
