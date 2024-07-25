using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.Localization;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace EmptySet.NPCs
{
    public abstract class BTLcNPC : ModNPC
    {
        public bool disorder = false;
        protected int State
        {
            get {return (int)NPC.ai[0];}
            set {NPC.ai[0] = value;}
        }
        protected int Timer
        {
            get { return (int)NPC.ai[1];}
            set { NPC.ai[1] = value; }
        }
        protected virtual void SwitchState(int state)
        {
            State = state;
        }
    }
}
