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

namespace Cryptogram.View
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        public void GetMoreInformation(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("You luzzer");
        }

        public void GetRandomKey(object sender, MouseButtonEventArgs e)
        {
            KeyHolder.box.Text = (new Random()).NextInt64(100000000000000000,999_999_999_999_999_999).ToString();
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Authorization NewAccount = new Authorization();
            this.Close();
            NewAccount.Show();

        }
    }
}
