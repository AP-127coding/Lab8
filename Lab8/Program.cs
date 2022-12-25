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
        public int Power { get { return power; } set { } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int HP { get { return hp; } set { } }
        public bool Vision { get { return vision; } set { } }
        public int Direction { get { return direction; } }
        public string Color { get { return color; } }
        public partial int[] Shoot(Map map, Tank tank) // стрельба танка
        {
            int[] mas = new int[2]; // массив для возврата координаты клетки
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
                            mas[0] = -1;
                            mas[1] = -1;
                            return mas;
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp, ytemp - 1).EnvHP > 0) // если клетка иммет здороье 
                            {
                                map.GetEnvironments(xtemp, ytemp - 1).Change(this); // вызов функции change у клетки
                                
                                
                                    mas[0] = xtemp;
                                    mas[1] = ytemp - 1;
                                    return mas;
                                
                            }
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
                            mas[0] = -1;
                            mas[1] = -1;
                            return mas;
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp, ytemp + 1).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp, ytemp + 1).Change(this);
                                
                                    mas[0] = xtemp;
                                    mas[1] = ytemp + 1;
                                    return mas;
                                
                            }
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
                            mas[0] = -1;
                            mas[1] = -1;
                            return mas;
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp - 1, ytemp).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp - 1, ytemp).Change(this);
                                
                                
                                    mas[0] = xtemp - 1;
                                    mas[1] = ytemp;
                                    return mas;
                                
                            }
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
                            mas[0] = -1;
                            mas[1] = -1;
                            return mas;
                        }
                        else
                        {
                            if (map.GetEnvironments(xtemp + 1, ytemp).EnvHP > 0)
                            {
                                map.GetEnvironments(xtemp + 1, ytemp).Change(this);
                                
                                
                                
                                    mas[0] = xtemp + 1;
                                    mas[1] = ytemp;
                                    return mas;
                                
                            }
                        }
                    }
                    break;
            }
            mas[0] = -1;
            mas[1] = -1;
            return mas;
        }
        public partial void Movement(Map map, Tank tank, char k) // движение танка 
        {

            switch (k)
            {
                case 'i':
                case 'w':
                    {
                        direction = 3;
                        if ((map.GetEnvironments(x - 1, y).EnvHP == 0 || map.GetEnvironments(x - 1, y).EnvHP == -2) && (x - 1 != tank.x || y != tank.y)) // если клетка проходимая и там не стоит танк
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
                        if ((map.GetEnvironments(x + 1, y).EnvHP == 0 || map.GetEnvironments(x +1 , y).EnvHP == -2 ) && (x + 1 != tank.x || y != tank.y))
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
                        if ((map.GetEnvironments(x, y + 1).EnvHP == 0 || map.GetEnvironments(x , y+1 ).EnvHP == -2) && (x != tank.x || y + 1 != tank.y))
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
                        if ((map.GetEnvironments(x, y - 1).EnvHP == 0 || map.GetEnvironments(x , y-1 ).EnvHP == -2 ) && (x != tank.x || y - 1 != tank.y))
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
        static public  Map MapSerialisation(Map map, int x, int y, int x2, int y2, bool flag)
        {
            if (flag == true)
            {
                for (int i = 1; i < 11; i++)
                {
                    for (int j = 1; j < 11; j++)
                    {
                        string celltank = "celltank" + Convert.ToString(i) + "_" + Convert.ToString(j) + ".json";
                        FileStream readcell1file = File.OpenRead(celltank);
                        DataContractJsonSerializer celldata = new DataContractJsonSerializer(map.environments[i, j].GetType());
                        map.environments[i, j] = celldata.ReadObject(readcell1file) as Environment;
                        readcell1file.Close();
                    }
                }

                return map;
            }

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    string celltank = "celltank" + Convert.ToString(i) + "_" + Convert.ToString(j) + ".json";
                    FileStream celltankfile = File.Create(celltank);
                    DataContractJsonSerializer celltankdata = new DataContractJsonSerializer(map.environments[i, j].GetType());
                    celltankdata.WriteObject(celltankfile, map.environments[i, j]);
                    celltankfile.Close();
                }
            }
            return map;
        }
        static public Tank TankSerialisation(Tank tank, bool flag)
        {
            if (flag == true)
            {
                FileStream tankdfile = File.OpenRead("tank.json");
                DataContractJsonSerializer tankddata = new DataContractJsonSerializer(typeof(BaseTank));
                Tank tankd1 = tankddata.ReadObject(tankdfile) as Tank;
                if (tankd1.Color == "Green")
                {
                    Tank tank1 = new SpeedTank(tankd1.X, tankd1.Y, tankd1.Direction);
                    tank1.Vision = tankd1.Vision;
                    tank1.HP = tankd1.HP;
                    tank1.Power = tankd1.Power;
                    tankdfile.Close();
                    return tank1;
                }
                if (tankd1.Color == "Orange")
                {
                    Tank tank1 = new BaseTank(tankd1.X, tankd1.Y, tankd1.Direction);
                    tank1.Vision = tankd1.Vision;
                    tank1.HP = tankd1.HP;
                    tank1.Power = tankd1.Power;
                    tankdfile.Close();
                    return tank1;
                }
                if (tankd1.Color == "Black")
                {
                    Tank tank1 = new PowerfulTank(tankd1.X, tankd1.Y, tankd1.Direction);
                    tank1.Vision = tankd1.Vision;
                    tank1.HP = tankd1.HP;
                    tank1.Power = tankd1.Power;
                    tankdfile.Close();
                    return tank1;
                }
                
                
            }
            FileStream tankfile = File.Create("tank.json");
            DataContractJsonSerializer tankdata = new DataContractJsonSerializer(tank.GetType());
            tankdata.WriteObject(tankfile, tank);
            tankfile.Close();

            return tank;
        }
        static public Tank Tank2Serialisation(Tank tank2, bool flag)
        {
            
            if (flag == true)
            {
                FileStream tankd2file = File.OpenRead("tank2.json");
                DataContractJsonSerializer tank2ddata = new DataContractJsonSerializer(typeof(BaseTank));
                Tank tankd2 = tank2ddata.ReadObject(tankd2file) as Tank;
                if (tankd2.Color == "Green")
                {
                    Tank tank22 = new SpeedTank(tankd2.X, tankd2.Y, tankd2.Direction);
                    tank22.Vision = tankd2.Vision;
                    tank22.HP = tankd2.HP;
                    tank22.Power = tankd2.Power;
                    tankd2file.Close();
                    return tank22;
                }
                if (tankd2.Color == "Orange")
                {
                    Tank tank22 = new BaseTank(tankd2.X, tankd2.Y, tankd2.Direction);
                    tank22.Vision = tankd2.Vision;
                    tank22.HP = tankd2.HP;
                    tank22.Power = tankd2.Power;
                    tankd2file.Close();
                    return tank22;
                }
                if (tankd2.Color == "Black")
                {
                    Tank tank22 = new PowerfulTank(tankd2.X, tankd2.Y, tankd2.Direction);
                    tank22.Vision = tankd2.Vision;
                    tank22.HP = tankd2.HP;
                    tank22.Power = tankd2.Power;
                    tankd2file.Close();
                    return tank22;
                }

              
            }
            FileStream tank2file = File.Create("tank2.json");
            DataContractJsonSerializer tank2data = new DataContractJsonSerializer(tank2.GetType());
            tank2data.WriteObject(tank2file, tank2);
            tank2file.Close();

            return tank2;
        }
    }
}
