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

namespace PL
{
    /// <summary>
    /// Interaction logic for CodeWindow.xaml
    /// </summary>
    public partial class CodeWindow : Window
    {
        public CodeWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            CheckPassword();
        }

        private void CodeTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                CheckPassword();
            }
        }

        private void CheckPassword()
        {
            if (CodeTextBox.Password != "1234")
            {
                MessageBox.Show("Wrong Password, please try again", "Oops...", MessageBoxButton.OK, MessageBoxImage.Error);
                CodeTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                new ManagementWindow().Show();
                this.Close();
            }
        }
    }
}
