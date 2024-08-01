using Microsoft.Xna.Framework;
using EmptySet.Dusts;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Buffs;

public class Lightning : ModBuff
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("LeavesPoisoning debuff");
        // Description.SetDefault("Losing life");
        Main.debuff[Type] = true;
        Main.pvpBuff[Type] = true;
        Main.buffNoSave[Type] = true; // 使这个增益在退出和重新加入世界时不持续
        //BuffID.Sets.LongerExpertDebuff[Type] = true; // 如果这个增益是debuff，设置为true将使这个增益在专家模式下持续时间增加一倍
    }
    // 允许你让这个buff给给定的玩家特定的效果
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed(DamageClass.Melee) += 3;
    }
}