using Terraria.Graphics.Effects;
using EmptySet.Common.Systems;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using EmptySet.Items.Consumables;
using EmptySet.Skies;

namespace EmptySet.Biomes
{
    internal class TheCoreOfChaosBiome : ModBiome
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.BiomeHigh;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/Like A Piece Of Ice（pass");

        //public override string BestiaryIcon => base.BestiaryIcon;

        //public override string BackgroundPath => base.BackgroundPath;

        //public override string MapBackground => base.MapBackground;

        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.Find<ModSurfaceBackgroundStyle>("EmptySet/TheCoreOfChaosBackgroundStyle");

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.Find<ModUndergroundBackgroundStyle>("EmptySet/TheCoreOfChaosUndergroundBackgroundStyle");

        //public override ModWaterStyle WaterStyle => base.WaterStyle;

        //public override Color? BackgroundColor => base.BackgroundColor;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("混沌核心");
        }

        public override bool IsBiomeActive(Player player)
        {
            return ModContent.GetInstance<EmptySetTileCount>().缭乱砾岩TileCount >= 200;
        }

        public override void OnEnter(Player player)
        {
            base.OnEnter(player);
        }

        public override void OnInBiome(Player player)
        {
            base.OnInBiome(player);
        }

        public override void OnLeave(Player player)
        {
            base.OnLeave(player);
        }


        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (!isActive)
            {
                if (Filters.Scene["EmptySet:TheCoreOfChaosScreen"].IsActive()) Filters.Scene.Deactivate("EmptySet:TheCoreOfChaosScreen");
                //if (SkyManager.Instance["TheCoreOfChaosSky"].IsActive()) SkyManager.Instance.Deactivate("TheCoreOfChaosSky");
                //EmptySet.bloomActive = false;
            }
            else 
            {
                EmptySet.Instance.theCoreOfChaosScreenShaderData.mode = player.HeldItem.type == ModContent.ItemType<LavaShimmerLamp>() ? 1 : 0;
                if (!Filters.Scene["EmptySet:TheCoreOfChaosScreen"].IsActive()) Filters.Scene.Activate("EmptySet:TheCoreOfChaosScreen");
                //if (!SkyManager.Instance["TheCoreOfChaosSky"].IsActive()) SkyManager.Instance.Activate("TheCoreOfChaosSky");
                //EmptySet.bloomActive = true;
            }
        }
    }
}
