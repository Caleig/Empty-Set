using Microsoft.Xna.Framework;
using EmptySet.Items.Consumables;
using EmptySet.Items.Placeable;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EmptySet.Tiles;

public class FireworkLauncherTile:ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;


        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        //TileObjectData.newTile.Height = 3;
        //TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
        TileObjectData.addTile(Type);

        LocalizedText name = CreateMapEntryName();
        // name.SetDefault("Firework Launcher");

        AddMapEntry(new Color(200, 200, 200), name);
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Firework1>();
    }
    public override bool RightClick(int i, int j)
    {
        if (Main.LocalPlayer.HeldItem.type==ModContent.ItemType<Firework1>())
        {
            if (TelescopePlayer.launcherFirework == TelescopePlayer.STOP)
            {
                Main.NewText("准备发射");
                Main.LocalPlayer.HeldItem.stack -= 1;
                TelescopePlayer.launcherFirework = TelescopePlayer.PREPARE;
                TelescopePlayer.launcherFireworkX = i;
                TelescopePlayer.launcherFireworkY = j;
            }
            else 
            {
                Main.NewText("还有烟花正在发射呢，快去用望远镜欣赏吧！");
            }
        }
        return base.RightClick(i, j);
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<FireworkLauncher>());
    }
}