using Cryptogram.Commands;
using Cryptogram.Model.Menager;
using Cryptogram.Model;
using Cryptogram.Pages;
using Cryptogram.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using Microsoft.VisualBasic;
using System.Windows.Media;
using System.Collections;

namespace Cryptogram.ViewModel
{
    internal class MyProfileVM 
    {
        BitmapImage bitmapImage = new BitmapImage();
        public string MyId { get; set; }
        public AnyUser Iam = new();
        public MyProfile Owner;
        public MyProfileVM(MyProfile owner ,int myId)
        {
            MyId = myId.ToString();
            Owner = owner;
            GetInfoFromBD();

            //Uri uri = new Uri(@"..\IMG\UserPhoto\UserId" + MyId + ".jpg", UriKind.Relative);
            //StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);
            //if (resourceInfo != null)
            //{
            //    BitmapImage bitmapImage = new BitmapImage();
            //    bitmapImage.BeginInit();
            //    bitmapImage.StreamSource = resourceInfo.Stream;
            //    bitmapImage.EndInit();

            //    // Используйте bitmapImage в вашем приложении.
            //}

            //Owner.MyAvatar.Source = bitmapImage;
            Owner.NameP.box.MaxLength = 30;
            Owner.LastnameP.box.MaxLength = 30;
            Owner.UserP.box.MaxLength = 30;
            Owner.StatuseP.box.MaxLength = 100;
        }

        private BaseCommand ApplyChangesCommand;
        public ICommand ApplyChanges
        {
            get
            {
                if (ApplyChangesCommand == null)
                    ApplyChangesCommand = new BaseCommand(ApplyChangesExecuted);

                return ApplyChangesCommand;
            }
        }

        private void ApplyChangesExecuted(object obj)
        {
            string queryString = 
            "UPDATE AnyUser SET Name = '" + Owner.NameP.Info
            + "', LastName = '" + Owner.LastnameP.Info
            + "', Username = '" + Owner.UserP.Info
            + "', Status = '" + Owner.StatuseP.Info
            + "' WHERE UserID = " + MyId + ";";


            using (SqlCommand command = new SqlCommand(queryString, MSSQLBDManager.Instance.Connection))
            {

                // выполнение команды
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Изменения успешно применены!");
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }

            MessageBox.Show("Применены изменения");
        }

        private void GetInfoFromBD()
        {
            // MSSQLBDManager.Instance.Connection
            string queryString = "\tSELECT Name, LastName, UserName, Status, AvatarIMG from AnyUser where UserId = " + MyId + " ;";

            SqlCommand command = new SqlCommand(queryString, MSSQLBDManager.Instance.Connection);

            // Получаем данные и выводим их содержимое в консоль
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Iam.Name = reader[0].ToString().TrimEnd();
                    Owner.NameP.box.Text = Iam.Name;
                    Iam.Lastname = reader[1].ToString().TrimEnd();
                    Owner.LastnameP.box.Text = Iam.Lastname;
                    Iam.UserName = reader[2].ToString().TrimEnd();
                    Owner.UserP.box.Text = Iam.UserName;
                    Iam.Status = reader[3].ToString().TrimEnd();
                    Owner.StatuseP.box.Text = Iam.Status;
                    Iam.AvatarImage = (byte[])reader[4];
                    SaveImg();
                }
                else
                {
                    MessageBox.Show("      ---------------- Данного аккаунта нет -----------------\n" +
                                    "      -- ошибка --\n");
                    return;
                }
            }
            command.Dispose();
        }

        private void SaveImg()
        {
            // Создание нового BitmapImage
            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(Iam.AvatarImage))
            {
                stream.Seek(0, SeekOrigin.Begin);
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            Owner.MyAvatar.Source = image;

            // Создание нового файла изображения и сохранение изображения в него
            string imagePath = @"..\..\..\IMG\UserPhoto\UserId" + MyId + ".jpg";
            if (!File.Exists(imagePath))
            {
                // Файл существует, выполняем нужные действия
                using (FileStream fs = new FileStream(imagePath, FileMode.CreateNew))
                {
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image));
                    encoder.Save(fs);
                }
            }
            
        }
    }
}
