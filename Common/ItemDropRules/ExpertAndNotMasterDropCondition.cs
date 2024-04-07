using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace EmptySet.Common.ItemDropRules;

class ExpertAndNotMasterDropCondition : IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info)
    {
        return info.IsExpertMode ? (info.IsMasterMode ? false : true) : false;
    }

    public bool CanShowItemDropInUI()
    {
        return Main.expertMode ? (Main.masterMode ? false : true) : false;
    }

    public string GetConditionDescription()
    {
        return "";
    }
}
