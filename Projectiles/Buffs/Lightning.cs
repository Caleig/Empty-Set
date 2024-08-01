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
        Main.buffNoSave[Type] = true; // ʹ����������˳������¼�������ʱ������
        //BuffID.Sets.LongerExpertDebuff[Type] = true; // ������������debuff������Ϊtrue��ʹ���������ר��ģʽ�³���ʱ������һ��
    }
    // �����������buff������������ض���Ч��
    public override void Update(Player player, ref int buffIndex)
    {
        player.GetAttackSpeed(DamageClass.Melee) += 3;
    }
}