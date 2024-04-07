using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace EmptySet.Common.ItemDropRules.RoamingUAV;

class IronDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return WorldGen.SavedOreTiers.Iron == 6;
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