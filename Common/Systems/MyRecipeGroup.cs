using EmptySet.Items.Weapons.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.Systems;

public class MyRecipeGroup : ModSystem
{
    //private static MyGroup _recipeGroups
    //public static RecipeGroup this[string name] => GetRecipeGroup(name);

    //public static RecipeGroup Get(string name)
    //{
    //    return _recipeGroups.First(x => x.recipeGroupName == name).recipeGroup;
    //}
    public static RecipeGroup Get(MyRecipeGroupId id)
    {
        return RecipeGroups[(int) id].rg;
    }

    private static (string name, RecipeGroup rg)[] RecipeGroups =
    {
        ("CopperOrTin", new(() => "铜锭或者锡锭", ItemID.CopperBar, ItemID.TinBar)),
        ("CopperAnvil", new(() => "铁砧或者铅砧", ItemID.IronAnvil, ItemID.LeadAnvil)),
        ("EvilBar", new(() => "任何邪恶金属锭", ItemID.CrimtaneBar, ItemID.DemoniteBar)),
        ("GoldBroadsword", new(() => "任何金阔剑", ItemID.GoldBroadsword, ItemID.PlatinumBroadsword)),
        ("EvilFlyingBlade", new(() => "任何邪恶飞刃", ModContent.ItemType<CrimtaneFlyingBlade>())),
        ("EvilBow", new(() => "任何邪恶弓", ItemID.TendonBow, ItemID.DemonBow)), //肌健弓 恶魔弓
        ("EvilThrowingSpear", new(() => "任何邪恶投矛", ModContent.ItemType<CorruptionThrowingSpear>(), ModContent.ItemType<CrimtaneThrowingSpear>())),
        ("TitaniumOrAdamantite",new(()=>"钛金锭或精金锭", ItemID.TitaniumBar, ItemID.AdamantiteBar)),
        ("SliverOrTungsten",new(()=>"银锭或钨锭", ItemID.SilverBar, ItemID.TungstenBar)),
        ("EvilGun",new(()=>"火枪或夺命枪", ItemID.Musket, ItemID.TheUndertaker)),
        ("IronOrLead",new (()=>"铁/铅锭", ItemID.IronBar,ItemID.LeadBar)),
        ("CopperOrTinBow",new (()=>"铜/锡弓", ItemID.CopperBow,ItemID.TinBow)),
        ("CopperOrBroadSword",new (()=>"铜/锡阔剑", ItemID.CopperBroadsword,ItemID.TinBroadsword))
    };

    public override void Unload()
    {
        for (int i = 0; i < RecipeGroups.Length; i++)
            RecipeGroups[i].rg = null;
    }

    public override void AddRecipeGroups()
    {
        foreach (var item in RecipeGroups)
            RecipeGroup.RegisterGroup($"{nameof(EmptySet)}:{item.name}", item.rg);

    }

    //public static int CreateRecipeGroup<T>(string groupName, params int[] itemIds) =>
    //    RecipeGroup.RegisterGroup($"{nameof(T)}:{groupName}",
    //        new(() => Language.GetTextValue("LegacyMisc.37") + $" {groupName}", itemIds));
}
public enum MyRecipeGroupId
{
    CopperOrTin,
    CopperAnvil,
    EvilBar,
    GoldBroadsword,
    EvilFlyingBlade,
    EvilBow,
    EvilThrowingSpear,
    TitaniumOrAdamantite,
    SliverOrTungsten,
    EvilGun,
    IronOrLead,
    CopperOrTinBow,
    CopperOrBroadSword
}
