using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EmptySet.Dusts
{
    public class Magic1 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            base.OnSpawn(dust);
            dust.noLight = false;
            dust.scale = 1;
        }
    }
}