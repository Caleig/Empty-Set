using Microsoft.Xna.Framework;
using EmptySet.Tiles;
using EmptySet.UI;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace EmptySet.Common.Systems
{
    internal class EmptySetUI:ModSystem
    {
		private TelescopeUI telescopeUI;
		private PortalUI portalUI;
		private UserInterface _telescopeUserInterface;
		private UserInterface _portalUserInterface;

		public override void Load()
		{
			if (!Main.dedServ)
			{
				telescopeUI = new TelescopeUI();
				portalUI = new PortalUI();
				telescopeUI.Activate();
				portalUI.Activate();
				_telescopeUserInterface = new UserInterface();
				_telescopeUserInterface.SetState(telescopeUI);
				_portalUserInterface = new UserInterface();
				_portalUserInterface.SetState(portalUI);
			}
		}

		public override void UpdateUI(GameTime gameTime)
		{
			if (TelescopePlayer.CamMoveEnabled) _telescopeUserInterface?.Update(gameTime); 
			if(PortalUI.Visible) _portalUserInterface?.Update(gameTime);
			base.UpdateUI(gameTime);
		}
		/// <summary>
		/// 将被多个钩子调用 待优化
		/// </summary>
		/// <param name="layers"></param>
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
			if (mouseTextIndex != -1)
			{
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"EmptySet: 望远镜UI",
					delegate {
						if (TelescopePlayer.CamMoveEnabled)
						{
							_telescopeUserInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
				layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
					"EmptySet: 传送器UI",
					delegate {
						if (PortalUI.Visible)
						{
							_portalUserInterface.Draw(Main.spriteBatch, new GameTime());
						}
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
			base.ModifyInterfaceLayers(layers);
		}
	}
}
