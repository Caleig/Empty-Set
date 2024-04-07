using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.ID;

namespace EmptySet.Common.Effects.Item;

public class UlcerPupilEffect : ModPlayer
{
    public bool IsEnable { get; private set; } = false;
    public bool hasDebuff = false;
    public int[] debuffTypes = {
        //来自敌怪
        BuffID.Poisoned,
        BuffID.Darkness,
        BuffID.Cursed,
        BuffID.OnFire,
        BuffID.Bleeding,
        BuffID.Confused,
        BuffID.Slow,
        BuffID.Weak,
        BuffID.Silenced,
        BuffID.BrokenArmor,
        BuffID.Horrified,
        BuffID.TheTongue,
        BuffID.CursedInferno,
        BuffID.Frostburn,
        BuffID.Chilled,
        BuffID.Frozen,
        BuffID.Ichor,
        BuffID.Venom,
        BuffID.Blackout,
        BuffID.Electrified,
        BuffID.MoonLeech,
        BuffID.Rabies,
        BuffID.Webbed,
        BuffID.Stoned,
        BuffID.Obstructed,
        BuffID.VortexDebuff,
        BuffID.WitheredArmor,
        BuffID.WitheredWeapon,
        BuffID.OgreSpit,
        //来自物品和环境
        BuffID.ManaSickness,
        BuffID.PotionSickness,
        BuffID.ChaosState,
        BuffID.Suffocation,
        BuffID.Burning,
        BuffID.Tipsy,
        BuffID.Lovestruck,
        BuffID.Stinky,
        BuffID.WaterCandle,
        BuffID.WindPushed,
        BuffID.NoBuilding,
        BuffID.NeutralHunger,
        BuffID.Hunger,
        BuffID.Starving,
        //来自mod
    };

    public void Enable()
    {
        IsEnable = true;
    }

    public override void PreUpdate()
    {
        base.PreUpdate();
        if (IsEnable)
        {
            foreach (int type in Player.buffType) 
            {
                foreach (int t in debuffTypes) 
                {
                    if (type == t) 
                    {
                        hasDebuff = true;
                        break;
                    }
                }
                if (hasDebuff) break;
            }
            EmptySet.Instance.ulcerPupilScreenShaderData.multiplier = hasDebuff ? 10f : 1.25f;
            //效果:视野变暗
            //获得debuff后将被致盲
            if (!Filters.Scene["EmptySet:UlcerPupilScreen"].IsActive()) Filters.Scene.Activate("EmptySet:UlcerPupilScreen");
            //Player.buffType 
            //EmptySet.DebugText("some");
        }
        else 
        {
            if (Filters.Scene["EmptySet:UlcerPupilScreen"].IsActive()) Filters.Scene.Deactivate("EmptySet:UlcerPupilScreen");
        }
    }

    public override void ResetEffects()
    {
        base.ResetEffects();
        IsEnable = false;
        hasDebuff = false;
    }
}