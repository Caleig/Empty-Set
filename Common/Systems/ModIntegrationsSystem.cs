using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EmptySet.Items.Accessories;
using EmptySet.Items.Consumables;
using EmptySet.Items.Materials;
using EmptySet.Items.Weapons.Magic;
using EmptySet.Items.Weapons.Melee;
using EmptySet.Items.Weapons.Ranged;
using EmptySet.NPCs.Boss.EarthShaker;
using EmptySet.NPCs.Boss.FrozenCore;
using EmptySet.NPCs.Boss.JungleHunter;
using EmptySet.NPCs.Enemy;
using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace EmptySet.Common.Systems
{
    internal class ModIntegrationsSystem:ModSystem
    {
		public override void PostSetupContent()
		{
			// Most often, mods require you to use the PostSetupContent hook to call their methods. This guarantees various data is initialized and set up properly
			// Census Mod allows us to add spawn information to the town NPCs UI:
			// https://forums.terraria.org/index.php?threads/.74786/
			DoCensusIntegration();

			// Boss Checklist shows comprehensive information about bosses in its own UI. We can customize it:
			// https://forums.terraria.org/index.php?threads/.50668/
			DoBossChecklistIntegration();

			// We can integrate with other mods here by following the same pattern. Some modders may prefer a ModSystem for each mod they integrate with, or some other design.
		}

		private void DoCensusIntegration()
		{
			// We figured out how to add support by looking at it's Call method: https://github.com/JavidPack/Census/blob/1.4/Census.cs
			// Census also has a wiki, where the Call methods are better explained: https://github.com/JavidPack/Census/wiki/Support-using-Mod-Call

			if (!ModLoader.TryGetMod("Census", out Mod censusMod))
			{
				// TryGetMod returns false if the mod is not currently loaded, so if this is the case, we just return early
				return;
			}

			// The "TownNPCCondition" method allows us to write out the spawn condition (which is coded via CanTownNPCSpawn), it requires an NPC type and a message
			//int npcType = ModContent.NPCType<Content.NPCs.ExamplePerson>();

			// The message makes use of chat tags to make the item appear directly, making it more fancy
			//string message = $"Have either an Example Item [i:{ModContent.ItemType<Content.Items.ExampleItem>()}] or an Example Block [i:{ModContent.ItemType<Content.Items.Placeable.ExampleBlock>()}] in your inventory";

			// Finally, call the desired method
			//censusMod.Call("TownNPCCondition", npcType, message);

			// Additional calls can be made here for other Town NPCs in our mod
		}

		private void DoBossChecklistIntegration()
		{
			// The mods homepage links to its own wiki where the calls are explained: https://github.com/JavidPack/BossChecklist/wiki/Support-using-Mod-Call
			// If we navigate the wiki, we can find the "AddBoss" method, which we want in this case
			if (!ModLoader.TryGetMod("BossChecklist", out Mod bossChecklistMod))
			{
				return;
			}

			// For some messages, mods might not have them at release, so we need to verify when the last iteration of the method variation was first added to the mod, in this case 1.3.1
			// Usually mods either provide that information themselves in some way, or it's found on the github through commit history/blame
			if (bossChecklistMod.Version < new Version(1, 3, 1))
			{
				return;
			}

			// The "AddBoss" method requires many parameters, defined separately below:

			// The name used for the title of the page
			//string bossName = "大地震撼者";

			// The NPC type of the boss
			//int bossType = ModContent.NPCType<EarthShaker>();

			// Value inferred from boss progression, see the wiki for details
			//float weight = 1.7f;

			// Used for tracking checklist progress
			//Func<bool> downed = () => DownedBossSystem.downedEarthShaker;

			// If the boss should show up on the checklist in the first place and when (here, always)
			//Func<bool> available = () => true;

			// "collectibles" like relic, trophy, mask, pet
			/*List<int> collection = new List<int>()
			{
				ModContent.ItemType<DefenderTalisman>(),
				ModContent.ItemType<MetalFragment>(),
				ModContent.ItemType<ChargedCrystal>(),
				ModContent.ItemType<EarthShakerChest>()

			};*/

			// The item used to summon the boss with (if available)
			//int summonItem = ModContent.ItemType<DustyRemoteControl>();

			// Information for the player so he knows how to encounter the boss
			//string spawnInfo = $"在地表任意时间使用 [i:{summonItem}] 召唤";

			// The boss does not have a custom despawn message, so we omit it
			//string despawnInfo = null;

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				"大地震撼者",
				ModContent.NPCType<EarthShaker>(),
				1.7f,
				() => DownedBossSystem.downedEarthShaker,
				() => true,
				new List<int>()
				{
					ModContent.ItemType<DefenderTalisman>(),
					ModContent.ItemType<MetalFragment>(),
					ModContent.ItemType<ChargedCrystal>(),
					ModContent.ItemType<EarthShakerChest>()

				},
				ModContent.ItemType<DustyRemoteControl>(),
				$"在地表任意时间使用 [i:{ModContent.ItemType<DustyRemoteControl>()}] 召唤",
				null,
				null
			);

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				"丛林游猎者",
				ModContent.NPCType<JungleHunterHead>(),
				3f,
				() => DownedBossSystem.downedJungleHunter,
				() => true,
				new List<int>()
				{
					ModContent.ItemType<DesertedTomahawk>(),
					ModContent.ItemType<FangsNecklace>(),
					ModContent.ItemType<ForestNecklace>()
				},
				ModContent.ItemType<HoneyBait>(),
				$"在丛林使用 [i:{ModContent.ItemType<HoneyBait>()}] 召唤",
				null,
				(SpriteBatch sb, Rectangle rect, Color color) => {
					Texture2D texture = ModContent.Request<Texture2D>("EmptySet/NPCs/Boss/JungleHunter/JungleHunter_Bestiary").Value;
					Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2), rect.Y + 70);
					sb.Draw(texture, centered, null, color, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0);
				}
			);

			bossChecklistMod.Call(
				"AddBoss",
				Mod,
				"极川之核",
				ModContent.NPCType<FrozenCore>(),
				7.5f,
				() => DownedBossSystem.downedFrozenCore,
				() => true,
				new List<int>()
				{
				},
				null,
				$"在暴风雪中击杀 [c/3333FF:极川灵] 召唤",
				null,
				null
			);
		}
	}
}
