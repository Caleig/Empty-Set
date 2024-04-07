using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Abstract.NPCs
{
    /// <summary>
    /// npc有限状态机
    /// @author:Insidious_cat
    /// </summary>
    public abstract class NPCStateMachine: ModNPC
    {
        /// <summary>
        /// npc目标
        /// </summary>
        public Player target;
        /// <summary>
        /// 是否有存活目标
        /// </summary>
        public bool targetIsActive = true;

        //是否允许状态执行
        public bool allowNPCStatesAI = true;
        public bool allowNPCAttackStatesAI = false;
        public bool allowNPCStates1AI = false;
        public bool allowNPCStates2AI = false;

        //计数器
        public int NPCAttackStatesCount { get; set; } = 0;
        public int Count { get; set; } = 0;
        public int Count1 { get; set; } = 0;
        public int Count2 { get; set; } = 0;

        public IState CurrentState => _npcStates[State - 1];
        private List<IState> _npcStates = new();
        private Dictionary<string, int> _stateDict = new();

        public IState CurrentAttackState => _npcAttackStates[AttackState - 1];
        private List<IState> _npcAttackStates = new();
        private Dictionary<string, int> _attackStateDict = new();

        public IState CurrentState1 => _npcStates1[State1 - 1];
        private List<IState> _npcStates1 = new();
        private Dictionary<string, int> _stateDict1 = new();

        public IState CurrentState2 => _npcStates2[State2 - 1];
        private List<IState> _npcStates2 = new();
        private Dictionary<string, int> _stateDict2 = new();

        /// <summary>
        /// npc状态
        /// </summary>
        private int State
        {
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = (int)value;
        }
        /// <summary>
        /// npc攻击状态
        /// </summary>
        private int AttackState
        {
            get => (int)NPC.ai[1];
            set => NPC.ai[1] = (int)value;
        }
        //两个额外状态（看需求是否使用）
        private int State1
        {
            get => (int)NPC.ai[2];
            set => NPC.ai[2] = (int)value;
        }
        private int State2
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = (int)value;
        }
        /// <summary>
        /// npc状态计时器
        /// </summary>
        public int Timer
        {
            get => (int)NPC.localAI[0];
            set => NPC.localAI[0] = value;
        }
        /// <summary>
        /// npc攻击计时器
        /// </summary>
        public int AttackTimer
        {
            get => (int)NPC.localAI[1];
            set => NPC.localAI[1] = value;
        }
        //两个额外计时器
        public int Timer1
        {
            get => (int)NPC.localAI[2];
            set => NPC.localAI[2] = value;
        }
        public int Timer2
        {
            get => (int)NPC.localAI[3];
            set => NPC.localAI[3] = value;
        }
        /// <summary>
        /// 注册状态
        /// </summary>
        /// <typeparam name="T">需要注册的<see cref="IState"/>类</typeparam>
        /// <param name="state">需要注册的<see cref="IState"/>类的实例</param>
        protected void RegisterState<T>(T state) where T : IState
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo != null)
            {
                var typename = memberInfo.Name;
                var name = typeof(T).FullName ?? throw new ArgumentNullException("typeof(T).FullName");

                switch (typename)
                {
                    case nameof(NPCState) when _stateDict.ContainsKey(name):
                        throw new ArgumentException(name + "状态已经注册");
                    case nameof(NPCState):
                        _npcStates.Add(state);
                        _stateDict.Add(name, _npcStates.Count);
                        break;
                    case nameof(NPCAttackState) when _attackStateDict.ContainsKey(name):
                        throw new ArgumentException(name + "攻击状态已经注册");
                    case nameof(NPCAttackState):
                        _npcAttackStates.Add(state);
                        _attackStateDict.Add(name, _npcAttackStates.Count);
                        break;
                    case nameof(NPCState1) when _stateDict1.ContainsKey(name):
                        throw new ArgumentException(name + "状态1已经注册");
                    case nameof(NPCState1):
                        _npcStates1.Add(state);
                        _stateDict1.Add(name, _npcStates1.Count);
                        break;
                    case nameof(NPCState2) when _stateDict2.ContainsKey(name):
                        throw new ArgumentException(name + "状态2已经注册");
                    case nameof(NPCState2):
                        _npcStates2.Add(state);
                        _stateDict2.Add(name, _npcStates2.Count);
                        break;
                }
            }
        }
        /// <summary>
        /// 把当前状态变为指定的NPC状态实例
        /// </summary>
        /// <typeparam name="T">注册过的<see cref="IState"/>类名</typeparam>
        public void SetState<T>() where T : IState
        {
            var memberInfo = typeof(T).BaseType;
            if (memberInfo != null)
            {
                var typename = memberInfo.Name;
                var name = typeof(T).FullName ?? throw new ArgumentNullException("typeof(T).FullName");
                switch (typename)
                {
                    case nameof(NPCState) when !_stateDict.ContainsKey(name):
                        throw new ArgumentException(name + "状态不存在");
                    case nameof(NPCState):
                        State = _stateDict[name];
                        break;
                    case nameof(NPCAttackState) when !_attackStateDict.ContainsKey(name):
                        throw new ArgumentException(name + "攻击状态不存在");
                    case nameof(NPCAttackState):
                        AttackState = _attackStateDict[name];
                        break;
                    case nameof(NPCState1) when !_stateDict1.ContainsKey(name):
                        throw new ArgumentException(name + "状态1不存在");
                    case nameof(NPCState1):
                        State1 = _stateDict1[name];
                        break;
                    case nameof(NPCState2) when !_stateDict2.ContainsKey(name):
                        throw new ArgumentException(name + "状态2不存在");
                    case nameof(NPCState2):
                        State2 = _stateDict2[name];
                        break;
                }
                NPC.frameCounter = 0;
            }
        }
        /// <summary>
        /// 初始化函数，用于注册NPC状态
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// 把AI函数封住，这样在子类无法重写AI函数，只能用before和after函数
        /// </summary>
        public sealed override void AI()
        {
            if (State == 0)
            {
                Initialize();
                State = 1;
                AttackState = 1;
                State1 = 1;
                State2 = 1;
            }

            targetIsActive = FindPlayer();

            AIBefore(this);
            if (_npcStates.Count > 0 && allowNPCStatesAI) 
            {
                CurrentState.AI(this);
            }
            if (_npcAttackStates.Count > 0 && allowNPCAttackStatesAI) 
            {
                CurrentAttackState.AI(this);
            }
            if (_npcStates1.Count > 0 && allowNPCStates1AI) 
            {
                CurrentState1.AI(this);
            }
            if (_npcStates2.Count > 0 && allowNPCStates2AI)
            {
                CurrentState2.AI(this);
            }
            AIAfter(this);
        }
        /// <summary>
        /// 在状态机执行之前要执行的代码，可以重写
        /// </summary>
        protected virtual void AIAfter(NPCStateMachine nsm) { }
        /// <summary>
        /// 在状态机执行之后要执行的代码，可以重写
        /// </summary>
        protected virtual void AIBefore(NPCStateMachine nsm) { }

        /// <summary>
        /// 寻找并检查玩家是否死亡，可以重写
        /// </summary>
        /// <returns>返回<see cref="bool"/>类型</returns>
        protected virtual bool FindPlayer()
        {
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active) NPC.TargetClosest(); 
            target = Main.player[NPC.target];
            if (target.dead || !target.active|| Math.Abs(NPC.position.X - target.position.X) > 2000f || Math.Abs(NPC.position.Y - target.position.Y) > 2000f)  return false;
            return true;
        }
        /// <summary>
        /// npc帧动画
        /// </summary>
        /// <param name="frameHeight"></param>
        public override void FindFrame(int frameHeight)
        {
            if (_npcStates.Count > 0 && allowNPCStatesAI)
            {
                CurrentState.FindFrame(this, frameHeight);
            }
            if (_npcAttackStates.Count > 0 && allowNPCAttackStatesAI)
            {
                CurrentAttackState.FindFrame(this, frameHeight);
            }
            if (_npcStates1.Count > 0 && allowNPCStates1AI)
            {
                CurrentState1.FindFrame(this, frameHeight);
            }
            if (_npcStates2.Count > 0 && allowNPCStates2AI)
            {
                CurrentState2.FindFrame(this, frameHeight);
            }
            base.FindFrame(frameHeight);
        }
    }
    /// <summary>
    /// NPC状态接口
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// AI函数接受一个<see cref="NPCStateMachine"/>的modNPC对象
        /// </summary>
        /// <remarks>
        /// 方法中需要有切换下一状态的条件！
        /// </remarks>
        /// <param name="nsm"></param>
        void AI(NPCStateMachine nsm);
        void FindFrame(NPCStateMachine nPCStateMachine, int frameHeight);
    }
    /// <summary>
    /// NPC状态类，NPC每个状态都要实现这个类才能注册到状态机中
    /// </summary>
    public abstract class NPCState : IState
    {
        public NPC NPC;
        
        public abstract void AI(NPCStateMachine nsm);

        public virtual void FindFrame(NPCStateMachine nsm, int frameHeight) { }
    }
    /// <summary>
    /// NPC攻击状态类
    /// </summary>
    public abstract class NPCAttackState : IState
    {
        public NPC NPC;
        public abstract void AI(NPCStateMachine nsm);
        public virtual void FindFrame(NPCStateMachine nsm, int frameHeight) { }
    }
    public abstract class NPCState1 : IState
    {
        public NPC NPC;
        public abstract void AI(NPCStateMachine nsm);
        public virtual void FindFrame(NPCStateMachine nsm, int frameHeight) { }
    }
    public abstract class NPCState2 : IState
    {
        public NPC NPC;
        public abstract void AI(NPCStateMachine nsm);
        public virtual void FindFrame(NPCStateMachine nsm, int frameHeight) { }
    }
}
