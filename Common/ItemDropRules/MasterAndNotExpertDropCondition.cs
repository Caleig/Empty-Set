using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace EmptySet.Common.ItemDropRules;

class MasterAndNotExpertDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.IsMasterMode ? true : false;
    }

    public bool CanShowItemDropInUI()
    {
        return Main.masterMode ? true : false;
    }

    public string GetConditionDescription()
    {
        return "";
    }
}