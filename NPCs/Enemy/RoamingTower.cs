using EmptySet.Common.Systems;
using EmptySet.Items.Consumables;
using EmptySet.Items.Materials;
using EmptySet.Projectiles.Boss.EarthShaker;
using EmptySet.Utils;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace EmptySet.NPCs.Enemy;
//掉落物应为巡游信标
public class RoamingTower : ModNPC
{
    public override void SetStaticDefaults()
    {
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
        {
            Velocity = 1f
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
    }
    public override void SetDefaults()
    {
        NPC.width = 22;
        NPC.height = 40;
        NPC.lifeMax = EmptySetUtils.GetNPCLifeMax(30, 60, 90);
        NPC.defense = 7;
        NPC.damage = EmptySetUtils.GetNPCDamage(5, 10, 15);
        NPC.knockBackResist = 1f;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        //NPC.value = 60f;
        NPC.aiStyle = 0;
    }
    int timer = 0;
    public override void AI()
    {
        if (timer != 120)
            timer++;
        else
        {
            if (NPC.position.Distance(Main.player[NPC.target].position) < 900f)
            {
                timer = 0;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Top, (Main.player[NPC.target].Center - NPC.Center + new Vector2(0, NPC.height / 2)).SafeNormalize(Vector2.UnitX) * 10, ModContent.ProjectileType<ChargedCrystal2Projectile>(), EmptySetUtils.ScaledProjDamage((NPC.damage)), 0, Main.myPlayer);
            }
        }
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(new OneFromRulesRule(1, 
            ItemDrop.GetItemDropRule(ItemID.SilverCoin, 100, 1, 2),
            ItemDrop.GetItemDropRule(ModContent.ItemType<Crystalfragments>(), 100),
            ItemDrop.GetItemDropRule(ModContent.ItemType<DustyRemoteControl>(), 10)
            ));
    }

    public override float SpawnChance(NPCSpawnInfo spawnInfo)
    {
        if (spawnInfo.Player.ZoneOverworldHeight)
        {
            if (DownedBossSystem.downedEarthShaker) //打败了撼地巨械
                return SpawnCondition.OverworldDay.Chance * 0.03f;
            else
                return SpawnCondition.OverworldDay.Chance * 0.1f;
        }
        return 0;
    }
    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
                new FlavorTextBestiaryInfoElement("这些微型炮塔喜欢在远处狙击敌人")
            });
    }
}
