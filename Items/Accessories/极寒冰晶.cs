using System;
using Microsoft.Xna.Framework;
using EmptySet.Common.Effects.Item;
using EmptySet.Projectiles.NoType;
using EmptySet.Projectiles.Summon.LavaHunterProj;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Items.Accessories;

/// <summary>
/// 极寒冰晶
/// </summary>
public class 极寒冰晶 : ModItem
{
    private int[] projs = new int[3];
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Ulcer Pupil")
        //Tooltip.SetDefault("Increases armor penetration by 10\n" +
        //                  "\"This thing is disgusting.\"");

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 38;
        Item.rare = ItemRarityID.Pink;
        Item.value = Item.sellPrice(0, 3, 0, 0);
        Item.accessory = true;
    }

    private long timer = 0;
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        for (int i = 0; i < 3; i++)
        {
            timer++;
            int type = ModContent.ProjectileType<极寒冰晶Proj>();//ProjectileID.FrostBoltStaff;

            if (projs[i] == 0 || !Main.projectile[projs[i]].active || Main.projectile[projs[i]].type != type)
            {
                
                projs[i] = Projectile.NewProjectile(player.GetSource_Accessory(Item), player.position, Vector2.Zero, type, default, default, player.whoAmI);
                
                //projarr[i].velocity = Vector2.Zero;// 要把弹幕速度归零，否则圆会有一个位移
                
            }
            else
            {
                var t = timer * 0.065f;
                var t2 = Main.time;
                Main.projectile[projs[i]].Center = player.Center + new Vector2((float)Math.Cos(t + (i + 1) * 2f), (float)Math.Sin(t + (i + 1) * 2f)) * 80f; // 半径r = 130，以玩家中心为圆心  //1 一排 2 三等分 3 2等分
                
            }
            Main.projectile[projs[i]].timeLeft = 2;
        }
    }
}