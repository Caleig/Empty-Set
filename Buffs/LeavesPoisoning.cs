using Microsoft.Xna.Framework;
using EmptySet.Dusts;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Buffs;

public class LeavesPoisoning : ModBuff
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
        player.GetModPlayer<LeavesPoisoningPlayer>().leavesPoisoningDebuff = true;
    }
}
public class LeavesPoisoningPlayer : ModPlayer
{
    /// <summary>
    /// 指示debuff是否被激活
    /// </summary>
    internal bool leavesPoisoningDebuff;

    public override void ResetEffects()
    {
        leavesPoisoningDebuff = false;
    }

    // 这通常是通过设置玩家来实现的。如果它是正的，lifeRegen为0，设置玩家。时间为0，并从player.lifeRegen中减去一个数字
    // 玩家受到伤害的速度是你每秒减去的数字的一半
    public override void UpdateBadLifeRegen()
    {
        if (leavesPoisoningDebuff)
        {
            // 将任何生命再生归零。
            if (Player.lifeRegen > 0)
                Player.lifeRegen = 0;
            // Player.lifeRegenTime 用于提高玩家达到其最大自然生命恢复的速度
            // 所以我们把它设为0，当这个debuff处于活动状态时，它永远不会到达它
            Player.lifeRegenTime = 0;
            // lifeegen的单位是1/2生命每秒。因此，这个效果每秒会导致8点生命的丧失
            Player.lifeRegen -= 16;

        }
    }
    public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
    {
        if (leavesPoisoningDebuff)
        {
            if (Main.rand.NextBool(10))
            {
                int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, ModContent.DustType<LeavesPoisoningDust>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
                Main.dust[dust].noGravity = true;
                //Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;

                drawInfo.DustCache.Add(dust);
            }

        }
    }
}