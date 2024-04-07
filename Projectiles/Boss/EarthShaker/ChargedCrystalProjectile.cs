
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EmptySet.Projectiles.Boss.EarthShaker
{
    class ChargedCrystalProjectile:ModProjectile
    {
        public int ParentIndex
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("充能晶体");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            //Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            //Projectile.alpha = 255; // 射弹的透明度，255为完全透明。(aiStyle 1快速淡入投射物)如果你没有使用淡入的aiStyle，请确保删除这个。你会奇怪为什么你的投射物是隐形的。
            Projectile.light = 1f; // 发射体周围发射出多少光
            Projectile.ignoreWater = false; // 水会影响弹丸的速度吗?
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = true; // Can the projectile deal damage to the player?
            Projectile.tileCollide = false; // 弹丸能与瓦片碰撞吗?
            Projectile.penetrate = -1; // 这样仆从就不会在与敌人或贴图碰撞时掉落
            Projectile.timeLeft = 2;
        }

        public override void OnKill(int timeLeft)
        {
            Dust dust = Main.dust[Dust.NewDust(Projectile.position, 26, 26, DustID.Electric, 0.0f, -0.8f, 0, new Color(), 2f)];
            Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.position, Projectile.velocity, GoreID.Smoke1);
            SoundEngine.PlaySound(SoundID.Item4, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); //爆炸声
        }
    }
    class ChargedCrystal2Projectile : ModProjectile
    {
        public override string Texture => "EmptySet/Projectiles/Boss/EarthShaker/ChargedCrystalProjectile";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("充能晶体");
        }
        public override void SetDefaults()
        {
            Projectile.width = 26;
            Projectile.height = 26;
            //Projectile.aiStyle = 1; // The ai style of the projectile, please reference the source code of Terraria
            //Projectile.alpha = 255; // 射弹的透明度，255为完全透明。(aiStyle 1快速淡入投射物)如果你没有使用淡入的aiStyle，请确保删除这个。你会奇怪为什么你的投射物是隐形的。
            Projectile.light = 1f; // 发射体周围发射出多少光
            Projectile.ignoreWater = false; // 水会影响弹丸的速度吗?
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.hostile = true; // Can the projectile deal damage to the player?
            Projectile.tileCollide = true; // 弹丸能与瓦片碰撞吗?
            Projectile.penetrate = -1; // 这样仆从就不会在与敌人或贴图碰撞时掉落
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation += 0.3f;
            for (int i = 0; i < 3; i++)
                Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.NorthPole)].noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 3; i++)
                Main.dust[Dust.NewDust(Projectile.position, 26, 26, DustID.NorthPole, 0.0f, -0.8f, 0, new Color(), 2f)].noGravity = true;
            Gore.NewGore(Projectile.GetSource_FromAI(), Projectile.position, Projectile.velocity, GoreID.Smoke1);
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center); //爆炸声
        }
    }
}
