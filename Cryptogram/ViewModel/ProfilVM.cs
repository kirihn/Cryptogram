using Cryptogram.Commands;
using Cryptogram.Model;
using Cryptogram.Model.Menager;
using Cryptogram.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using Cryptogram.Pages;

namespace Cryptogram.ViewModel
{
    public class ProfilVM : WindowVM<Profil>
    {
        public ProfilVM(Profil owner) : base(owner)
        {
            if (MSSQLBDManager.Instance.Connection.Database == "TestCryptogram") // добавить анимацию подключания к бд
            {
                //Thread.Sleep(5000);
                //SetState(2);
            }
        }

        private BaseCommand GoToMyProfileCommand;
        private BaseCommand GoToMyChatsCommand;
        private BaseCommand GoToMyFriendsCommand;
        private BaseCommand GoToSettingsCommand;
        private BaseCommand GoToMyHelpCommand;

        public ICommand GoToMyProfile
        {
            get
            {
                if (GoToMyProfileCommand == null)
                    GoToMyProfileCommand = new BaseCommand(GoToMyProfileExecuted);

                return GoToMyProfileCommand;
            }
        }
        public ICommand GoToMyChats
        {
            get
            {
                if (GoToMyChatsCommand == null)
                    GoToMyChatsCommand = new BaseCommand(GoToMyChatsExecuted);

                return GoToMyChatsCommand;
            }
        }
        public ICommand GoToMyFriends
        {
            get
            {
                if (GoToMyFriendsCommand == null)
                    GoToMyFriendsCommand = new BaseCommand(GoToMyFriendsExecuted);

                return GoToMyFriendsCommand;
            }
        }
        public ICommand GoToSettings
        {
            get
            {
                if (GoToSettingsCommand == null)
                    GoToSettingsCommand = new BaseCommand(GoToMySettingsExecuted);

                return GoToSettingsCommand;
            }
        }
        public ICommand GoToMyHelp
        {
            get
            {
                if (GoToMyHelpCommand == null)
                    GoToMyHelpCommand = new BaseCommand(GoToMyHelpExecuted);

                return GoToMyHelpCommand;
            }
        }


        private void GoToMyProfileExecuted(object obj)
        {
            MyProfile myPage = new MyProfile(Owner.MyId);
            // Переход на страницу
            Owner.mainFrame.NavigationService.Navigate(myPage);
        }
        private void GoToMyChatsExecuted(object obj)
        {
            MyChats myPage = new MyChats(Owner.MyId);
            // Переход на страницу
            Owner.mainFrame.NavigationService.Navigate(myPage);
        }
        private void GoToMyFriendsExecuted(object obj)
        {
            MyFriends myPage = new MyFriends(Owner.MyId);
            // Переход на страницу
            Owner.mainFrame.NavigationService.Navigate(myPage);
        }
        private void GoToMySettingsExecuted(object obj)
        {
            MySettings myPage = new MySettings(Owner.MyId);
            // Переход на страницу
            Owner.mainFrame.NavigationService.Navigate(myPage);
        }
        private void GoToMyHelpExecuted(object obj)
        {
            MyHelp myPage = new MyHelp(Owner.MyId);
            // Переход на страницу
            Owner.mainFrame.NavigationService.Navigate(myPage);

        }
    }
}
