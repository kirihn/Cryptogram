﻿using Cryptogram.ViewModel;
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

namespace Cryptogram.Pages
{
    /// <summary>
    /// Логика взаимодействия для MyFriends.xaml
    /// </summary>
    public partial class MyFriends : Page
    {
        int MyId { get; set; }
        public MyFriends(int myId)
        {
            InitializeComponent();
            MyId = myId;
            DataContext = new MyFriendsVM(this, MyId);
        }
    }
}
