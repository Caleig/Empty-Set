using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EmptySet.Common.Abstract.Items;

public abstract class BossBag : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("Treasure Bag");
        // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        ItemID.Sets.BossBag[Type] = true;
        ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
    }
    public override void SetDefaults()
    {
        Item.maxStack = 999;
        Item.consumable = true;
        Item.width = 54;
        Item.height = 54;
        Item.rare = ItemRarityID.Purple;
        Item.expert = true;
    }
    public override bool CanRightClick()
    {
        return true;
    }
    // 下面是视觉效果的代码
    public override Color? GetAlpha(Color lightColor)
    {
        // 确保掉落的包总是可见的
        return Color.Lerp(lightColor, Color.White, 0.4f);
    }

    public override void PostUpdate()
    {
        // 当掉落在世界上时产生一些光和灰尘
        Lighting.AddLight(Item.Center, Color.White.ToVector3() * 0.4f);

        if (Item.timeSinceItemSpawned % 12 == 0)
        {
            Vector2 center = Item.Center + new Vector2(0f, Item.height * -0.1f);

            // 这创建了一个长度为1的随机旋转向量，它的分量乘以参数
            Vector2 direction = Main.rand.NextVector2CircularEdge(Item.width * 0.6f, Item.height * 0.6f);
            float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
            Vector2 velocity = new Vector2(0f, -Main.rand.NextFloat() * 0.3f - 1.5f);

            Dust dust = Dust.NewDustPerfect(center + direction * distance, DustID.SilverFlame, velocity);
            dust.scale = 0.5f;
            dust.fadeIn = 1.1f;
            dust.noGravity = true;
            dust.noLight = true;
            dust.alpha = 0;
        }
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        // 当物品掉落到世界中时，在物品后面绘制周期性辉光效果(因此是PreDrawInWorld)
        Texture2D texture = TextureAssets.Item[Item.type].Value;

        Rectangle frame;

        if (Main.itemAnimations[Item.type] != null)
        {
            // 如果这个项目是动画的，这将选择正确的帧
            frame = Main.itemAnimations[Item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
        }
        else
        {
            frame = texture.Frame();
        }

        Vector2 frameOrigin = frame.Size() / 2f;
        Vector2 offset = new Vector2(Item.width / 2f - frameOrigin.X, Item.height - frame.Height);
        Vector2 drawPos = Item.position - Main.screenPosition + frameOrigin + offset;

        float time = Main.GlobalTimeWrappedHourly;
        float timer = Item.timeSinceItemSpawned / 240f + time * 0.04f;

        time %= 4f;
        time /= 2f;

        if (time >= 1f)
        {
            time = 2f - time;
        }

        time = time * 0.5f + 0.5f;

        for (float i = 0f; i < 1f; i += 0.25f)
        {
            float radians = (i + timer) * MathHelper.TwoPi;

            spriteBatch.Draw(texture, drawPos + new Vector2(0f, 8f).RotatedBy(radians) * time, frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, SpriteEffects.None, 0);
        }

        for (float i = 0f; i < 1f; i += 0.34f)
        {
            float radians = (i + timer) * MathHelper.TwoPi;

            spriteBatch.Draw(texture, drawPos + new Vector2(0f, 4f).RotatedBy(radians) * time, frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, SpriteEffects.None, 0);
        }

        return true;
    }
}