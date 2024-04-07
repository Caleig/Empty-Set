using Microsoft.Xna.Framework;
using EmptySet.Dusts;
using EmptySet.Extensions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EmptySet.Buffs
{
    public class TheAsylumoftheStarGod : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("星神之庇护");
            // Description.SetDefault("增加防御，减免伤害，增加移速");
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            base.SetStaticDefaults();
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (Main.hardMode)
            {
                player.statDefense += 7;
                player.endurance += 7;
                player.moveSpeed += 0.3f;
                player.lifeRegen += 2;
            }
            else
            {
                player.statDefense += 3;
                player.endurance += 3;
                player.moveSpeed += 0.1f;
            }
            base.Update(player, ref buffIndex);
        }
    }
}