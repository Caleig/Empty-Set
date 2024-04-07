using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace EmptySet.Extensions;

public static class Vector2Extensions
{
    /// <summary>
    /// 在碰撞时触发：使向量反弹
    /// </summary>
    /// <param name="velocity"></param>
    /// <param name="oldVelocity"></param>
    public static void Rebound(this ref Vector2 velocity, Vector2 oldVelocity)
    {
        if (velocity.X != oldVelocity.X)
        {
            velocity.X = -oldVelocity.X;
        }
        if (velocity.Y != oldVelocity.Y)
        {
            velocity.Y = -oldVelocity.Y;
        }
    }

    public static Vector2 DirectionalityRotationBy(this Vector2 vector2, int dir, double rotationValue) =>
        dir < 0 ? vector2.RotatedBy(rotationValue) : vector2.RotatedBy(-rotationValue);
}
