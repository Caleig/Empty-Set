using EmptySet.Biomes;
using EmptySet.NPCs.Enemy;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Systems;

//邪恶投矛相关功能类（请转移）
//修改npc相关属性
public class EmptySetNPC : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public int num = 0;
    public bool flag = false;

    public override void ResetEffects(NPC npc)
    {
        base.ResetEffects(npc);
        num = 0;
        flag = false;
    }

    //修改npc生成
    public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
        //混沌核心npc生成
        if (spawnInfo.Player.InModBiome<TheCoreOfChaosBiome>()) 
        {
            pool.Clear();
            if (Main.hardMode == true) 
            {
                pool.Add(ModContent.NPCType<AbyssGazer>(), 0.1f);//渊视者
                pool.Add(ModContent.NPCType<DeepGumPolymer>(), 0.1f);//渊胶聚合体
                pool.Add(NPCID.CorruptSlime, 0.15f);//恶翅史莱姆
                pool.Add(NPCID.FloatyGross, 0.15f);//恶心浮游怪
                pool.Add(NPCID.IlluminantBat, 0.15f);//夜明蝙蝠
            }
        }
    }
}