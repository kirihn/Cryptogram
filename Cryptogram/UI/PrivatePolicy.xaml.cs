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

namespace Cryptogram.UI
{
    /// <summary>
    /// Логика взаимодействия для PrivatePolicy.xaml
    /// </summary>
    public partial class PrivatePolicy : UserControl
    {
        public static readonly DependencyProperty ReadPoliceProperty;
        public static readonly DependencyProperty CheckValidateProperty;
        public static readonly RoutedEvent OnPressedEvent;

        static PrivatePolicy()
        {
            ReadPoliceProperty = DependencyProperty.Register("ReadPolice", typeof(bool),
                typeof(PrivatePolicy), new PropertyMetadata(false, OnPressed));

            //
            CheckValidateProperty = DependencyProperty.Register("CheckValidate", typeof(bool),
                typeof(PrivatePolicy), new PropertyMetadata(false, OnPressed), ValidateMyPropertyValue);
            //

            OnPressedEvent = EventManager.RegisterRoutedEvent(
                "OnPressed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PrivatePolicy)
                );
        }

        public bool ReadPolice
        {
            get { return (bool)GetValue(ReadPoliceProperty); }
            set { SetValue(ReadPoliceProperty, value); }
        }

        //
        public bool CheckValidate
        {
            get { return (bool)GetValue(ReadPoliceProperty); }
            set { SetValue(ReadPoliceProperty, value); }
        }
        //

        public event RoutedEventHandler Pressed
        {
            add => AddHandler(OnPressedEvent, value);
            remove => RemoveHandler(OnPressedEvent, value);
        }


        public PrivatePolicy()
        {
            InitializeComponent();
        }

        //
        private static bool ValidateMyPropertyValue(object value)
        {
            bool propertyValue = (bool)value;

            //MessageBox.Show("Сработала валидация свойства, его значение = " + propertyValue);

            return true;
        }
        //
        private void GetAgree(object sender, RoutedEventArgs e)
        {
            if (ReadPolice == true)
            {
                CheckBoxForPolicy.IsChecked = true;
            }
            else
            {
                CheckBoxForPolicy.IsChecked = false;
                MessageBox.Show("Вы не прочитали политику конфиденциальности!");
            }
        }
        private void GetRead(object sender, RoutedEventArgs e)
        {
            ReadPolice = true;

            MessageBox.Show("Вы прочитали политику конфиденциальности");

            RaiseEvent(new RoutedEventArgs(OnPressedEvent, this));
        }
        private static void OnPressed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PrivatePolicy instance = (PrivatePolicy)sender;
            if (instance != null)
            {
                instance.ReadPolice = true;
            }
        }
    }
}
