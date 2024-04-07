using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using EmptySet.Utils;
using EmptySet.Tiles;
using Terraria.Graphics.Effects;
using Microsoft.Xna.Framework;
using EmptySet.Projectiles.Boss.EarthShaker;
using EmptySet.Projectiles.Weapons.Melee;
using EmptySet.Projectiles.Ranged;
using Microsoft.Xna.Framework.Graphics;
//using SubworldLibrary;
//using EmptySet.Common.Systems.subworlds;

namespace EmptySet.Items.Consumables
{
    //测试类
    internal class SubTest: ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Test");



            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = UseSpeedLevel.NormalSpeed + 2;
            Item.useTime = UseSpeedLevel.NormalSpeed + 2;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = false;
            Item.shoot = ModContent.ProjectileType<StarLightProj2>();
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Player player = Main.player[whoAmI];
            Texture2D texture = ModContent.Request<Texture2D>("EmptySet/Projectiles/Weapons/Melee/StarLightProj").Value;
            Vector2 pos = player.Center - Main.screenPosition;
            if (player.channel)
            {
                if (player.direction > 0)
                {
                    Main.spriteBatch.Draw(texture, new Vector2(pos.X - 10, pos.Y + 7), null, lightColor, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
                }
                else
                {
                    Main.spriteBatch.Draw(texture, new Vector2(pos.X + 10, pos.Y + 7), null, lightColor, MathHelper.Pi, texture.Size() * 0.5f, 1f, SpriteEffects.FlipHorizontally, 0f);
                }
            }
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
        /*
                public override bool? UseItem(Player player)
                {
                    //SubworldSystem.Enter<TheCoreOfChaos>();
                    int x = (int)(Main.mouseX + Main.screenPosition.X) / 16;
                    int y = (int)(Main.mouseY + Main.screenPosition.Y) / 16;
                    //WorldGen.PlaceTile(x, y, 26);
                    //WorldGen.Place3x2(x, y, (ushort)ModContent.TileType<崩溃祭坛Tile>());
                    /*Tile tile = Framing.GetTileSafely(x, y);
                    if (tile.TileType == TileID.Containers)
                    {
                        //tile.TileType = TileID.Stone;
                        Chest chest;
                        for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
                        {
                            chest = Main.chest[chestIndex];
                            if (chest != null && chest.x == x && chest.y == y) 
                            {

                            Main.NewText(chest.ToString());
                                for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                                {
                                    chest.item[inventoryIndex].type = ItemID.None;
                                }
                            }
                        }
                    }*/
        //WorldGen.KillTile(x, y);
        /*if (SkyManager.Instance["jcSky"].IsActive())
        {
            SkyManager.Instance.Deactivate("jcSky");
            Main.NewText("guan");
        }
        else 
        {
            SkyManager.Instance.Activate("jcSky");
            Main.NewText("kai");
        }*/
        /*foreach (Dust g in Main.dust) 
        {
            if (g.active&&g.type == DustID.Snow) 
            {

                g.velocity = (player.Center - g.position).SafeNormalize(Microsoft.Xna.Framework.Vector2.UnitX)*10;
            }
        }
        Projectile.NewProjectile(Item.GetSource_FromAI(), Main.MouseWorld, -Vector2.UnitY * 7, ModContent.ProjectileType<钻地导弹>(), 10, 0, Main.myPlayer, Main.myPlayer, 0);
        return true;
    }
        */
    }
}
