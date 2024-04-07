using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Item;

public class BloodlettingKnifeEffect : ModPlayer
{
    public int UseTimer { get;  set; } = 0;
    //override life
    //禁用生命恢复，但可以在使用治疗药水后的10秒内免除此效果
    public override void PreUpdate()
    {
        if (UseTimer != 0) UseTimer--;
    }
}

public class BloodlettingKnifeEffect2 : GlobalItem
{
    //public override bool InstancePerEntity => true;
    private static int[] Heals =
    {
        ItemID.HealingPotion,
        ItemID.LesserHealingPotion,
        ItemID.GreaterHealingPotion,
        ItemID.SuperHealingPotion
    };
    public override bool? UseItem(Terraria.Item item, Player player)
    {
        if (Heals.Any(x => x == item.type))
            player.GetModPlayer<BloodlettingKnifeEffect>().UseTimer = 10 * EmptySet.Frame;
        return base.UseItem(item,player);
    }
}