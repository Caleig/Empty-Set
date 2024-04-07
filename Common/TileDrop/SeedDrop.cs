using System.Linq;
using Microsoft.Xna.Framework;
using EmptySet.Items.Weapons.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Common.TileDrop;

public class SeedDrop : GlobalTile
{
    int[] tileType = {
        TileID.Plants,
        TileID.Plants2,
        TileID.CorruptPlants,
        TileID.MushroomPlants,
        TileID.OasisPlants,
        TileID.CrimsonPlants,
        TileID.JunglePlants,
        TileID.JunglePlants2,
        TileID.HallowedPlants,
        TileID.HallowedPlants2,
    };
    public override void Drop(int i, int j, int type)/* tModPorter Suggestion: Use CanDrop to decide if items can drop, use this method to drop additional items. See documentation. */
    {
    }
}