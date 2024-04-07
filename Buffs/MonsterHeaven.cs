using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using EmptySet.Dusts;
using EmptySet.Extensions;
using EmptySet.Items.Weapons.Throwing;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace EmptySet.Buffs
{
    public class MonsterHeaven: ModBuff
    {
        float Speed;
        //天堂制造
        //给怪物做的buff，用于减慢怪物速度
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            base.SetStaticDefaults();
        }
        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            
            return base.ReApply(npc, time, buffIndex);
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            if(npc.velocity.X < -2f)
            {
                npc.velocity *= 0.9f;
            }
            else if(npc.velocity.X > 2f)
            {
                npc.velocity *= 0.9f;
            }
            else
            {
                return;
            }
            base.Update(npc, ref buffIndex);
        }
    }
}