using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace EmptySet.Common.ItemDropRules.DeepGumPolymer;

class ShadowScaleDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return !WorldGen.crimson;
    }

    public bool CanShowItemDropInUI()
    {
        return true;
    }

    public string GetConditionDescription()
    {
        return "";
    }
}