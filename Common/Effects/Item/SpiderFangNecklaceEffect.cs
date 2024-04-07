using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class SpiderFangNecklaceEffect : ModPlayer
{
    public bool IsEnabled { get; private set; } = false;
    public void Enable() => IsEnabled = true;
    private int duration = 7 * EmptySet.Frame;
    public override void OnHitNPCWithItem(Terraria.Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (IsEnabled && item.DamageType == DamageClass.Throwing && Main.rand.NextBool(7))
            target.AddBuff(BuffID.Venom, duration);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (IsEnabled && proj.DamageType == DamageClass.Throwing && Main.rand.NextBool(7))
            target.AddBuff(BuffID.Venom, duration);
    }

    public override void ResetEffects()
    {
        if (IsEnabled) IsEnabled = false;
    }
}