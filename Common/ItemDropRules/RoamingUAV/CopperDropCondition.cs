using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace EmptySet.Common.ItemDropRules.RoamingUAV;

class CopperDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return WorldGen.SavedOreTiers.Copper == 7;
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