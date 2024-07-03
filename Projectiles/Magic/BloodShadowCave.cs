using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.Extensions;

namespace EmptySet.Projectiles.Magic;
/// <summary>
/// 血影空洞(血影空洞杖弹幕)
/// </summary>
public class BloodShadowCave : ModProjectile
{
    private int Timer { get; set; } = 0;

    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Blood Shadow Cave");
    }

    public override void SetDefaults()
    {
        Projectile.width = 56;
        Projectile.height = 56;
        Projectile.timeLeft = 30 * EmptySet.Frame;
        Projectile.light = 0.25f;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        //Projectile.scale = 1
        //Projectile.Center = new Vector2(Projectile.Center.X + 17, Projectile.Center.Y + 17);
    }
    
    public override void AI()
    {
        Projectile.DirectionalityRotation(0.2f);
        Timer++;
        if (Timer == 15)
        {
            var p = Projectile.position;
            var x = p.X;//- Projectile.width / 2f;
            var y = p.Y;// - Projectile.height / 2f;
            var pos = new Vector2(x+Main.rand.Next(6,44+1), y+ Main.rand.Next(34 + 1));
            Projectile.NewProjectile(Projectile.GetSource_FromAI(), pos, Projectile.velocity,
                ModContent.ProjectileType<BloodShadowTear>(), Projectile.damage, Projectile.knockBack,
                Projectile.owner);
            Timer = 0;
        }
    }
    //public override bool? CanHitNPC(NPC target) => false;
}
