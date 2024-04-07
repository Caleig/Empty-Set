namespace EmptySet.Utils;

/// <summary>
/// 2f-3f 很弱击退力
/// 5f-6f 普通
/// 7f 较强
/// 8f-9f 很强
/// 10f-11f 极强
/// 12f-14f 疯狂
/// </summary>
public static class KnockBackLevel
{
    /// <summary>
    /// 无
    /// </summary>
    public const float None = 0f;
    /// <summary>
    /// 极弱
    /// </summary>
    public const float VeryLower = 1f;
    /// <summary>
    /// 很弱
    /// </summary>
    public const float TooLower = 3f;
    /// <summary>
    /// 较弱
    /// </summary>
    public const float BeLower = 4f;
    /// <summary>
    /// 普通
    /// </summary>
    public const float Normal = 5f;
    /// <summary>
    /// 较高
    /// </summary>
    public const float BeHigher = 7f;
    /// <summary>
    /// 很强
    /// </summary>
    public const float TooHigher = 8f;
    /// <summary>
    /// 极强
    /// </summary>
    public const float VeryHigh =10f;
    /// <summary>
    /// 疯狂
    /// </summary>
    public const float Crazy = 12f;
}