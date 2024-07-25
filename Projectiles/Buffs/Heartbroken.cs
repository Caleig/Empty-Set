using Terraria;
using Terraria.ModLoader;
namespace EmptySet.Projectiles.Buffs
{
    public class Heartbroken: ModBuff
    {
        public override void SetStaticDefaults()
        {
            // 设置buff名字和描述
            // DisplayName.SetDefault("碎心");
            // Description.SetDefault("适得其反，最大生命值减少");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.pvpBuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 = player.statLifeMax2/10*8;//减少20%最大生命
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            base.Update(npc, ref buffIndex);
        }
    }
}