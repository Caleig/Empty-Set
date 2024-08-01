using Microsoft.Build.Construction;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EmptySet.Projectiles.Weapons.Melee
{
	public class Lightning : ModProjectile
	{
        SpriteBatch sb;
        Vector2 targetVel;
        public override void SetDefaults()
        { 
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 5;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.damage = 4;
            Projectile.light = 0.5f;
            Projectile.scale = 1.3f;
            AIType = ProjectileID.Bullet;
        }
        private LightTree tree;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Dust dust = Dust.NewDustDirect(player.Center, 0, 0, DustID.Bee, 0, -1);
            dust.noGravity = true;
            dust.velocity = new Vector2(0, -3);
            NPC target = null;
            float distanceMax = 600f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, player.Center);
                    if (currentDistance < distanceMax)
                    {
                        if (Projectile.timeLeft <= 270)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
            }
            if (target != null)
            {
                targetVel = new Vector2((target.Center.X - Projectile.Center.X), (target.Center.Y - Projectile.Center.Y));
            }
            if (Projectile.ai[0] == 0)
            {
                tree = new LightTree(Main.rand);
                tree.Generate(Projectile.Center, target);
                Projectile.ai[0] = 1;
            }
        }
        public override void PostDraw(Color lightColor)
        {
            NPC target = null;
            float distanceMax = 600f;
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && !npc.friendly)
                {
                    // 计算与投射物的距离
                    float currentDistance = Vector2.Distance(npc.Center, Projectile.Center);
                    if (currentDistance < distanceMax)
                    {
                        if (Projectile.timeLeft <= 270)
                        {
                            distanceMax = currentDistance;
                            target = npc;
                        }
                    }
                }
            }
            if (target != null)
            {
                targetVel = new Vector2((target.Center.X - Projectile.Center.X), (target.Center.Y - Projectile.Center.Y));
            }
            tree.Draw(Projectile.Center - Main.screenPosition, new Vector2(0, 100));
            base.PostDraw(lightColor);
        }
    }
    public class LightTree
    {
        private class Node
        {
            public float rad, size, length;
            public List<Node> children;
            public Node(float rad, float size, float length)
            {
                this.rad = rad;
                this.size = size;
                this.length = length;
                this.children = new List<Node>();
            }
        };
        private Node root;
        private UnifiedRandom random;
        public LightTree(UnifiedRandom random)
        {
            // 一开始树的根节点为空，我们需要后续的构造去给它赋值
            root = null;
            this.random = random;
        }
        private float rand(float range)
        {
            return random.NextFloatDirection() * range;
        }
        private float rand2()
        {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return (float)Math.Max(0, Math.Sqrt(u) * Math.Cos(v) * 0.3 + 0.5);
        }
        public void Generate(Vector2 vector2, NPC target)
        {
            ;
            float distance = (vector2.Y - target.Center.Y);
            // 根节点生成，朝向0，粗细1，长度随机50中选
            root = new Node(0, 2f, 12f);
            root = _build(root, false);
        }
        private Node _build(Node node, bool isMain)
        {
            // 终止条件：树枝太细了，或者太短了
            if (node.size < 0.6f || node.length < 1) return node;
            float r = isMain ? MathHelper.Pi / 6f : MathHelper.Pi / 120f;
            Node main = new Node(rand(r), node.size * 0.98f, node.length);
            node.children.Add(_build(main, isMain));
            // 只有较小的几率出分支
            if (rand2() > 1.5f)
            {
                // 生成分支的时候长度变化不大，但是大小变化很大
                Node child = new Node(rand(MathHelper.Pi / 6f), node.size * 0.5f, node.length);
                node.children.Add(_build(child, false));
            }
            return node;
        }
        public void Draw(Vector2 pos, Vector2 vel)
        {
            _draw(pos, vel, root);
        }
        private void _draw(Vector2 pos, Vector2 vel, Node node)
        {
            // 树枝实际的方向向量
            Vector2 unit = (vel.ToRotation() + node.rad).ToRotationVector2();
            // 类似激光的线性绘制方法，绘制出树枝
            for (float i = 0; i <= node.length; i += 0.04f)
            {
                Main.spriteBatch.Draw(ModContent.Request<Texture2D>("EmptySet/Projectiles/Weapons/Melee/1").Value, pos + unit * i, new Rectangle(0, 0, 1, 1), Color.White * 0.5f, 0,
                    new Vector2(0.5f, 0.5f), Math.Max(node.size * 7, 0.2f), SpriteEffects.None, 0f);
            }
            // 递归到子节点进行绘制
            foreach (var child in node.children)
            {
                // 传递给子节点真实的位置和方向向量
                _draw(pos + unit * node.length, unit, child);
            }
        }
    }
}