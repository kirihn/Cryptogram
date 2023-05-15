using Cryptogram.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.ViewModel
{
    internal class MyFriendsVM
    {
        public string MyId { get; set; }
        public MyFriends Owner;
        public MyFriendsVM(MyFriends owner, int myId)
        {
            MyId = myId.ToString();
            Owner = owner;

        }
    }
}
