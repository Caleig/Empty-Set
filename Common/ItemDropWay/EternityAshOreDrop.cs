using EmptySet.Extensions;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class EternityAshOreDrop : GlobalNPC
{
    private static readonly int[] DropNpcList =
    {
        //肉前地牢小怪
        //NPCID.AngryBones, //愤怒骷髅怪
        //NPCID.ShortBones, //矮骷髅怪
        //NPCID.BigBoned, //大骷髅怪
        //NPCID.AngryBonesBig, //大愤怒骷髅怪
        //NPCID.AngryBonesBigMuscle, //大块头愤怒骷髅怪
        //NPCID.AngryBonesBigHelmet, //大头盔愤怒骷髅怪
        //NPCID.DungeonSlime, //地牢史莱姆
        //NPCID.CursedSkull, //诅咒骷髅头
        //NPCID.DarkCaster, //暗黑法师

        NPCID.AngryBones, //愤怒骷髅怪
        NPCID.BlueArmoredBones, //蓝装甲骷髅
        NPCID.BlueArmoredBonesMace,
        NPCID.BlueArmoredBonesNoPants,
        NPCID.BlueArmoredBonesSword,
        NPCID.RustyArmoredBonesAxe, //生锈装甲骷髅
        NPCID.RustyArmoredBonesFlail,
        NPCID.RustyArmoredBonesSword,
        NPCID.RustyArmoredBonesSwordNoArmor,
        NPCID.HellArmoredBones, //地狱装甲骷髅
        NPCID.HellArmoredBonesSpikeShield,
        NPCID.HellArmoredBonesMace,
        NPCID.HellArmoredBonesSword,
        NPCID.Paladin, //圣骑士
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) => npcLoot.AddManualLoot(DropNpcList, npc,
        ItemDrop.GetItemDropRule<EternityAshOre>(44, 5, 9));
}