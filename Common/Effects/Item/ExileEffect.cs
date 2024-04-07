using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class ExileEffect : ModPlayer
{
    internal const int DashCooldown = 75;// Time (frames) between starting dashes. If this is shorter than DashDuration you can start a new dash before an old one has finished
    internal const int DashDuration = 50;// Duration of the dash afterimage effect in frames
    internal const float DashVelocity = 10.5f;

    internal bool dashAccessoryEquipped;
    internal int dashDelay = 0; // frames remaining till we can dash again
    internal int dashTimer = 0;// frames remaining in the dash


    public override void PreUpdateMovement()
    {
        if (CanUseDash && dashDelay == 0 && Player.dashDelay == 0)
        {
            SoundEngine.PlaySound(SoundID.Item71, Main.LocalPlayer.position);
            int p = Projectile.NewProjectile(Player.GetSource_ItemUse(Player.HeldItem), Player.position, new Vector2(0, 0), ModContent.ProjectileType<ExilesDashProjectile>(), 0, 0, Player.whoAmI);
            Main.projectile[p].ai[0] = Player.whoAmI;
            Main.projectile[p].ai[1] = Player.direction;

            Player.dashDelay = DashCooldown;
            Vector2 newVelocity = new(0,0);
            newVelocity.X = Player.direction * DashVelocity;
            dashDelay = DashCooldown;
            dashTimer = DashDuration;
            Player.velocity = newVelocity;
        }

        if (dashDelay > 0)
            dashDelay--;

        if (dashTimer > 0)
        {
            //Player.eocDash = DashTimer;
            Player.armorEffectDrawShadowEOCShield = true;
            dashTimer--;
        }
        if (dashTimer==0) 
        {
            dashAccessoryEquipped = false;
        }
    }

    public override bool CanHitNPC(NPC target)
    {
        if(dashTimer > 0)
            return false;
        return true;
    }

    private bool CanUseDash => dashAccessoryEquipped && !Player.mount.Active;
}