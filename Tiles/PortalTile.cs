using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.Items.Placeable;
using EmptySet.UI;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EmptySet.Tiles
{
    internal class PortalTile:ModTile
    {
        public override void SetStaticDefaults()
        {
            TileID.Sets.HasOutlines[Type] = true;

            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();

            // name.SetDefault("Telescope");
            AddMapEntry(new Color(0, 255, 255), name);
            
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Portal>();
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return true;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<Portal>());
        }

        public override bool RightClick(int i, int j)
        {
            PortalUI.Visible = false;
            for (int a = 0; a < EmptySetWorld.PortalList.Count; a++)
            {
                if (EmptySetWorld.PortalList[a].x - i <= 1 && EmptySetWorld.PortalList[a].x - i >= 0 && EmptySetWorld.PortalList[a].y - j <= 2 && EmptySetWorld.PortalList[a].y - j >= 0) 
                {
                    PortalUI.addItem(a);
                    PortalUI.Visible = true;
                    break;
                }
            }
            return base.RightClick(i, j);
        }

        public override void PlaceInWorld(int i, int j, Item item)
        {
            PortalUI.Visible = false;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                PortalTag portal = new PortalTag(EmptySetWorld.PortalList.Count, i, j, "传送门" + EmptySetWorld.PortalList.Count);
                EmptySetWorld.PortalList.Add(portal);
            }
            else 
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((int)EmptySetMessageType.PlacePortal);
                packet.Write((int)i);
                packet.Write((int)j);
                packet.Send(-1, -1);
            }
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            PortalUI.Visible = false;
            for (int a = 0; a < EmptySetWorld.PortalList.Count; a++)
            {
                if (EmptySetWorld.PortalList[a].x == i && EmptySetWorld.PortalList[a].y == j) 
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        EmptySetWorld.PortalList.RemoveAt(a);
                    }
                    else
                    {
                        ModPacket packet = Mod.GetPacket();
                        packet.Write((int)EmptySetMessageType.KillPortal);
                        packet.Write((int)a);
                        packet.Send(-1, -1);
                    }
                    break;
                }
            }
        }
    }
}
