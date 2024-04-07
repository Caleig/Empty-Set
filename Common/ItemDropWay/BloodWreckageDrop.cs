using EmptySet.Extensions;
using EmptySet.Items.Materials;
using EmptySet.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class BloodWreckageDrop : GlobalNPC
{
    private static readonly int[] Skeletons =
    {
        NPCID.Skeleton, //骷髅
        NPCID.SmallSkeleton, //小骷髅
        NPCID.BigSkeleton, //大骷髅
        NPCID.HeadacheSkeleton, //头痛骷髅
        NPCID.SmallHeadacheSkeleton, //小头痛骷髅
        NPCID.BigHeadacheSkeleton, //大头痛骷髅
        NPCID.MisassembledSkeleton, //畸形骷髅
        NPCID.SmallMisassembledSkeleton, //小畸形骷髅
        NPCID.BigMisassembledSkeleton, //大畸形骷髅
        NPCID.PantlessSkeleton, //无裤骷髅
        NPCID.SmallPantlessSkeleton, //小无裤骷髅
        NPCID.BigPantlessSkeleton, //大无裤骷髅
        //halloween only
        NPCID.SkeletonTopHat, //高顶礼帽骷髅
        NPCID.SkeletonAstonaut, //宇航员骷髅
        NPCID.SkeletonAlien, //异星骷髅
    };

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (Main.hardMode)
        {
            npcLoot.AddManualLoot(Skeletons, npc,
                ItemDrop.GetItemDropRule<BloodWreckage>(35, 1, 2));
        }
       
    }
}