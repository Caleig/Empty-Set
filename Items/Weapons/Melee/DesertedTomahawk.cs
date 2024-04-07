using EmptySet.Utils;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Weapons.Melee;

/// <summary>
/// 荒芜战斧
/// </summary>
public class DesertedTomahawk : ModItem
{
    public override void SetStaticDefaults()
    {

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 62; // The item texture's width.
        Item.height = 62; // The item texture's height.
        Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
        Item.useTime = UseSpeedLevel.VeryFastSpeed;
        Item.useAnimation = UseSpeedLevel.VeryFastSpeed; // 武器使用动画的时间跨度，建议与useTime设置相同。
        Item.autoReuse = true; // 自动挥舞
        Item.DamageType = DamageClass.Melee; // 伤害类型
        Item.damage = 21;
        Item.knockBack = KnockBackLevel.Normal; // 武器的击退力。最大的是20
        Item.crit = 4; // 武器的致命一击几率。默认情况下，玩家有4%的暴击几率。
        Item.value = Item.sellPrice(0,0,3,0);
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.axe = 35;//武器有多少斧力，注意游戏中显示的斧力值乘以5
        Item.tileBoost += 3; //即+3范围
    }
    
}