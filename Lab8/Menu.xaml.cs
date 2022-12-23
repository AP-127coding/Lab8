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
    public partial class Menu : Window
    {
        MainWindow main = new MainWindow();
        public Menu ()
        {
            InitializeComponent();
        }
        public Menu(MainWindow window)
        {
            main = window;
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
            main.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Continue_MouseDown(object sender, MouseButtonEventArgs e)
        {
            main.Show();
            Close();
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
