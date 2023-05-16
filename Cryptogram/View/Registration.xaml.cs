using Cryptogram.ViewModel;
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
            DataContext = new RegistrationVM(this);

        }
    #region }
        public void GetMoreInformation(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Шифр-ключ применяется для кодировки отправляемых сообщений. " +
                "Вам следует запомнить его и делиться им только с теми людьми, которые " +
                "должны получать сообщения от вас для их расшифровки. Ключ должен содержать " +
                "только цифры и не может начинаться с нуля. Максимальное число, которое ключ " +
                "может содержать, составляет 999 999 999 999 999 999. Чем больше разрядов " +
                "используется, тем надежнее ваш шифр-ключ.\n(The cipher key is used to encode" +
                " the messages being sent. You should remember it and share it only with those" +
                " people who need to receive messages from you to decrypt them. The key must" +
                " contain only digits and cannot start from zero. The maximum number that a" +
                " key can contain is 999,999,999,999,999. The more bits are used, the " +
                "more reliable your cipher key is.)");
        }

        public void GetRandomKey(object sender, MouseButtonEventArgs e)
        {
            KeyHolderR.box.Text = (new Random()).NextInt64(100000000000000000,999_999_999_999_999_999).ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization NewAccount = new Authorization();
            this.Close();
            NewAccount.Show();
        }

    }
    #endregion
}
