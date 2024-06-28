using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace EmptySet.Buffs
{
    public class Speedfaster : ModBuff
    {
        int time = 0;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("身法");
            // Description.SetDefault("是纯度极高的武艺！速度提高了！");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed = 1.6f;
            if(time % 3 == 0)
            {
                for(int i =0; i < 5; i ++)
                {
                    Dust dust = Dust.NewDustDirect(player.Center, 20, 20, DustID.Cloud);
                }
            }
            time++;
        }
    }
}