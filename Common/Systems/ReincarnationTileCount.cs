using EmptySet.Tiles;
using System;
using Terraria.ModLoader;

namespace EmptySet.Common.Systems
{
    internal class EmptySetTileCount : ModSystem
    {
        public int 缭乱砾岩TileCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            缭乱砾岩TileCount = tileCounts[ModContent.TileType<缭乱砾岩Tile>()];
        }
    }
}
