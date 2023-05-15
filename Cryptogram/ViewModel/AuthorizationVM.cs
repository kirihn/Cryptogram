using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Cryptogram.Commands;
using Cryptogram.Model;
using Cryptogram.Model.Menager;
using Cryptogram.View;

namespace Cryptogram.ViewModel
{
    public class AuthorizationVM : WindowVM<Authorization>
    {
        public AuthorizationVM(Authorization owner) : base(owner)
        {
            SetState(2);

            if (MSSQLBDManager.Instance.Connection.Database == "TestCryptogram") // добавить анимацию подключания к бд
            {
                //Thread.Sleep(5000);
                //SetState(2);
            }
        }

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
            return true;
        }
        private void GetAuthorizathionExecuted(object obj)
        {
            if (CheckValid() == false) return;

            // MSSQLBDManager.Instance.Connection
            string queryString = "SELECT UserId FROM LogInUser WHERE Login = '" + Owner.LoginBox.Info + "' and Password = '" + Cryptoger.HashString(Owner.PasswordBox.Info) + "';";

            SqlCommand command = new SqlCommand(queryString, MSSQLBDManager.Instance.Connection);

            int UserId;
            // Получаем данные и выводим их содержимое в консоль
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    UserId = Convert.ToInt32(reader[0]);
                }
                else
                {
                    MessageBox.Show("      ---------------- Данного аккаунта нет -----------------\n" +
                                    "      -- Возможно вы ввели неверный логин или пароль --\n" +
                                    "      - You may have entered an incorrect login or password -");
                    return;
                }
            }

            command.Dispose();
            //
            Profil profil = new(UserId);
            profil.Show();
            Owner.Close();
            //MessageBox.Show("Запрос авторизации сработал");
        }

        private bool CheckValid()
        {
            string ErrorMessage = "";
            if (Owner.LoginBox.Info == "")
            {
                Owner.LoginBox.box.Background = Brushes.IndianRed;
                ErrorMessage += "Пустое значение логина(Empty login value)\n";
            }
            else
            {
                Owner.LoginBox.box.Background = Brushes.White;
            }

            if (Owner.PasswordBox.Info == "")
            {
                Owner.PasswordBox.pbox.Background = Brushes.IndianRed;
                ErrorMessage += "Пустое значение пароля(Empty password value)\n";
            }
            else
            {
                Owner.PasswordBox.pbox.Background = Brushes.White;
            }

            if (ErrorMessage != "")
            {
                MessageBox.Show(ErrorMessage);
                return false;
            }
            return true;
        }
        // Registrathion

        private BaseCommand GetRegistrathionCommand;
        public ICommand GetRegistrathion
        {
            get
            {
                if (GetRegistrathionCommand == null)
                    GetRegistrathionCommand = new BaseCommand(GetRegistrathionExecuted, GetRegistrathionCanExecuted);

                return GetRegistrathionCommand;
            }
        }
        private bool GetRegistrathionCanExecuted(object obj)
        {
            return true;
        }
        private void GetRegistrathionExecuted(object obj)
        {
            Registration RegWindow = new();
            Owner.Close();
            RegWindow.Show();
        }
    }
}
