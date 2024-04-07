using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;

namespace EmptySet.Extensions;

public static class ProjectileExtensions
{
    /// <summary>
    /// 使弹幕产生一个爆炸伤害，爆炸完弹幕恢复原状
    /// </summary>
    /// <param name="projectile">要爆炸的弹幕</param>
    /// <param name="explosionArea">爆炸范围</param>
    /// <param name="damage">爆炸伤害</param>
    /// <param name="friendly">是否对敌方伤害</param>
    /// <param name="hostile">是否对友方伤害</param>
    public static void LetExplode(this Projectile projectile, int explosionArea, int damage = default, bool friendly = true,
        bool hostile = true)
    {
        var oldSize = projectile.Size;
        var oldHostile = projectile.hostile;
        var oldDamage = projectile.damage;
        var oldFriendly = projectile.friendly;

        projectile.maxPenetrate = -1;
        projectile.penetrate = -1;
        projectile.position = projectile.Center;
        projectile.Size += new Vector2(explosionArea);
        projectile.Center = projectile.position;
        projectile.tileCollide = false;
        projectile.velocity *= 0.01f;
        projectile.damage = damage == -1 ? projectile.damage : damage;
        projectile.hostile = hostile;
        projectile.friendly = friendly;
        projectile.Damage();
        //projectile.scale = 0.01f;
        projectile.position = projectile.Center;
        projectile.Size = oldSize;
        projectile.Center = projectile.position;
        projectile.hostile = oldHostile;
        projectile.friendly = oldFriendly;
        projectile.damage = oldDamage;
    }

    /// <summary>
    /// 使弹幕产生一个爆炸伤害,伴随指定特效,爆炸完弹幕恢复原状
    /// </summary>
    /// <param name="projectile">要爆炸的弹幕</param>
    /// <param name="explosionArea">爆炸范围</param>
    /// <param name="damage">爆炸伤害</param>
    /// <param name="action">填入效果代码</param>
    /// <param name="friendly">是否对敌方伤害</param>
    /// <param name="hostile">是否对友方伤害</param>
    public static void LetExplodeWith(this Projectile projectile, int explosionArea, Action action, int damage = default,
        bool friendly = true, bool hostile = true)
    {
        var oldSize = projectile.Size;
        var oldHostile = projectile.hostile;
        var oldDamage = projectile.damage;
        var oldFriendly = projectile.friendly;

        projectile.maxPenetrate = -1;
        projectile.penetrate = -1;
        projectile.position = projectile.Center;
        projectile.Size += new Vector2(explosionArea);
        projectile.Center = projectile.position;
        projectile.tileCollide = false;
        projectile.velocity *= 0.01f;
        projectile.damage = damage == default ? projectile.damage : damage;
        projectile.hostile = hostile;
        projectile.friendly = friendly;
        projectile.Damage();
        action.Invoke();
        projectile.position = projectile.Center;
        projectile.Size = oldSize;
        projectile.Center = projectile.position;
        projectile.hostile = oldHostile;
        projectile.friendly = oldFriendly;
        projectile.damage = oldDamage;
    }

    /// <summary>
    /// 使弹幕方向性旋转
    /// </summary>
    /// <remarks>
    /// 比如镰刀，往左边辉，往左边转。往右边辉，往右边转。
    /// </remarks>
    /// <param name="projectile">指定弹幕</param>
    /// <param name="rotationValue">旋转大小</param>
    public static void DirectionalityRotation(this Projectile projectile, float rotationValue)
    {
        if (projectile.spriteDirection < 0) //左 -1 | 右 1
            projectile.rotation -= rotationValue;
        else
            projectile.rotation += rotationValue;
    }
    public static void DirectionalityRotationSet(this Projectile projectile, float rotationValue)
    {
        if (projectile.spriteDirection < 0) //左 -1 | 右 1
            projectile.rotation = rotationValue;
        else
            projectile.rotation = rotationValue - MathHelper.PiOver2;
    }
    /// <summary>
    /// 控制帧图的切换时间
    /// </summary>
    /// <remarks>
    /// 请在Proj的AI()/PerAI()中使用 <br />
    /// 前提：SetStaticDefaults()中，需有Main.projFrames[Type] = {帧数量};
    /// </remarks>
    /// <param name="projectile">指定弹幕</param>
    /// <param name="nextFrameTime">下一帧时间</param>
    public static void FrameControl(this Projectile projectile,int nextFrameTime = 5)
    {
        if (projectile.frameCounter < nextFrameTime)
        {
            projectile.frameCounter++;
        }
        else
        {
            projectile.frameCounter = 0;
            projectile.frame++;
            if (projectile.frame == Main.projFrames[projectile.type])
                projectile.frame = 0;
        }
    }

    //public static void FindNpc(this Projectile projectile,int distance, float curvature, bool FindNotFriendly = true,
    //    int frameToFind = 3)
    //{
    //    if (Main.time % frameToFind == 0)    //每两帧记录一次（打一次点）
    //    {
    //        var npcs = Main.npc.Where((x) =>
    //            Vector2.Distance(x.position, projectile.position) < distance &&
    //            x.friendly == !FindNotFriendly &&
    //            x.active
    //        );
    //    }

        
    //    var npc = npcs.OrderBy(x => Vector2.Distance(x.position, projectile.position)).ToList();
    //    if (npc.Count > 0)
    //    {
    //        var len = projectile.velocity.Length();
    //        var np = npc[0].position;
    //        var pp = projectile.position;
    //        var v2 = new Vector2(np.X - pp.X, np.Y - pp.Y);
    //        var v2p = projectile.velocity;
    //        var v2f = (v2 * 0.00046f + v2p).SafeNormalize(Vector2.One);
    //        projectile.velocity = v2f * len;
    //    }
    //}
}