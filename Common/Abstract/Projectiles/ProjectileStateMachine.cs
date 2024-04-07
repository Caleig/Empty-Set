using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EmptySet.Common.Abstract.Projectiles
{
    /// <summary>
    /// Projectile有限状态机
    /// </summary>
    public abstract class ProjectileStateMachine : ModProjectile
    {
        /// <summary>
        /// Projectile所有者
        /// </summary>
        public Player owner;

        /// <summary>
        /// 是否寻找到目标
        /// </summary>
        public bool foundTarget;
        /// <summary>
        /// 目标npc
        /// </summary>
        public NPC target;
        //是否允许状态执行
        public bool allowProjectileStates1AI = true;
        public bool allowProjectileStates2AI = false;

        public State currentState1 => ProjectileStates1[State1 - 1];
        private List<State> ProjectileStates1 = new List<State>();
        private Dictionary<string, int> stateDict1 = new Dictionary<string, int>();

        public State currentState2 => ProjectileStates2[State2 - 1];
        private List<State> ProjectileStates2 = new List<State>();
        private Dictionary<string, int> stateDict2 = new Dictionary<string, int>();

        /// <summary>
        /// Projectile状态1
        /// </summary>
        private int State1
        {
            get { return (int)Projectile.ai[0]; }
            set { Projectile.ai[0] = (int)value; }
        }
        /// <summary>
        /// Projectile状态2
        /// </summary>
        private int State2
        {
            get { return (int)Projectile.ai[1]; }
            set { Projectile.ai[1] = (int)value; }
        }
        /// <summary>
        /// Projectile状态计时器1
        /// </summary>
        public int Timer1
        {
            get { return (int)Projectile.localAI[0]; }
            set { Projectile.localAI[0] = value; }
        }
        /// <summary>
        /// Projectile计时器2
        /// </summary>
        public int Timer2
        {
            get { return (int)Projectile.localAI[1]; }
            set { Projectile.localAI[1] = value; }
        }
        /// <summary>
        /// 注册状态
        /// </summary>
        /// <typeparam name="T">需要注册的<see cref="State"/>类</typeparam>
        /// <param name="state">需要注册的<see cref="State"/>类的实例</param>
        protected void RegisterState<T>(T state) where T : State
        {
            var typename = typeof(T).BaseType.Name;
            var name = typeof(T).FullName;
            if (typename == typeof(ProjectileState1).Name)
            {
                if (stateDict1.ContainsKey(name)) throw new ArgumentException(name + "状态1已经注册");
                ProjectileStates1.Add(state);
                stateDict1.Add(name, ProjectileStates1.Count);
            }
            else if (typename == typeof(ProjectileState2).Name)
            {
                if (stateDict2.ContainsKey(name)) throw new ArgumentException(name + "状态2已经注册");
                ProjectileStates2.Add(state);
                stateDict2.Add(name, ProjectileStates2.Count);
            }
        }
        /// <summary>
        /// 把当前状态变为指定的Projectile状态实例
        /// </summary>
        /// <typeparam name="T">注册过的<see cref="State"/>类名</typeparam>
        public void SetState<T>() where T : State
        {
            var typename = typeof(T).BaseType.Name;
            var name = typeof(T).FullName;
            if (typename == typeof(ProjectileState1).Name)
            {
                if (!stateDict1.ContainsKey(name)) throw new ArgumentException(name + "状态1不存在");
                State1 = stateDict1[name];
            }
            else if (typename == typeof(ProjectileState2).Name)
            {
                if (!stateDict2.ContainsKey(name)) throw new ArgumentException(name + "状态2不存在");
                State2 = stateDict2[name];
            }
        }
        /// <summary>
        /// 初始化函数，用于注册Projectile状态
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// 把AI函数封住，这样在子类无法重写AI函数，只能用before和after函数
        /// </summary>
        public sealed override void AI()
        {
            if (State1 == 0)
            {
                Initialize();
                State1 = 1;
                State2 = 1;
            }
            owner = Main.player[Projectile.owner];
            AIBefore(this);
            if (ProjectileStates1.Count > 0 && allowProjectileStates1AI) 
            {
                currentState1.AI(this);
            }
            if (ProjectileStates2.Count > 0 && allowProjectileStates2AI)
            {
                currentState2.AI(this);
            }
            AIAfter(this);
        }
        /// <summary>
        /// 在状态机执行之前要执行的代码，可以重写
        /// </summary>
        public virtual void AIAfter(ProjectileStateMachine ProjectileStateMachine) { }
        /// <summary>
        /// 在状态机执行之后要执行的代码，可以重写
        /// </summary>
        public virtual void AIBefore(ProjectileStateMachine ProjectileStateMachine) { }
    }
    /// <summary>
    /// Projectile状态的接口
    /// </summary>
    public interface State
    {
        // AI函数接受一个ProjectileStateMachine类型的modProjectile对象
        void AI(ProjectileStateMachine p);
    }
    /// <summary>
    /// Projectile状态类，Projectile每个状态都要实现这个类才能注册到状态机中
    /// </summary>
    public abstract class ProjectileState1 : State
    {
        public Projectile Projectile;
        /// <summary>
        /// 需要有切换下一状态的条件
        /// </summary>
        /// <param name="Projectile"></param>
        public abstract void AI(ProjectileStateMachine p);
    }
    /// <summary>
    /// Projectile攻击状态类
    /// </summary>
    public abstract class ProjectileState2 : State
    {
        public Projectile Projectile;
        /// <summary>
        /// 需要有切换下一状态的条件
        /// </summary>
        /// <param name="Projectile"></param>
        public abstract void AI(ProjectileStateMachine p);
    }
}