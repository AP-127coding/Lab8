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
    /// Логика взаимодействия для Keyboarding.xaml
    /// </summary>
    public partial class Keyboarding : Window // окно для управления танками
    {
        Menu menu = new Menu();
        public Keyboarding(Menu menu) // конструктор
        {
            this.menu = menu;
            InitializeComponent();
        }
        private void BackToMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            BackToMenu.Background = Brushes.Black;
        }
        private void BackToMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            BackToMenu.Background = Brushes.Red;
        }
        private void BackToMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            menu.Show();
            Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                menu.Show();
                Close();
            }
        }
    }
}
