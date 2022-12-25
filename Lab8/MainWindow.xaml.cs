using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tank tank1;
        Tank tank2;
        Map map = new Map();
        int[] arr = new int[2];
        RotateTransform right = new RotateTransform(90);
        RotateTransform left = new RotateTransform(270);
        RotateTransform up = new RotateTransform(0);
        RotateTransform down = new RotateTransform(180);
        System.Windows.Threading.DispatcherTimer formTimer = new System.Windows.Threading.DispatcherTimer();
        Image[,] im = new Image[12, 12]; // массив изображений карты
        
        public MainWindow() // конструктор
        {
            InitializeComponent();

        }
        public MainWindow(Tank t1,Tank t2,Map map) // конструктор
        {
            this.map = map;
            tank1 = t1;
            tank2 = t2;
            InitializeComponent();
        }
        public MainWindow(Tank t1, Tank t2)  // конструктор
        {
            tank1 = t1;
            tank2 = t2;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) // сериализация 
        {
            Grid.SetColumn(tankplayer1, tank1.Y);
            Grid.SetRow(tankplayer1, tank1.X);
            Grid.SetColumn(tankplayer2, tank2.Y);
            Grid.SetRow(tankplayer2, tank2.X);
            if (tank1.GetType().Name == "BaseTank")
            {
                tankplayer1.Source = basetank.Source;
            }
            if (tank1.GetType().Name == "SpeedTank")
            {
                tankplayer1.Source = speedytank.Source;
            }
            if (tank1.GetType().Name == "PowerfulTank")
            {
                tankplayer1.Source = powerfultank.Source;
            }
            if (tank2.GetType().Name == "BaseTank")
            {
                tankplayer2.Source = basetank.Source;
            }
            if (tank2.GetType().Name == "SpeedTank")
            {
                tankplayer2.Source = speedytank.Source;
            }
            if (tank2.GetType().Name == "PowerfulTank")
            {
                tankplayer2.Source = powerfultank.Source;
            }
            Image[,] images =
          {
            {brick0_0,brick0_1,brick0_2,brick0_3,brick0_4,brick0_5,brick0_6,brick0_7,brick0_8,brick0_9,brick0_10,brick0_11},
            { brick1_0,wood,wood,grass,grass,grass,grass,wood,wood,lava1_9,lava1_10,brick1_11},
            { brick2_0,wood,wood,grass,grass,grass,grass,wood,brick2_8,wood,lava2_10,brick2_11},
            { brick3_0,beton3_1,brick3_2,brick3_3,wood,grass,grass,beton3_7,brick3_8,wood,wood,brick3_11},
            { brick4_0,beton4_1,beton4_2,beton4_3,beton4_4,brick4_5,brick4_6,beton4_7,beton4_8,beton4_9,beton4_10, brick4_11},
            { brick5_0,wood,water5_2,water5_3,beton5_4,glass5_5,glass5_6,beton5_7,beton5_8,water5_9,beton5_10, brick5_11},
            { brick6_0,wood,water6_2,beton6_3,beton6_4,glass6_5,glass6_6,beton6_7,beton6_8,water6_9,beton6_10, brick6_11},
            { brick7_0,wood,water7_2,beton7_3,beton7_4,brick7_5,brick7_6,beton7_7,water7_8,water7_9,beton7_10, brick7_11},
            { brick8_0,wood,beton8_2,beton8_3,beton8_4,grass,grass,beton8_7,beton8_8,beton8_9,beton8_10, brick8_11},
            { brick9_0, lava9_1, wood,beton9_3,wood,grass,grass,grass,grass,beton9_9,wood, brick9_11},
            { brick10_0, lava10_1,lava10_2,wood,wood,grass,grass,grass,grass,brick10_9,wood, brick10_11},
            { brick11_0, brick11_1,brick11_2,brick11_3,brick11_4,brick11_5,brick11_6,brick11_7,brick11_8,brick11_9,brick11_10, brick11_11}
            };
            
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    im[i, j] = images[i, j];
                }
            }
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    if (map.environments[i, j].EnvHP <= 60 && map.environments[i, j].EnvHP > 1)
                    {
                        im[i, j].Source = damaged_beton.Source;
                    }
                    if (map.environments[i, j].EnvHP == 0)
                    {
                        im[i, j].Source = wood.Source;
                    }
                }
            }
            
            formTimer.Interval = new TimeSpan(100); // таймер для проверки состояния игры 
            formTimer.Start();
            formTimer.Tick += new EventHandler(FormTimer_Tick);
        }
        private void FormTimer_Tick(object sender, EventArgs e)
        {
            if (tank1.HP <= 0) // условие окончания игры 
            {
                MessageBox.Show("GAME OVER!\nPlayer 2 Win", "GAME", MessageBoxButton.OK, MessageBoxImage.Information);
                Menu menu = new Menu();
                menu.Show();
                formTimer.Stop();
                Close();
            }
            if (tank2.HP <= 0) // условие окончания игры 
            {
                MessageBox.Show("GAME OVER!\nPlayer 1 Win", "GAME", MessageBoxButton.OK, MessageBoxImage.Information);
                Menu menu = new Menu();
                menu.Show();
                formTimer.Stop();
                Close();
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) // реализация управления 
        {
            if (e.Key == Key.X)
            {
                arr = tank1.Shoot(map, tank2);

                if (arr[0] != -1 && arr[1] != -1)
                {
                    if (map.environments[arr[0], arr[1]].EnvHP <= 60 && map.environments[arr[0], arr[1]].EnvHP > 0)
                    {
                        im[arr[0], arr[1]].Source = damaged_beton.Source;
                    }
                    if (map.environments[arr[0], arr[1]].EnvHP == 0)
                    {
                        im[arr[0], arr[1]].Source = wood.Source;
                    }
                }
            }
            if (e.Key == Key.M)
            {
                arr = tank2.Shoot(map, tank1);

                if (arr[0] != -1 && arr[1] != -1)
                {
                    if (map.environments[arr[0], arr[1]].EnvHP <= 60 && map.environments[arr[0], arr[1]].EnvHP > 0)
                    {
                        im[arr[0], arr[1]].Source = damaged_beton.Source;
                    }
                    if (map.environments[arr[0], arr[1]].EnvHP == 0)
                    {
                        im[arr[0], arr[1]].Source = wood.Source;
                    }
                }
             
            }
            if (e.Key == Key.Escape)
            {
                Menu menu = new Menu(this,tank1,tank2,map);
                menu.Show();
                Hide();
            }
            if (e.Key == Key.W)
            {
                char w = 'w';
                tank1.Movement(map, tank2, w);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = up;
            }
            if (e.Key == Key.I)
            {
                char i = 'i';
                tank2.Movement(map, tank1, i);
                Grid.SetColumn(tankplayer2, tank2.Y);
                Grid.SetRow(tankplayer2, tank2.X);
                tankplayer2.RenderTransform = up;
            }
            if (e.Key == Key.A)
            {
                char a = 'a';
                tank1.Movement(map, tank2, a);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = left;

            }
            if (e.Key == Key.J)
            {
                char j = 'j';
                tank2.Movement(map, tank1, j);
                Grid.SetColumn(tankplayer2, tank2.Y);
                Grid.SetRow(tankplayer2, tank2.X);
                tankplayer2.RenderTransform = left;
            }
            if (e.Key == Key.S)
            {
                char s = 's';
                tank1.Movement(map, tank2, s);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = down;
            }
            if (e.Key == Key.K)
            {
                char k = 'k';
                tank2.Movement(map, tank1, k);
                Grid.SetColumn(tankplayer2, tank2.Y);
                Grid.SetRow(tankplayer2, tank2.X);
                tankplayer2.RenderTransform = down;
            }
            if (e.Key == Key.D)
            {
                char d = 'd';
                tank1.Movement(map, tank2, d);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = right;
            }
            if (e.Key == Key.L)
            {
                char l = 'l';
                tank2.Movement(map, tank1, l);
                Grid.SetColumn(tankplayer2, tank2.Y);
                Grid.SetRow(tankplayer2, tank2.X);
                tankplayer2.RenderTransform = right;
            }

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
