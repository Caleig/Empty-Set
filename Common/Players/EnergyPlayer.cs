using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Players;

public class EnergyPlayer : ModPlayer
{
    public bool IsEnabled { get; private set; } = false;
    public void Enable() => IsEnabled = true;

    public override void ResetEffects()
    {
        if (IsEnabled) IsEnabled = false;    
    }
	private int defaultEnergy = 30;
    public int Enengy { get; set; } = 0;
    public int EnengyMax { get; set; } = 0;

    private int stillCharging = 3* EmptySet.Frame;
    private int howFramePreCharge = 20;
	private int frameTochargeTimer = 0;
	private int waitChargingTimer = 0;
	public bool Consume(int energy)
	{
        if (CanConsume(energy))
        {
            waitChargingTimer = stillCharging; //进入战斗
            Enengy -= energy;
            return true;
        }
        return false;
    }
    public bool CanConsume(int energy)
    {
        if (IsEnabled && Enengy >= energy)
            return true;
        return false;
    }
    public bool IsMaxEnergy => Enengy == EnengyMax;
    public override void Initialize()
    {
        //if (IsEnabled) 
        //    EnengyMax = defaultEnergy;
    }
    public override void PostUpdate()//2
    {
        //重置 如果没佩戴的话
        if (!IsEnabled) Enengy = 0;
    }
    public override void PostUpdateMiscEffects()//1
    {
        if (IsEnabled && Enengy > EnengyMax)
            Enengy = EnengyMax;
    }
    public override void PreUpdate()
    {
        if (IsEnabled)
        {
            //如果不脱战
            if (waitChargingTimer > 0)
                waitChargingTimer--;
            else
            {//脱战
             //充能
                if (frameTochargeTimer < howFramePreCharge)
                    frameTochargeTimer++;
                else
                {
                    if (Enengy < EnengyMax) Enengy++;
                    //Main.NewText($"energy:{Enengy}");
                    frameTochargeTimer = 0;
                }
            }
            //重置MAX
            EnengyMax = defaultEnergy;
        }
    }
}