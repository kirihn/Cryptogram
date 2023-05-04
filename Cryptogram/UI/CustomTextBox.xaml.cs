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
using static System.Net.Mime.MediaTypeNames;

namespace Cryptogram.UI
{
    /// <summary>
    /// Логика взаимодействия для CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        public static readonly DependencyProperty BaseTextProperty;
        public static readonly DependencyProperty IsPasswordProperty;
        public static readonly RoutedEvent ChangedEvent;

        public string Info { get; private set; }

        static CustomTextBox()
        {
            BaseTextProperty = DependencyProperty.Register("BaseText", typeof(string),
                typeof(CustomTextBox), new PropertyMetadata(string.Empty, OnPlaceholderChanged));

            IsPasswordProperty = DependencyProperty.Register("IsPassword", typeof(bool),
                typeof(CustomTextBox), new PropertyMetadata(false));

            ChangedEvent = EventManager.RegisterRoutedEvent(
                "Changed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomTextBox)
                );
        }


        public string BaseText
        {
            get { return (string)GetValue(BaseTextProperty); }
            set { SetValue(BaseTextProperty, value); }
        }

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public event RoutedEventHandler Changed
        {
            add => AddHandler(ChangedEvent, value);
            remove => RemoveHandler(ChangedEvent, value);
        }

        public CustomTextBox()
        {
            InitializeComponent();

            Info = "";
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (IsPassword)
            {
                box.Visibility = Visibility.Collapsed;
                pbox.Visibility = Visibility.Visible;
            }
            else
            {
                box.Visibility = Visibility.Visible;
                pbox.Visibility = Visibility.Collapsed;
            }
        }


        private void Box_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isEmpty = box.Text == string.Empty;

            if (!isEmpty)
                baseText.Visibility = Visibility.Hidden;
            else
                baseText.Visibility = Visibility.Visible;

            Info = box.Text;

            RaiseEvent(new RoutedEventArgs(ChangedEvent, this));
        }
        private void Box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            bool isEmpty = pbox.Password == string.Empty;

            if (!isEmpty)
                baseText.Visibility = Visibility.Hidden;
            else
                baseText.Visibility = Visibility.Visible;

            Info = pbox.Password;

            RaiseEvent(new RoutedEventArgs(ChangedEvent, this));
        }

        private static void OnPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            CustomTextBox instance = (CustomTextBox)sender;
            
            if (instance != null)
            {
                instance.baseText.Text = e.NewValue as string;
            }
        }
    }
}
