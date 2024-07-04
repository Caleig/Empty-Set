using EmptySet.Common.Players;
using EmptySet.Items.Accessories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace EmptySet.UI;

internal class EnergyUIState : UIState
{
    private UIText text;
    private UIText mouseText;
    private UIElement area;
    private UIImage boxFrame;
    private Color gradientA;
    private Color gradientB;
    private Vector2 rec = Vector2.Zero;

    public override void OnInitialize()
    {
        // UIElement is invisible and has no padding.
        area = new UIElement();
        area.Left.Set(-area.Width.Pixels - 600, 1f); // Place the resource bar to the left of the hearts.
        area.Top.Set(40, 0f); // Placing it just a bit below the top of the screen.
        area.Width.Set(40, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
        area.Height.Set(40, 0f);

        boxFrame = new UIImage(ModContent.Request<Texture2D>("EmptySet/UI/EnergyUI")); // Frame of our resource bar
        boxFrame.Left.Set(0, 0f);
        boxFrame.Top.Set(0, 0f);
        boxFrame.Width.Set(40, 0f);
        boxFrame.Height.Set(40, 0f);

        text = new UIText("", 0.8f); // text to show stat
        text.Width.Set(20, 0f);
        text.Height.Set(20, 0f);
        text.Top.Set(14, 0f);
        text.Left.Set(10, 0f);

        mouseText = new UIText("");
        mouseText.Deactivate();

        //gradientA = new Color(123, 25, 138); // A dark purple
        //gradientB = new Color(187, 91, 201); // A light purple
        area.OnMouseOver += Area_OnMouseOver;
        area.OnMouseOut += Area_OnMouseOut; ;
        area.OnLeftMouseDown += Area_OnLeftMouseDown;
        area.Append(boxFrame);
        area.Append(text);
        Append(area);
        Append(mouseText);
    }

    private void Area_OnLeftMouseDown(UIMouseEvent evt, UIElement listeningElement)
    {
        var mp = evt.MousePosition;
        if (rec == Vector2.Zero)
        {
            var x = area.Left.Pixels;
            if (x < 0)
                x = Main.screenWidth + x;
            rec = new Vector2(mp.X - x, mp.Y - area.Top.Pixels);
        }
    }
    public override void LeftMouseUp(UIMouseEvent evt)
    {
        rec = Vector2.Zero;
    }
    public override void MouseOver(UIMouseEvent evt)
    {
        if (rec != Vector2.Zero)
        {
            var mp = evt.MousePosition;
            var pos = new Vector2(mp.X - rec.X, mp.Y - rec.Y);
            //Main.NewText($"mp:{mp},pos:{pos}");
            area.Left.Set(pos.X, 0f);
            area.Top.Set(pos.Y, 0f);
            //area.Recalculate();
        }
    }
    private void Area_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
        mouseText.Deactivate();
        mouseText.SetText("");
    }

    private void Area_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
        var modPlayer = Main.LocalPlayer.GetModPlayer<EnergyPlayer>();
        mouseText.Activate();
        mouseText.Left.Set(evt.MousePosition.X, 0f);
        mouseText.Top.Set(evt.MousePosition.Y, 0f);
        mouseText.SetText($"{EnergyUISystem.UIText.Format()}:{modPlayer.Enengy}/{modPlayer.EnengyMax}");
    }
    private bool IsWearing => Main.LocalPlayer.armor.Any((x) => x.type == ModContent.ItemType<EarthSharkerAmulet>());
    public override void Draw(SpriteBatch spriteBatch)
    {
        // This prevents drawing unless we are using an ExampleCustomResourceWeapon
        if (!IsWearing)
            return;
        base.Draw(spriteBatch);
    }
    //public override void MouseOver(UIMouseEvent evt)
    //{
    //    //if (mouseText is null)
    //    //    mouseText = new UIText("");
    //    //mouseText.Top.Set(evt.MousePosition.Y, 0f);
    //    //mouseText.Left.Set(evt.MousePosition.X, 0f);
    //    //var modPlayer = Main.LocalPlayer.GetModPlayer<EnergyPlayer>();
    //    //mouseText.SetText(EnergyUISystem.UIText.Format() + $":{modPlayer.Enengy}/{modPlayer.EnengyMax}");
    //    base.MouseOver(evt);
    //}
    //public override void MouseOut(UIMouseEvent evt)
    //{
    //    //mouseText = null;
    //    base.MouseOut(evt);
    //}
    // Here we draw our UI
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);


        // Calculate quotient
        //float quotient = (float)modPlayer.Enengy / modPlayer.EnengyMax; // Creating a quotient that represents the difference of your currentResource vs your maximumResource, resulting in a float of 0-1f.

        // Here we get the screen dimensions of the barFrame element, then tweak the resulting rectangle to arrive at a rectangle within the barFrame texture that we will draw the gradient. These values were measured in a drawing program.


        //Rectangle hitbox = barFrame.GetInnerDimensions().ToRectangle();
        //hitbox.X += 12;
        //hitbox.Width -= 24;
        //hitbox.Y += 8;
        //hitbox.Height -= 16;

        //// Now, using this hitbox, we draw a gradient by drawing vertical lines while slowly interpolating between the 2 colors.
        //int top  = hitbox.Top;
        //int bottom = hitbox.Bottom;
        //int steps = (int)((top - bottom) * quotient);
        //for (int i = 0; i > steps; i -= 1)
        //{
        //    // float percent = (float)i / steps; // Alternate Gradient Approach
        //    float percent = (float)i / (top - bottom);
        //    spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(hitbox.X , hitbox.Y + i, 1, hitbox.Width), Color.Lerp(gradientA, gradientB, percent));
        //}
    }

    public override void Update(GameTime gameTime)
    {
        if (!IsWearing)
            return;

        var modPlayer = Main.LocalPlayer.GetModPlayer<EnergyPlayer>();
        // Setting the text per tick to update and show our resource values.
        text.SetText(modPlayer.Enengy.ToString());
        base.Update(gameTime);
    }
}

// This class will only be autoloaded/registered if we're not loading on a server
[Autoload(Side = ModSide.Client)]
internal class EnergyUISystem : ModSystem
{
    private UserInterface energyUI;

    internal EnergyUIState energyUIState;

    public static LocalizedText UIText { get; private set; }

    public override void Load()
    {
        energyUIState = new();
        energyUI = new();
        energyUI.SetState(energyUIState);

        string category = "UI";
        UIText ??= Mod.GetLocalization($"{category}.Energy");
    }

    public override void UpdateUI(GameTime gameTime)
    {
        energyUI?.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals($"Vanilla: Mouse Text"));
        if (resourceBarIndex != -1)
        {
            layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                $"{nameof(EmptySet)}: Energy UI",
                delegate
                {
                    energyUI.Draw(Main.spriteBatch, new GameTime());
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }

}