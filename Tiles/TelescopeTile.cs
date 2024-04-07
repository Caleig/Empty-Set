using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EmptySet.Items.Placeable;
using EmptySet.Projectiles.Firework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace EmptySet.Tiles;

public class TelescopeTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;


        TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
        TileObjectData.newTile.Height = 3;
        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
        TileObjectData.addTile(Type);

        LocalizedText name = CreateMapEntryName();

        AddMapEntry(new Color(200, 200, 200), name);
    }
    public override void MouseOver(int i, int j)
    {
        Player player = Main.LocalPlayer;
        player.noThrow = 2;
        player.cursorItemIconEnabled = true;
        player.cursorItemIconID = ModContent.ItemType<Telescope>();
    }
    public override bool RightClick(int i, int j)
    {
        if (!TelescopePlayer.CamMoveEnabled) 
        {
            SoundEngine.PlaySound(SoundID.MenuOpen, Main.LocalPlayer.position);
            TelescopePlayer.CamMoveEnabled = true;
        }
        return base.RightClick(i, j);
    }
    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        TelescopePlayer.CamMoveEnabled = false;
        Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 48, ModContent.ItemType<Telescope>());
    }
}
public class TelescopePlayer : ModPlayer
{
    internal static Vector2 FlyCamPosition = Vector2.Zero;
    internal static int launcherFirework = 0;
    private int fireworkTimer = 60 * 10;
    internal static int launcherFireworkX;
    internal static int launcherFireworkY;
    internal const int STOP = 0;
    internal const int PREPARE = 1;
    internal const int FIRE = 2;
    internal static bool CamMoveEnabled { get; set; }
    /// <summary>
    /// 问题 这玩意会被所有钩子调用，可能引起奇怪的问题
    /// </summary>
    public override void PostUpdate()
    {
        if (CamMoveEnabled && !Main.blockInput)
        {

            float speed = 30f;

            if (Main.keyState.IsKeyDown(Keys.LeftAlt)) speed *= .3f;
            if (Main.keyState.IsKeyDown(Keys.LeftShift)) speed *= 1.5f;

            if (FlyCamPosition.X - Player.position.X < 16 * 200 - Main.screenWidth)
            {
                if (PlayerInput.Triggers.Current.KeyStatus["Right"]) FlyCamPosition.X += speed;
            }
            if (FlyCamPosition.X - Player.position.X > -16 * 200)
            {
                if (PlayerInput.Triggers.Current.KeyStatus["Left"]) FlyCamPosition.X -= speed;
            }

            if (PlayerInput.Triggers.Current.KeyStatus["Up"]) FlyCamPosition.Y -= speed;

            if (FlyCamPosition.Y < Player.position.Y - 16 * 35)
            {
                if (PlayerInput.Triggers.Current.KeyStatus["Down"]) FlyCamPosition.Y += speed;
            }

            if (PlayerInput.Triggers.Current.KeyStatus["Inventory"]) CamMoveEnabled = false;

        }
        else
        {
            FlyCamPosition = Main.screenPosition;
        }

        if (launcherFirework == PREPARE)
        {
            fireworkTimer--;
            if (fireworkTimer % 60 == 0)
            {
                switch (fireworkTimer / 60)
                {
                    case 5:
                        Main.NewText("距离烟花发射还剩" + (int)(fireworkTimer / 60) + "秒");
                        break;
                    case 3:
                        Main.NewText("距离烟花发射还剩" + (int)(fireworkTimer / 60) + "秒");
                        break;
                    case 2:
                        Main.NewText("距离烟花发射还剩" + (int)(fireworkTimer / 60) + "秒");
                        break;
                    case 1:
                        Main.NewText("距离烟花发射还剩" + (int)(fireworkTimer / 60) + "秒");
                        break;
                }
            }
            if (fireworkTimer <= 0)
            {
                Projectile.NewProjectile(Player.GetSource_TileInteraction(launcherFireworkX, launcherFireworkY), new Vector2((launcherFireworkX + 1.5f) * 16f, (launcherFireworkY + 1.5f) * 16f), new(0, 0), ModContent.ProjectileType<YanHuaProj>(), 0, 0, Main.myPlayer);
                SoundEngine.PlaySound(SoundID.Item14, Main.LocalPlayer.position);
                launcherFirework = FIRE;
                fireworkTimer = 60 * 10;
            }
        }
        else if (launcherFirework == FIRE)
        {
            fireworkTimer--;
            if (fireworkTimer <= 0)
            {
                launcherFirework = STOP;
            }
        }
        else
        {
            fireworkTimer = 60 * 10;
        }

        base.PostUpdate();
    }

    public override void PreSavePlayer()
    {
        CamMoveEnabled = false;
        base.PreSavePlayer();
    }
    public override void PreUpdateMovement()
    {
        if (CamMoveEnabled)
        {
            Player.velocity = new(0, 0);
        }
        base.PreUpdateMovement();
    }

    public override void ModifyScreenPosition()
    {
        if (CamMoveEnabled)
        {
            Main.screenPosition = FlyCamPosition;
        }
    }
}