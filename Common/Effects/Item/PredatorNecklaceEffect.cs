using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class PredatorNecklaceEffect : ModPlayer
{
    public Terraria.Item Accessory { get; private set; }

    public bool IsEnabled { get; private set; } = false;

    public void Enabled(Terraria.Item item) 
    {
        if(!IsEnabled) IsEnabled = true;
        Accessory = item;
    }

    public override void ResetEffects()
    {
        if(IsEnabled) IsEnabled = false;
    }

    public override void PostHurt(Player.HurtInfo info)
    {
        if (IsEnabled) 
        {
            int num = Main.rand.Next(2, 6);
            for (int i = 0; i < num; i++)
            {
                var beeVelocity = new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(MathHelper.ToRadians(180), MathHelper.ToRadians(360)));
                Projectile.NewProjectile(Player.GetSource_Accessory(Accessory),Player.position, beeVelocity, ProjectileID.Bee,10,0,Player.whoAmI);
            }
            Player.AddBuff(BuffID.Honey,1800);
        }
    }
}