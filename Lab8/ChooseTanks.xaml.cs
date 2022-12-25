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
    /// Логика взаимодействия для ChooseTanks.xaml
    /// </summary>
    public partial class ChooseTanks : Window
    {
        Tank tank1,tank2;
        bool player1 = true; // флаг для определения номера игрока
        public ChooseTanks()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ChooseBase_MouseEnter(object sender, MouseEventArgs e)
        {
            ChooseBase.Background = Brushes.Black;
        }

        private void ChooseBase_MouseLeave(object sender, MouseEventArgs e)
        {
            ChooseBase.Background = Brushes.Red;
        }

        private void ChooseBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (player1 == true)
            {
                tank1 = new BaseTank(1, 1, 4);
                NumberofPlayer.Content = "Игрок 2";
                player1 = false;
                return;
            }
                if (player1 == false)
                tank2 = new BaseTank(10, 10, 3);
            MainWindow main = new MainWindow(tank1,tank2);
            main.Show();
            Close();
        }

        private void ChooseSpeedy_MouseEnter(object sender, MouseEventArgs e)
        {
            ChooseSpeedy.Background = Brushes.Black;
        }

        private void ChooseSpeedy_MouseLeave(object sender, MouseEventArgs e)
        {
            ChooseSpeedy.Background = Brushes.Red;
        }

        private void ChooseSpeedy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (player1 == true)
            {
                tank1 = new SpeedTank(1, 1, 4);
                NumberofPlayer.Content = "Игрок 2";
                player1 = false;
                return;
            }
                if (player1 == false)
                tank2 = new SpeedTank(10, 10, 3);
            MainWindow main = new MainWindow(tank1, tank2);
            main.Show();
            Close();
        }

        private void ChoosePowerful_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (player1 == true)
            {
                tank1 = new PowerfulTank(1, 1, 4);
                NumberofPlayer.Content = "Игрок 2";
                player1 = false;
                return;
            }
                if (player1 == false)
                tank2 = new PowerfulTank(10, 10, 3);
                
            MainWindow main = new MainWindow(tank1, tank2);
            main.Show();
            Close();
        }

        private void ChoosePowerful_MouseEnter(object sender, MouseEventArgs e)
        {
            ChoosePowerful.Background = Brushes.Black;
        }

        private void ChoosePowerful_MouseLeave(object sender, MouseEventArgs e)
        {
            ChoosePowerful.Background = Brushes.Red;
        }
    }
}
