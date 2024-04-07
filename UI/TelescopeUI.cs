using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace EmptySet.UI
{
    internal class TelescopeUI : UIState
    {
        public UIText tipText;
        public override void OnInitialize()
        {
            tipText = new UIText("望远镜视角：\nW,A,S,D移动视角\nEsc退出");
            tipText.Left.Set(50,0);
            tipText.Top.Set(Main.screenHeight-200,0);
            tipText.Width.Set(200, 0);
            tipText.Height.Set(120, 0);
            Append(tipText);
        }
    }
}
