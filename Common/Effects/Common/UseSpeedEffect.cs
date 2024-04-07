using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Effects.Common;

/// <summary>
/// 设置对应伤害类型的使用速度
/// </summary>
/// <remarks>
/// eg: player.GetModPlayer&lt;UseSpeedEffect&gt;().AddSpeed(DamageClass.Throwing, 0.12f);
/// </remarks>
public class UseSpeedEffect : ModPlayer
{
    public bool IsEnabled { get; private set; } = false; //武器使用速度是否生效
    private Dictionary<int,float> _damageListOfUseSpeed;//影响武器使用速度的伤害类型 和 对应的武器使用速度倍数

    // <summary>
    // public no mean
    // if no use speed
    // </summary>
    
    //public void Enable() => IsEnabled = true;

    /// <summary>
    /// 增加对应伤害类型的使用速度（负值即减少）
    /// </summary>
    /// <param name="damageClass"></param>
    /// <param name="value"></param>
    public void AddSpeed(DamageClass damageClass,float value)
    {
        if (!IsEnabled) IsEnabled = true;
        this._damageListOfUseSpeed[damageClass.Type] += value;
    }
    /// <summary>
    /// 武器使用速度初始化
    /// </summary>
    public override void Initialize()
    {
        _damageListOfUseSpeed = new Dictionary<int, float>
        {
            {DamageClass.Melee.Type, 1f},
            {DamageClass.Ranged.Type, 1f},
            {DamageClass.Magic.Type, 1f},
            {DamageClass.Summon.Type, 1f},
            {DamageClass.Throwing.Type, 1f}
        };
    }
    /// <summary>
    /// 使武器使用速度重置
    /// </summary>
    public override void ResetEffects()
    {
        if (IsEnabled)
        {
            IsEnabled = false;
            foreach (var i in _damageListOfUseSpeed.Where(i => Math.Abs(i.Value - 1f) > 0.001f))
                _damageListOfUseSpeed[i.Key] = 1f;
        }
    }

    public override float UseSpeedMultiplier(Terraria.Item item) =>
        IsEnabled && _damageListOfUseSpeed.ContainsKey(item.DamageType.Type)
            ? _damageListOfUseSpeed[item.DamageType.Type]
            : base.UseSpeedMultiplier(item);
    
}