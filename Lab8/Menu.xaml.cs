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
using System.Windows.Shapes;

namespace Lab8
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window // окно меню
    {
        Tank tank1;
        Tank tank2;
        MainWindow main = new MainWindow();
        Map map = new Map();
        public Menu () // конструктор
        {
            InitializeComponent();
            try // попытка десериализации 
            {
                tank1 = Program.TankSerialisation(tank1, true);
                tank2 = Program.Tank2Serialisation(tank2, true);
                map = Program.MapSerialisation(map, tank1.X, tank1.Y, tank2.X, tank2.Y, true);
            }
            catch (Exception) // иначе кнопку "Продолжить" невозможно нажать
            {
                Continue.IsEnabled = false;
                Continue.Foreground = Brushes.Gray;
            }       
        }
        public Menu(MainWindow window, Tank t1, Tank t2,Map map) // конструктор
        {
            this.map = map;
            main = window;
            tank1 = t1;
            tank2 = t2;
            InitializeComponent();
        }
        private void Continue_MouseEnter(object sender, MouseEventArgs e)
        {
            Continue.Foreground = Brushes.Red;
        }

        private void Continue_MouseLeave(object sender, MouseEventArgs e)
        {
            Continue.Foreground = Brushes.White;
        }

        private void Start_MouseEnter(object sender, MouseEventArgs e)
        {
            Start.Foreground = Brushes.Red;
        }

        private void Start_MouseLeave(object sender, MouseEventArgs e)
        {
            Start.Foreground = Brushes.White;
        }

        private void Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            Exit.Foreground = Brushes.Red;
        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            Exit.Foreground = Brushes.White;
        }

        private void Start_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            ChooseTanks ct = new ChooseTanks();
            ct.Show();
            Close();
        }

        private void Continue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainw = new MainWindow(tank1, tank2,map);
            mainw.Show();
            Close();
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e) // сериализация при нажатии "Выход"
        {
            Program.TankSerialisation(tank1, false);
            Program.Tank2Serialisation(tank2, false);
            Program.MapSerialisation(map, tank1.X, tank1.Y, tank2.X, tank2.Y, false);
            Application.Current.Shutdown();
        }

        private void Keyboard_MouseEnter(object sender, MouseEventArgs e)
        {
            Keyboard.Foreground = Brushes.Red;
        }

        private void Keyboard_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.Foreground = Brushes.White;
        }

        private void Keyboard_MouseDown(object sender, MouseButtonEventArgs e) // нажатие клавиши "Управление"
        {
            Keyboarding keyboarding = new Keyboarding(this);
            keyboarding.Show();
            Hide();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
