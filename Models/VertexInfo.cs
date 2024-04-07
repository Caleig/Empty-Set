using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmptySet.Models;

//public record VertexInfo(Vector2 Position, Color Color, Vector3 TextureCoordinate) : IVertexType
//{
//    public VertexDeclaration VertexDeclaration => new(new VertexElement[]
//    {
//        new(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
//        new(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
//        new(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
//    });
//}

public struct VertexInfo : IVertexType
{
    public Vector2 Position { get; }
    public Color Color { get; }
    public Vector3 TextureCoordinate { get; }
    public VertexInfo(Vector2 position, Color color, Vector3 textureCoordinate)
    {
        Position = position;
        Color = color;
        TextureCoordinate = textureCoordinate;
    }
    public VertexDeclaration VertexDeclaration => new(new VertexElement[]
    {
        new(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
        new(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
        new(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0),
    });
}