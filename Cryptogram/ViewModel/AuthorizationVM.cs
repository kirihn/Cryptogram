using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Cryptogram.Commands;

namespace Cryptogram.ViewModel
{
    public class AuthorizationVM : WindowVM<Authorization>
    {
        public AuthorizationVM(Authorization owner) : base(owner)
        {
            SetState(2);

            //owner.errorMessage.Visibility = Visibility.Hidden;

            //DataBaseManager.CreateInstance();

            //DataBaseManager.Instance.OnConnected += DataBase_OnConnected;
            //DataBaseManager.Instance.OnError += DataBase_OnError;

            //if (!DataBaseManager.Instance.IsConnected)
            //    DataBaseManager.Instance.ConnectAsync();
            //else
            //    SetState(2);
        }

        //private void DataBase_OnError(string message, DataBaseErrorType errorType)
        //{
        //    if (errorType == DataBaseErrorType.ConnectionFailed)
        //        SetState(1);
        //}
        //private void DataBase_OnConnected()
        //{
        //    ThicknessAnimation maranim = new ThicknessAnimation();

        //    maranim.Duration = TimeSpan.FromSeconds(0.5d);
        //    maranim.From = new Thickness(30, 85, 40, 0);
        //    maranim.To = Owner.Main.Margin;
        //    maranim.EasingFunction = new PowerEase() { EasingMode = EasingMode.EaseOut, Power = 2 };

        //    SetState(2);
        //    Owner.Main.BeginAnimation(FrameworkElement.MarginProperty, maranim);
        //}

        public void SetState(int state = 0)
        {
            switch (state)
            {
                case 1:
                    //Owner.Failed.Visibility = Visibility.Visible;
                    Owner.Loading.Visibility = Visibility.Collapsed;
                    Owner.Main.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    //Owner.Failed.Visibility = Visibility.Collapsed;
                    Owner.Loading.Visibility = Visibility.Collapsed;
                    Owner.Main.Visibility = Visibility.Visible;
                    break;
                default:
                    //Owner.Failed.Visibility = Visibility.Collapsed;
                    Owner.Loading.Visibility = Visibility.Visible;
                    Owner.Main.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private string login;
        private string password;

        public string Login
        {
            get { return login; }
            set { login = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private BaseCommand GetAuthorizathionCommand;
        public ICommand GetAuthorizathion
        {
            get
            {
                if (GetAuthorizathionCommand == null)
                    GetAuthorizathionCommand = new BaseCommand(GetAuthorizathionExecuted, GetAuthorizathionCanExecuted);

                return GetAuthorizathionCommand;
            }
        }
        private bool GetAuthorizathionCanExecuted(object obj)
        {
            //if (!DataBaseManager.Instance.IsConnected)
            //    return false;

            //User user = DataBaseManager.Instance.Users.GetByEmail(Owner.emailBox.Text);

            //if (user == null)
            //{
            //    Owner.errorMessage.Visibility = Visibility.Visible;
            //    return false;
            //}

            //if (!PasswordHasher.Compare(user.Password, Owner.passwordBox.Password))
            //{
            //    Owner.errorMessage.Visibility = Visibility.Visible;
            //    return false;
            //}

            //User.Current = user;

            return true;
        }
        private void GetAuthorizathionExecuted(object obj)
        {
            //User.Current.LastAuthDate = DateTime.Now;

            //DataBaseManager.Instance.Users.Update(User.Current);

            //MainWindow mainWindow = new MainWindow();

            Login = Owner.LoginBox.Info;

            MessageBox.Show(Login);
        }
    }
}
