using Terraria;

namespace EmptySet.NPCs.Boss.LavaHunter;

public class LavaHunterTail : LavaHunterBody
{
    public override void SetDefaults()
    {
        base.SetDefaults();
        NPC.width = 98;
        NPC.height = 58;
        NPC.damage = 90;
        NPC.defense = 25;
    }
    public override bool PreAI()
    {
        NPC.defense = Main.masterMode ? 25 : Main.expertMode ? 20 : 15;
        NPC.damage = Main.masterMode ? 90 : Main.expertMode ? 70 : 50;
        return base.PreAI();
    }
}