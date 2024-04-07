using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Tiles
{
    internal class 缭乱砾岩Tile:ModTile
    {
        public override void SetStaticDefaults()
        {

            Main.tileLavaDeath[Type] = true;
            TileID.Sets.Ore[Type] = false;
            Main.tileSpelunker[Type] = false; // 瓷砖是否会受到洞穴探险者高光的影响
            //Main.tileOreFinderPriority[Type] = 0; // 金属探测器的价值,https://terraria.gamepedia.com/Metal_Detector
            Main.tileShine2[Type] = true; // 略微修改绘制颜色。会有黑暗中发光的小光点
            Main.tileShine[Type] = 5000; // 这瓷砖上的小灰尘。越大越少
            Main.tileMergeDirt[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = false;

            LocalizedText name = CreateMapEntryName();

            // name.SetDefault("缭乱砾岩");//英文
            AddMapEntry(new Color(13, 22, 154), name);

            DustType = 59;
            RegisterItemDrop(ModContent.ItemType<Items.Placeable.缭乱砾岩>());
            HitSound = SoundID.Tink;
            MinPick = 190;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}
