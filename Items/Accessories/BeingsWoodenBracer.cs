using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using EmptySet.Common.Effects.Common;

namespace EmptySet.Items.Accessories
{
   public class BeingsWoodenBracer : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 2, 40, 0);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            var hasIt = player.armor.Any(x => x.type == ModContent.ItemType<WoodenBracer>() || x.type == ModContent.ItemType<MetalBracer>() || x.type == ModContent.ItemType<ScrletBracer>() || x.type == ModContent.ItemType<DemonBracer>());
            var isThis = player.armor[slot].type == ModContent.ItemType<WoodenBracer>() || player.armor[slot].type == ModContent.ItemType<MetalBracer>() || player.armor[slot].type == ModContent.ItemType<ScrletBracer>() || player.armor[slot].type == ModContent.ItemType<DemonBracer>();
            if (hasIt)
            {
                if (isThis)
                    return true;
                return false;
            }
            return true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetCritChance(DamageClass.Throwing) += 4f;
            player.GetModPlayer<UseSpeedEffect>().AddSpeed(DamageClass.Throwing, 0.07f);
        }
        public override void AddRecipes() => CreateRecipe()
        .AddIngredient(75, 2)
        .AddIngredient(ModContent.ItemType<WoodenBracer>())
        .AddIngredient(ItemID.LifeCrystal)
        .AddRecipeGroup(RecipeGroupID.IronBar)
        .AddTile(TileID.Anvils)
        .Register();
    }
}
