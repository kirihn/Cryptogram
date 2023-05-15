using Cryptogram.Commands;
using Cryptogram.Model;
using Cryptogram.Model.Menager;
using Cryptogram.View;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
using System;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Data;

namespace Cryptogram.ViewModel
{
    public class RegistrationVM : WindowVM<Registration>
    {
        //public string username;
        //public string Name { get; set; }
        //public string Lastname { get; set; }
        //public string Status { get; set; }
        //public string Email { get; set; }
        //public byte[] AvatarImage { get; set; }
        //public string Login { get; set; }
        //public string Password { get; set; }
        //public string UserName
        //{
        //    get { return username; }
        //    set { username = "@" + value; }
        //}

        public AnyUser U;

        public RegistrationVM(Registration owner) : base(owner)
        {
            if (MSSQLBDManager.Instance.Connection.Database == "TestCryptogram") // добавить анимацию подключания к бд
            {
                //Thread.Sleep(5000);
                //SetState(2);
            }

            U = new();
            Owner.NameR.box.MaxLength = 20;
            Owner.LastnameR.box.MaxLength = 20;
            Owner.UsernameR.box.MaxLength = 20;
            Owner.EmailR.box.MaxLength = 70;
            Owner.KeyHolderR.box.MaxLength = 18;


        }

        #region Commands

        #region Continue
        private BaseCommand ContinueCommand;
        public ICommand Continue
        {
            get
            {
                if (ContinueCommand == null)
                    ContinueCommand = new BaseCommand(ContinueExecuted);

                return ContinueCommand;
            }
        }

        private void ContinueExecuted(object obj)
        {
            if (!CheckValidate()) return;

            GetDataFromForm();

            AddNewUserToBD();
        }


        #endregion

        #region getImg
        private BaseCommand getImgCommand;
        public ICommand getImg
        {
            get
            {
                if (getImgCommand == null)
                    getImgCommand = new BaseCommand(getImgExecuted);

                return getImgCommand;
            }
        }

        private void getImgExecuted(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage(new System.Uri(openFileDialog.FileName));
                    MemoryStream memoryStream = new MemoryStream();
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(memoryStream);
                    U.AvatarImage = memoryStream.ToArray();
                }
                catch (IOException ex)
                {
                    MessageBox.Show("An error occurred while selecting the image: " + ex.Message);
                }
            }
        }
        #endregion

        #endregion

        public void GetDataFromForm()
        {
            U.Name = Owner.NameR.Info;
            U.Lastname = Owner.LastnameR.Info;
            U.Email = Owner.EmailR.Info;
            U.Login = Owner.EmailR.Info;
            U.UserName = Owner.UsernameR.Info;
            U.Password = Cryptoger.HashString(Owner.FPassR.Info);
            U.EncryptKey = Convert.ToInt64(Owner.KeyHolderR.Info);
        }

        public bool CheckValidate()
        {
            // Проверка на 0 значения, +
            // Проверка на корректную почту +
            // Проверка на уникальную почту +
            // Проверка на уникальный юзернейм +
            // Проверка на ввод изображения +
            // Проверка на совпадение паролей +
            // Проверка на ключ +
            // Политика конфиденциальности +

            string ErrorMessage = "";

            #region CheckEmptyValue 

            if (Owner.NameR.Info == "")
            {
                ErrorMessage += "Пустое значение имени(Empty name value)\n";
                Owner.NameR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.NameR.box.Background = Brushes.White;
            }
            if (Owner.LastnameR.Info == "")
            {
                ErrorMessage += "Пустое значение Фамилии(Empty lastname Value)\n";
                Owner.LastnameR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.LastnameR.box.Background = Brushes.White;

            }
            if (Owner.EmailR.Info == "")
            {
                ErrorMessage += "Пустое значение почты(Empty mail value)\n";
                Owner.EmailR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.EmailR.box.Background = Brushes.White;

            }
            if (Owner.UsernameR.Info == "")
            {
                ErrorMessage += "Пустое значение username(Empty username value)\n";
                Owner.UsernameR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.UsernameR.box.Background = Brushes.White;

            }
            if (Owner.FPassR.Info == "")
            {
                ErrorMessage += "Пустое значение пароля(Empty password value)\n";
                Owner.FPassR.pbox.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.FPassR.pbox.Background = Brushes.White;

            }
            if (Owner.SPassR.Info == "")
            {
                ErrorMessage += "Пустое значение повторного пароля(Empty value repeated password)\n";
                Owner.SPassR.pbox.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.SPassR.box.Background = Brushes.White;
            }
            if (Owner.KeyHolderR.Info == "")
            {
                ErrorMessage += "Пустое значение шифр-ключа(Empty encryptkey value)\n";
                Owner.KeyHolderR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.KeyHolderR.box.Background = Brushes.White;

            }
            if (U.AvatarImage.Length < 1)
            {
                ErrorMessage += "Вы не выбрали изображение(You have not selected an image)\n";
            }

            #endregion

            #region CheckEncryptKey

            string content = "0123456789";
            foreach (var item in Owner.KeyHolderR.Info)
            {
                if (!content.Contains(item))
                {
                    ErrorMessage += "Ключ содержит некорректные символы" +
                        "(The key contains invalid characters)\n";
                    Owner.KeyHolderR.box.Background = Brushes.IndianRed;
                }
                else
                {
                    Owner.KeyHolderR.box.Background = Brushes.White;
                }
                break;
            }

            if (Owner.KeyHolderR.Info.Length > 0)
            {
                if (Owner.KeyHolderR.Info[0] == '0')
                {
                    ErrorMessage += "Ключ не может начинаться с 0" +
                                    "(The key cannot start with 0)\n";
                    Owner.KeyHolderR.box.Background = Brushes.IndianRed;
                }
            }

            #endregion

            #region CheckValidEmailAndUsername

            // Используем регулярное выражение для проверки формата email
            var pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            var regex = new Regex(pattern);
            if (!regex.IsMatch(Owner.EmailR.Info))
            {
                ErrorMessage += "Неккоретное значение почты(Invalid email value)\n";
                Owner.EmailR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.EmailR.box.Background = Brushes.White;
            }

            // Используем регулярное выражение для проверки формата UserName

            pattern = @"^@[A-Za-z0-9]+$";
            regex = new Regex(pattern);
            if (!regex.IsMatch(Owner.UsernameR.Info))
            {
                ErrorMessage += "Неккоретное значение UserName(Invalid UserName value)\n";
                Owner.UsernameR.box.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.UsernameR.box.Background = Brushes.White;
            }

            #endregion

            #region ComparerPasswords
            if (Owner.FPassR.Info != Owner.SPassR.Info || Owner.FPassR.Info == "")
            {
                ErrorMessage += "Ваши пароли не совпадают(Your passwords don't match)\n";
                Owner.FPassR.pbox.Background = Brushes.IndianRed;
                Owner.SPassR.pbox.Background = Brushes.IndianRed;
            }
            else
            {
                Owner.FPassR.pbox.Background = Brushes.White;
                Owner.SPassR.pbox.Background = Brushes.White;
            }
            #endregion

            #region PrivPolicy

            if (!Owner.PrivPolicy.CheckBoxForPolicy.IsChecked.Value.Equals(true))
            {
                ErrorMessage += "Вы не согласились с политикой приложения(You did not agree with the application policy)\n";
            }

            #endregion

            #region CheckHave Such user
            string queryString = "SELECT UserId FROM AnyUser WHERE Email = '" + Owner.EmailR.Info + "';";

            SqlCommand command = new SqlCommand(queryString, MSSQLBDManager.Instance.Connection);

            // Получаем данные и выводим их содержимое в консоль
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ErrorMessage += "Пользователь уже зарегистрирован(The user is already registered)\n";
                    Owner.EmailR.box.Background = Brushes.IndianRed;
                }
            }

            command.Dispose();

            // Username

            queryString = "SELECT UserId FROM AnyUser WHERE Username = '" + Owner.UsernameR.Info + "';";

            command = new SqlCommand(queryString, MSSQLBDManager.Instance.Connection);

            // Получаем данные и выводим их содержимое в консоль
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ErrorMessage += "Данный username уже занят(This username is already occupied)\n";
                    Owner.UsernameR.box.Background = Brushes.IndianRed;
                }
            }

            command.Dispose();
            #endregion

            if (ErrorMessage != "")
            {
                MessageBox.Show(ErrorMessage);
                return false;
            }
            return true;
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                // Используем регулярное выражение для проверки формата email
                var pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                var regex = new Regex(pattern);
                return regex.IsMatch(email);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public void AddNewUserToBD()
        {
            SqlTransaction transaction = MSSQLBDManager.Instance.CreateTransaction();

            try
            {
                SqlCommand command = MSSQLBDManager.Instance.CreateCommand();

                command.Transaction = transaction;

                command.CommandType = CommandType.StoredProcedure;

                SqlParameter NParam = new SqlParameter("@Name", U.Name);
                SqlParameter LNParam = new SqlParameter("@LastName", U.Lastname);
                SqlParameter EParam = new SqlParameter("@Email", U.Email);
                SqlParameter UParam = new SqlParameter("@Username", U.UserName);
                SqlParameter PParam = new SqlParameter("@Password", U.Password);
                SqlParameter LParam = new SqlParameter("@Login", U.Login);
                SqlParameter SParam = new SqlParameter("@Status", " ");
                SqlParameter Iaram = new SqlParameter("@AvatarIMG", U.AvatarImage);

                command.Parameters.Add(NParam);
                command.Parameters.Add(LNParam);
                command.Parameters.Add(EParam);
                command.Parameters.Add(UParam);
                command.Parameters.Add(PParam);
                command.Parameters.Add(LParam);
                command.Parameters.Add(SParam);
                command.Parameters.Add(Iaram);


                command.CommandText = "[dbo].[AddAnyUser]";

                int rowsAffected = command.ExecuteNonQuery();
                transaction.Commit();

                if (rowsAffected != 2)
                {
                    MessageBox.Show("Неизвестная ошибка регистрации(Registrathion Error)");
                }
                else
                {
                    MessageBox.Show("Вы успешно зарегистрированны(You have successfully registered)");
                    Authorization authorization = new();
                    authorization.Show();
                    Owner.Close();
                }
            }
            catch (SqlException ex)
            {
                transaction.Rollback();

                MessageBox.Show(ex.Message, "SQL Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            transaction.Dispose();
        }
    }
}
