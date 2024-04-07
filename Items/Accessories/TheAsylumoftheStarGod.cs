using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;

namespace EmptySet.Items.Accessories
{
    public class TheAsylumoftheStarGod : ModItem
    {
        int time = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("the Asy Lum of the Star God");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Main.hardMode)
            {
                player.statDefense += 3;
            }
            else
            {
                player.statDefense += 1;
            }
            if (time > 1500)
            {
                player.AddBuff(ModContent.BuffType<Buffs.TheAsylumoftheStarGod>(), 300);
                time = 0;
            }
            time++;
            
            base.UpdateAccessory(player, hideVisual);
        }
    }
}