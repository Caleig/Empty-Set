using Terraria.ModLoader;

namespace EmptySet.Biomes
{
	public class TheCoreOfChaosUndergroundBackgroundStyle : ModUndergroundBackgroundStyle
	{
		public override void FillTextureArray(int[] textureSlots) {
			textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot("EmptySet/Assets/Textures/Backgrounds/ExampleBiomeUnderground0");
			textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot("EmptySet/Assets/Textures/Backgrounds/ExampleBiomeUnderground1");
			textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot("EmptySet/Assets/Textures/Backgrounds/ExampleBiomeUnderground2");
			textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot("EmptySet/Assets/Textures/Backgrounds/ExampleBiomeUnderground3");
		}
	}
}