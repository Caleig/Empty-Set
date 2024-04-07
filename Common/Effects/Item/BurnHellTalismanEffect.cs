using Microsoft.CodeAnalysis.CSharp.Syntax;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class BurnHellTalismanEffect : ModPlayer
{
    public bool IsEnabled { get; private set; } = false;
    public void Enable() => IsEnabled = true;

    public override void OnHitNPCWithItem(Terraria.Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (IsEnabled && (item.DamageType == DamageClass.Throwing || item.DamageType == DamageClass.SummonMeleeSpeed))
            target.AddBuff(BuffID.OnFire, 180);
            base.OnHitNPCWithItem(item, target, hit, damageDone);
    }

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (IsEnabled && (proj.DamageType == DamageClass.Throwing || proj.DamageType == DamageClass.SummonMeleeSpeed))
            target.AddBuff(BuffID.OnFire, 180);
    }

    public override void ResetEffects()
    {
        if(IsEnabled) IsEnabled = false;
    }
}