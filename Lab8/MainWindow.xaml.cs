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
        Tank tank1 = new BaseTank(1, 1, 4);
        Tank tank2 = new BaseTank(10, 10, 3);
        Map map = new Map();
        int x, y;
        RotateTransform right = new RotateTransform(90);
        RotateTransform left = new RotateTransform(270);
        RotateTransform up = new RotateTransform(0);
        RotateTransform down = new RotateTransform(180);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.M)
            {
                tank1.Shoot(map, tank2);
                if (map.environments[3, 1].EnvHP == 0)
                {
                    beton3_1.Source = wood1_1.Source;
                }
                return;
            }
            if (e.Key == Key.W)
            {
                char w = 'w';
                tank1.Movement(map, tank2, w);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = up;
            }
            if (e.Key == Key.A)
            {
                char a = 'a';
                tank1.Movement(map, tank2, a);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = left;

            }
            if (e.Key == Key.S)
            {
                char s = 's';
                tank1.Movement(map, tank2, s);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = down;
            }
            if (e.Key == Key.D)
            {
                char d = 'd';
                tank1.Movement(map, tank2, d);
                Grid.SetColumn(tankplayer1, tank1.Y);
                Grid.SetRow(tankplayer1, tank1.X);
                tankplayer1.RenderTransform = right;
            }
        }
    }
}
