using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab8
{
    public abstract partial class Tank
    {
        public int Power { get { return power; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int HP { get { return hp; } }
        public bool Vision { get { return vision; } }
        public int Direction { get { return direction; } }
        public partial void Shoot(Map map, Tank tank) // стрельба танка
        {
            int xtemp = x;
            int ytemp = y;
            switch (direction)
            {
                case 1:
                    {
                        while (map.GetEnvironments(xtemp, ytemp - 1).BulletPassability == true && (tank.x != xtemp || tank.y != ytemp - 1)) // пока клетка пропускает пулю и в клетке не стоит танк
                        {
                            ytemp--; // переход на следующую клетку
                        }
                        if (tank.x == xtemp && tank.y == ytemp - 1) // если в клетке стоит танк
                        {
                            tank.hp -= power;
                            Console.WriteLine("Есть пробитие! ");
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp, ytemp - 1).EnvHP > 0) // если клетка иммет здороье 
                            {
                                map.GetEnvironments(xtemp, ytemp - 1).Change(this); // вызов функции change у клетки
                                if (map.GetEnvironments(xtemp, ytemp - 1).EnvHP == 0)
                                {
                                    map.environments[xtemp, ytemp - 1] = map.GetEnvironments(1, 1); // делает клетку дефолтной
                                    Console.WriteLine("Клетка стала дефолтной ");
                                }
                            }
                            else { Console.WriteLine("Снаряд прилетел в клетку, которую невозвожно разрушить. "); }
                        }
                    }
                    break;
                case 2:
                    {
                        while (map.GetEnvironments(xtemp, ytemp + 1).BulletPassability == true && (tank.x != xtemp || tank.y != ytemp + 1))
                        {
                            ytemp++;
                        }
                        if (tank.x == xtemp && tank.y == ytemp + 1)
                        {
                            tank.hp -= power;
                            Console.WriteLine("Есть пробитие! ");
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp, ytemp + 1).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp, ytemp + 1).Change(this);
                                if (map.GetEnvironments(xtemp, ytemp + 1).EnvHP == 0)
                                {
                                    map.environments[xtemp, ytemp + 1] = map.GetEnvironments(1, 1);
                                    Console.WriteLine("Клетка стала дефолтной! ");
                                }
                            }
                            else { Console.WriteLine("Снаряд прилетел в клетку, которую невозвожно разрушить. "); }
                        }
                    }
                    break;
                case 3:
                    {
                        while (map.GetEnvironments(xtemp - 1, ytemp).BulletPassability == true && (tank.x != xtemp - 1 || tank.y != ytemp))
                        {
                            xtemp--;
                        }
                        if (tank.x == xtemp - 1 && tank.y == ytemp)
                        {
                            tank.hp -= power;
                            Console.WriteLine("Есть пробитие! ");
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp - 1, ytemp).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp - 1, ytemp).Change(this);
                                if (map.GetEnvironments(xtemp - 1, ytemp).EnvHP == 0)
                                {
                                    map.environments[xtemp - 1, ytemp] = map.GetEnvironments(1, 1);
                                    Console.WriteLine("Клетка стала дефолтной! ");
                                }
                            }
                            else { Console.WriteLine("Снаряд прилетел в клетку, которую невозвожно разрушить. "); }
                        }
                    }
                    break;
                case 4:
                    {
                        while (map.GetEnvironments(xtemp + 1, ytemp).BulletPassability == true && (tank.x != xtemp + 1 || tank.y != ytemp))
                        {
                            xtemp++;
                        }
                        if (tank.x == xtemp + 1 && tank.y == ytemp)
                        {
                            tank.hp -= power;
                            Console.WriteLine("Есть пробитие! ");
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp + 1, ytemp).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp + 1, ytemp).Change(this);
                                
                                if (map.GetEnvironments(xtemp + 1, ytemp).EnvHP == 0)
                                {
                                    
                                    map.environments[xtemp + 1, ytemp] = map.GetEnvironments(1, 1);
                                    Console.WriteLine("Клетка стала дефолтной ");
                                }
                            }
                            else { Console.WriteLine("Снаряд прилетел в клетку, которую невозвожно разрушить. "); }
                        }
                    }
                    break;
            }

        }
        public partial void Movement(Map map, Tank tank, char k) // движение танка 
        {

            switch (k)
            {
                case 'i':
                case 'w':
                    {
                        direction = 3;
                        if (map.GetEnvironments(x - 1, y).EnvHP == 0 && (x - 1 != tank.x || y != tank.y)) // если клетка проходимая и там не стоит танк
                        {
                            OwnMovement(); // вызов виртуальной функции
                            x--;
                            hp -= map.GetEnvironments(x, y).Damage; // танк получает урон от клетки 
                            Console.WriteLine($"Танк переместился на клетку({x},{y})");
                            if (map.GetEnvironments(x, y).EnvVision == false) // если клетка скрывает танк
                            {
                                vision = false; // танк становится невидемым
                                Console.WriteLine("Танк невидимый! ");
                            }
                            else { vision = true; } // в любой другой клетке видимость танка есть
                        }
                        else
                        {
                            Console.WriteLine("Танк не сдвинулся! В этом направлении препятствие или танк.");
                        }

                    }
                    break;

                case 'k':
                case 's':
                    {
                        direction = 4;
                        if (map.GetEnvironments(x + 1, y).EnvHP == 0 && (x + 1 != tank.x || y != tank.y))
                        {
                            OwnMovement();
                            x++;
                            hp -= map.GetEnvironments(x, y).Damage;
                            Console.WriteLine($"Танк переместился на клетку({x},{y})");
                            if (map.GetEnvironments(x, y).EnvVision == false)
                            {
                                vision = false;
                                Console.WriteLine("Танк невидимый! ");
                            }
                            else { vision = true; }
                        }
                        else
                        {
                            Console.WriteLine("Танк не сдвинулся! В этом направлении препятствие или танк.");
                        }
                    }
                    break;

                case 'l':
                case 'd':
                    {
                        direction = 2;
                        if (map.GetEnvironments(x, y + 1).EnvHP == 0 && (x != tank.x || y + 1 != tank.y))
                        {
                            OwnMovement();
                            y++;
                            hp -= map.GetEnvironments(x, y).Damage;
                            Console.WriteLine($"Танк переместился на клетку({x},{y})");
                            if (map.GetEnvironments(x, y).EnvVision == false)
                            {
                                vision = false;
                                Console.WriteLine("Танк невидимый! ");
                            }
                            else { vision = true; }
                        }
                        else
                        {
                            Console.WriteLine("Танк не сдвинулся! В этом направлении препятствие или танк.");
                        }
                    }
                    break;

                case 'j':
                case 'a':
                    {
                        direction = 1;
                        if (map.GetEnvironments(x, y - 1).EnvHP == 0 && (x != tank.x || y - 1 != tank.y))
                        {
                            OwnMovement();
                            y--;
                            hp -= map.GetEnvironments(x, y).Damage;
                            Console.WriteLine($"Танк переместился на клетку({x},{y})");
                            if (map.GetEnvironments(x, y).EnvVision == false)
                            {
                                vision = false;
                                Console.WriteLine("Танк невидимый! ");
                            }
                            else { vision = true; }
                        }
                        else
                        {
                            Console.WriteLine("Танк не сдвинулся! В этом направлении препятствие или танк.");
                        }
                    }
                    break;
            }
        }
    }
    public partial class BaseTank
    {

        public override partial void OwnMovement() // замедление танка 
        {
            Thread.Sleep(300);
        }
    }
    partial class PowerfulTank
    {

        public override partial void OwnMovement() // замедление танка 
        {
            Thread.Sleep(500);
        }
    }
    partial class SpeedTank
    {

        public override partial void OwnMovement() // замедление танка 
        {
            Thread.Sleep(100);
        }
    }
    abstract public partial class Environment
    {
        public virtual partial void Change(Tank tank) // виртуальная функция для изменения клетки
        {
            Console.WriteLine("Свойства клетки изменились");
        }
    }
    public partial class Beton
    {
        public override partial void Change(Tank tank) // виртуальная функция для изменения клетки
        {
            if (tank.Power >= envhp)
            {
                envhp = 0;
                bulletPassability = true;
                Console.WriteLine("Бетон разрушен! ");
            }
            else
            {
                envhp -= tank.Power;
                Console.WriteLine("Бетон повреждён!");
            }

        }
    }
    public partial class Glass
    {
        public override partial void Change(Tank tank) // виртуальная функция для изменения клетки
        {
            if (tank.Power >= envhp)
            {
                envhp = 0;
                bulletPassability = true;
                Console.WriteLine("Стекло разрушено! ");
            }
            else
            {
                envhp -= tank.Power;
                Console.WriteLine("Стекло повреждено!");
            }
        }
    }

    public partial class Map
    {
        public partial Environment GetEnvironments(int x, int y) // получение клетки по координатам
        {
            return environments[x, y];
        }
    }
    class Program
    {
        static Map MapSerialisation(Map map, int x, int y, int x2, int y2, bool flag)
        {
            if (flag == true)
            {
                FileStream readcell1file = File.OpenRead("celltank1.json");
                DataContractJsonSerializer celldata = new DataContractJsonSerializer(map.environments[x, y].GetType());
                map.environments[x, y] = celldata.ReadObject(readcell1file) as Environment;

                FileStream readcell2file = File.OpenRead("celltank1.json");
                DataContractJsonSerializer celldata2 = new DataContractJsonSerializer(map.environments[x2, y2].GetType());
                map.environments[x, y] = celldata2.ReadObject(readcell2file) as Environment;

                readcell1file.Close();
                readcell2file.Close();
                return map;
            }

            FileStream celltankfile = File.Create("celltank1.json");
            FileStream celltank2file = File.Create("celltank2.json");


            DataContractJsonSerializer celltankdata = new DataContractJsonSerializer(map.environments[x, y].GetType());
            celltankdata.WriteObject(celltankfile, map.environments[x, y]);

            DataContractJsonSerializer celltank2data = new DataContractJsonSerializer(map.environments[x2, y2].GetType());
            celltank2data.WriteObject(celltank2file, map.environments[x2, y2]);

            celltank2file.Close();
            celltankfile.Close();

            return map;
        }
        static Tank TankSerialisation(Tank tank, bool flag)
        {
            if (flag == true)
            {
                FileStream tankdfile = File.OpenRead("tank.json");
                DataContractJsonSerializer tankddata = new DataContractJsonSerializer(tank.GetType());
                Tank tankd1 = tankddata.ReadObject(tankdfile) as Tank;
                tankdfile.Close();
                return tankd1;
            }
            FileStream tankfile = File.Create("tank.json");
            DataContractJsonSerializer tankdata = new DataContractJsonSerializer(tank.GetType());
            tankdata.WriteObject(tankfile, tank);
            tankfile.Close();

            return tank;
        }
        static Tank Tank2Serialisation(Tank tank2, bool flag)
        {
            if (flag == true)
            {
                FileStream tankd2file = File.OpenRead("tank2.json");
                DataContractJsonSerializer tank2ddata = new DataContractJsonSerializer(tank2.GetType());
                Tank tankd2 = tank2ddata.ReadObject(tankd2file) as Tank;
                tankd2file.Close();
                return tankd2;
            }
            FileStream tank2file = File.Create("tank2.json");
            DataContractJsonSerializer tank2data = new DataContractJsonSerializer(tank2.GetType());
            tank2data.WriteObject(tank2file, tank2);
            tank2file.Close();

            return tank2;
        }
        static Tank ChooseTank1(Tank tank) // выбор танка для 1 игрока
        {
            string p1;
            int player1 = 0;
            while (true)
            {
                Console.WriteLine("Выберите танк:");
                Console.WriteLine("Игрок 1:\n1 - Обычный танк\n2 - Быстрый танк\n3 - Мощный танк");
                p1 = Console.ReadLine();
                try
                {
                    player1 = int.Parse(p1);
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод.");
                    continue;
                }
                if (player1 == 1 || player1 == 2 || player1 == 3)
                {
                    switch (player1)
                    {
                        case 1: break;
                        case 2: tank = new SpeedTank(1, 1, 2); break;
                        case 3: tank = new PowerfulTank(1, 1, 2); break;
                    }
                    return tank;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод.");
                    continue;
                }
            }

        }
        static Tank ChooseTank2(Tank tank2) // выбор танка для 2 игрока
        {
            string p2;
            int player2 = 0;
            while (true)
            {
                Console.WriteLine("Выберите танк:");
                Console.WriteLine("Игрок 2:\n1 - Обычный танк\n2 - Быстрый танк\n3 - Мощный танк");
                p2 = Console.ReadLine();
                try
                {
                    player2 = int.Parse(p2);
                }
                catch
                {
                    Console.WriteLine("Неправильный ввод.");
                    continue;
                }
                if (player2 == 1 || player2 == 2 || player2 == 3)
                {
                    switch (player2)
                    {
                        case 1: break;
                        case 2: tank2 = new SpeedTank(10, 10, 3); break;
                        case 3: tank2 = new PowerfulTank(10, 10, 3); break;
                    }
                    return tank2;
                }
                else
                {
                    Console.WriteLine("Неправильный ввод.");
                    continue;
                }
            }
        }
        static void Actions()
        {
            Map map = new Map();
            Tank tank = new BaseTank(1, 1, 2);
            Tank tank2 = new BaseTank(10, 10, 3);

            Console.WriteLine("Управление\nИгрок 1: w - вверх, s - вниз, d - вправо, a - влево, x - выстрел\nИгрок 2: i - вверх, k - вниз, j - влево, l - вправо, m - выстрел\n");
            tank = ChooseTank1(tank);
            tank2 = ChooseTank2(tank2);
            Console.WriteLine("Для выхода с сохранением нажмите q");
            Console.WriteLine("Чтобы продолжить игру с предыдущего сохранения - нажмите z\n");
            Console.WriteLine("Игра началась");

            while (tank.HP > 0 && tank2.HP > 0) // игра может продолжаться только тогда, когда у такнков есть здоровье
            {
                string g;
                char k;
                try
                {
                    g = Console.ReadLine(); // считывание кнопок
                    if (g.Length > 1)
                    {
                        Console.WriteLine("Таких кнопок нет в управлении.");
                        continue;
                    }
                    k = Convert.ToChar(g);
                }
                catch
                {
                    Console.WriteLine("Ошибка при вводе");
                    continue;
                }
                switch (k) // вызов функциий, соответствующих клавиш
                {
                    case 'i': tank2.Movement(map, tank, k); break;
                    case 'w': tank.Movement(map, tank2, k); break;
                    case 'k': tank2.Movement(map, tank, k); break;
                    case 's': tank.Movement(map, tank2, k); break;
                    case 'l': tank2.Movement(map, tank, k); break;
                    case 'd': tank.Movement(map, tank2, k); break;
                    case 'j': tank2.Movement(map, tank, k); break;
                    case 'a': tank.Movement(map, tank2, k); break;
                    case 'x': tank.Shoot(map, tank2); break;
                    case 'm': tank2.Shoot(map, tank); break;
                    case 'q': // сериализация  
                        {
                            try
                            {
                                map = MapSerialisation(map, tank.X, tank.Y, tank2.X, tank2.Y, false);
                                tank = TankSerialisation(tank, false);
                                tank2 = Tank2Serialisation(tank2, false);
                                Console.WriteLine("Данные успешно сохранены!");
                                Process.GetCurrentProcess().Kill();
                            }
                            catch
                            {
                                Console.WriteLine("Данные не сохранены!");
                            }

                        };
                        break;
                    case 'z': // десериализация 
                        {
                            try
                            {
                                tank = TankSerialisation(tank, true);
                                tank2 = Tank2Serialisation(tank2, true);
                                map = MapSerialisation(map, tank.X, tank.Y, tank2.X, tank2.Y, true);
                                Console.WriteLine("Данные прошлой игры востановлены!");
                            }
                            catch
                            {
                                Console.WriteLine("Данные прошлой игры не сохранены!");
                            }
                        }
                        break;
                    default: Console.WriteLine("Неправильный ввод!"); break;
                }
            }
            Console.WriteLine("GAME OVER!");
        }
        
    }
}
