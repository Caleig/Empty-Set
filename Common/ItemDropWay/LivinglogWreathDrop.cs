using EmptySet.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.ItemDropWay;

public class LivinglogWreathDrop : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        if(shop.NpcType == NPCID.WitchDoctor)
        {
            shop.Add(new Item(ModContent.ItemType<LivinglogWreath>()) {
                        shopCustomPrice = Item.buyPrice(0, 85, 0, 0),
                        shopSpecialCurrency = CustomCurrencyID.DefenderMedals // omit this line if shopCustomPrice should be in regular coins.
                    });
        }
        base.ModifyShop(shop);
    }
}