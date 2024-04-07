using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EmptySet.Utils
{
    public static class ReUtils
    {
        public static float ActualClassDamage(this Player player, DamageClass damageClass)
            => (float)player.GetDamage(DamageClass.Generic).Additive + (float)player.GetDamage(damageClass).Additive - 1f;
    }
}