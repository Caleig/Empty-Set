using Microsoft.Xna.Framework;
using EmptySet.Common.Systems;
using EmptySet.UI.CustomComponents;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace EmptySet.UI
{
    internal class PortalUI : UIState
    {

		public static UIList UIPortalList;
		public static UIPanel panel;
		public static List<PortalTag> PortalList;
		public static bool Visible = false;
		public static int PortalIndex;
		public override void OnInitialize()
        {
			panel = new UIPanel();
			panel.SetPadding(0);
			panel.Left.Set(500f, 0f);
			panel.Top.Set(500f, 0f);
			panel.Width.Set(170f, 0f);
			panel.Height.Set(300f, 0f);
			panel.BackgroundColor = new Color(73, 94, 171);

			UIImageButton closeButton = new UIImageButton(TextureAssets.Item[2]);
			closeButton.Left.Set(140, 0f);
			closeButton.Top.Set(10, 0f);
			closeButton.Width.Set(22, 0f);
			closeButton.Height.Set(22, 0f);
			closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
			panel.Append(closeButton);
			Append(panel);
		}

		public static void addItem(int index)
		{
			PortalIndex = index;
			if (UIPortalList != null)
			{
				panel.RemoveChild(UIPortalList);
			}
			PortalList = EmptySetWorld.PortalList;
			UIPortalList = new UIList();
			for (int a = 0; a < PortalList.Count; a++)
			{
				if (a == index) continue;
				UIEmptySetText name = new UIEmptySetText(PortalList[a].portalName);
				name.Top.Set(5, 0f);
				name.OnLeftClick += Name_OnClick;
				name.parameter1 = a;
				UIPortalList.Add(name);
			}
			UIPortalList.Left.Set(10, 0f);
			UIPortalList.Top.Set(10, 0f);
			UIPortalList.Width.Set(100, 0f);
			UIPortalList.Height.Set(700, 0f);
			panel.Append(UIPortalList);
		}

        private static void Name_OnClick(UIMouseEvent evt, UIElement listeningElement)
        {
			
			for (int a = 0; a < PortalList.Count; a++)
			{
				if (PortalList[a].id == ((UIEmptySetText)listeningElement).parameter1)
				{
					Vector2 destination = new Vector2(PortalList[a].x * 16, (PortalList[a].y - 2) * 16);
					Main.player[Main.myPlayer].Teleport(destination, TeleportationStyleID.TeleporterTile, 0);
					Visible = false;
				}
			}
		}
		private void CloseButtonClicked(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuClose);
			Visible = false;
		}

        public override void Update(GameTime gameTime)
        {
			if (Vector2.Distance(new Vector2(PortalList[PortalIndex].x * 16, PortalList[PortalIndex].y * 16), Main.player[Main.myPlayer].Center) >= 100) 
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				Visible = false;
			}

			base.Update(gameTime);
        }
    }
}
