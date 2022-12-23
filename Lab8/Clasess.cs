using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    [Serializable]
    public abstract partial class Tank : IShoot, IMovement
    {
        protected int hp; // здоровье танка
        protected int power; // мощность танка
        protected string color; // цвет танка 
        protected bool vision; // видимость танка
        protected int direction; // направление танка (1 - влево, 2 - вправо, 3 - вверх, 4 - вниз)
        protected int x; // положение по оси x
        protected int y; // положение по оси y

        protected Tank(int hp, int power, string color, bool vision)
        {
            this.hp = hp;
            this.power = power;
            this.color = color;
            this.vision = vision;
        }
        public partial int[] Shoot(Map map, Tank tank);

        public partial void Movement(Map map, Tank tank, char k);

        public virtual void OwnMovement() { }
    }
    [Serializable]
    public partial class BaseTank : Tank
    {
        public BaseTank(int x, int y, int dir) : base(120, 70, "Orange", true)
        {
            base.x = x;
            base.y = y;
            base.direction = dir;
        }

        public override partial void OwnMovement();
    }
    [Serializable]
    public partial class PowerfulTank : Tank
    {
        public PowerfulTank(int x, int y, int dir) : base(200, 80, "Black", true)
        {
            base.x = x;
            base.y = y;
            base.direction = dir;
        }

        public override partial void OwnMovement();
    }
    [Serializable]
    public partial class SpeedTank : Tank
    {
        public SpeedTank(int x, int y, int dir) : base(100, 55, "Green", true)
        {
            base.x = x;
            base.y = y;
            base.direction = dir;
        }

        public override partial void OwnMovement();
    }
    [Serializable]
    abstract public partial class Environment
    {
        protected bool bulletPassability; // проходимость пули
        protected int envhp; // "здоровье" клетки
        protected int damage; // урон клетки
        protected bool envVision; // видимость танка в клетке

        public int EnvHP { get { return envhp; } }
        public bool EnvVision { get { return envVision; } }
        public int Damage { get { return damage; } }
        public bool BulletPassability { get { return bulletPassability; } }

        protected Environment(bool bulletPassability, int envhp, int damage, bool envVision)
        {
            this.bulletPassability = bulletPassability;
            this.envhp = envhp;
            this.damage = damage;
            this.envVision = envVision;
        }

        public virtual partial void Change(Tank tank);
    }
    /* здоровье = -1 показывает, что клетку нельзя пройти и сломать, 
         * 0 - это значит, что клетку танк может пройти,
         * а положительное значение показывает, что пройти её нельзя, но можно разрушить*/
    [Serializable]
    public partial class Default : Environment // обычная клетка (не меняется)
    {
        public Default() : base(true, 0, 0, true)
        {

        }
    }
    [Serializable]
    public partial class Water : Environment // вода (не меняется)
    {
        public Water() : base(true, -1, 0, true)
        {

        }
    }
    [Serializable]
    public partial class Brick : Environment // кирпич (не меняется)
    {
        public Brick() : base(false, -1, 0, true)
        {

        }
    }
    [Serializable]
    public partial class Beton : Environment // бетон 
    {
        public Beton() : base(false, 110, 0, true)
        {

        }
        public override partial void Change(Tank tank);
    }
    [Serializable]
    public class Grass : Environment // трава (не меняется)
    {

        public Grass() : base(true, 0, 0, false)
        {

        }
    }
    [Serializable]
    public partial class Glass : Environment // стекло
    {
        public Glass() : base(false, 50, 0, true)
        {

        }
        public override partial void Change(Tank tank);
    }
    [Serializable]
    public class Lava : Environment // лава (не меняется)
    {
        public Lava() : base(true, 0, 15, true)
        {

        }
    }
    [Serializable]
    public partial class Map
    {
        static Brick brick = new Brick();
        static Default defaults = new Default();
        static Water water = new Water();
        static Grass grass = new Grass();
        static Lava lava = new Lava();
        static Glass g56 = new Glass(), g66 = new Glass(), g55 = new Glass(), g65 = new Glass();
        static Beton b18 = new Beton(), b17 = new Beton(), b27 = new Beton(), b23 = new Beton(), b37 = new Beton(), b35 = new Beton(), b34 = new Beton(), b33 = new Beton(),
        b32 = new Beton(), b47 = new Beton(), b46 = new Beton(), b45 = new Beton(), b44 = new Beton(), b43 = new Beton(), b78 = new Beton(), b77 = new Beton(), b76 = new Beton(),
        b75 = new Beton(), b74 = new Beton(), b73 = new Beton(), b87 = new Beton(), b86 = new Beton(), b85 = new Beton(), b83 = new Beton(), b97 = new Beton(), b93 = new Beton(),
        b92 = new Beton(), b107 = new Beton(), b106 = new Beton(), b105 = new Beton(), b104 = new Beton(), b103 = new Beton();
        public Grass EnvGrass { get { return grass; } }
        public Environment[,] environments = {
        {brick, brick, brick, brick, brick, brick, brick, brick, brick, brick, brick, brick},
        {brick, defaults, defaults, grass, grass, grass, grass, defaults, defaults, lava, lava, brick},
        {brick, defaults, defaults, grass, grass, grass, grass, defaults, brick, defaults, lava, brick},
        {brick, b18, brick, brick, defaults, grass, grass, b78, brick, defaults, defaults, brick},
        {brick, b17, b27, b37, b47, brick, brick, b77, b87, b97, b107, brick},
        {brick, defaults, water, water, b46, g56, g66, b76, b86, water, b106, brick},
        {brick, defaults, water, b35, b45, g55, g65, b75, b85, water, b105, brick},
        {brick, defaults, water, b34, b44, brick, brick, b74, water, water, b104, brick},
        {brick, defaults, b23, b33, b43, grass, grass, b73, b83, b93, b103, brick},
        {brick, lava, defaults, b32, defaults, grass, grass, grass, grass, b92, defaults, brick},
        {brick, lava, lava, defaults, defaults, grass, grass, grass, grass, brick, defaults, brick},
        {brick, brick, brick, brick, brick, brick, brick, brick, brick, brick, brick, brick}
        };
        public partial Environment GetEnvironments(int x, int y);
    }
}
