using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace EmptySet.Common.Systems
{
    internal class DownedBossSystem:ModSystem
    {
		public static bool downedEarthShaker = false;
		public static bool downedFrozenCore = false;
		public static bool downedJungleHunter = false;

        public override void OnWorldLoad()
		{
			downedEarthShaker = false;
			downedFrozenCore = false;
			downedJungleHunter = false;
		}

		public override void OnWorldUnload()
		{
			downedEarthShaker = false;
			downedFrozenCore = false;
			downedJungleHunter = false;
		}

		public override void SaveWorldData(TagCompound tag)
		{
			if (downedEarthShaker) tag["downedEarthShaker"] = true;
			if (downedFrozenCore) tag["downedFrozenCore"] = true;
			if (downedJungleHunter) tag["downedJungleHunter"] = true;
		}

		public override void LoadWorldData(TagCompound tag)
		{
			downedEarthShaker = tag.ContainsKey("downedEarthShaker");
			downedFrozenCore = tag.ContainsKey("downedFrozenCore");
			downedJungleHunter = tag.ContainsKey("downedJungleHunter");
		}

		public override void NetSend(BinaryWriter writer)
		{
			// Order of operations is important and has to match that of NetReceive
			var flags = new BitsByte();
			flags[0] = downedEarthShaker;
			flags[1] = downedFrozenCore;
			flags[2] = downedJungleHunter;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			// Order of operations is important and has to match that of NetSend
			BitsByte flags = reader.ReadByte();
			downedEarthShaker = flags[0];
			downedFrozenCore = flags[1];
			downedJungleHunter = flags[2];
		}
	}
}
