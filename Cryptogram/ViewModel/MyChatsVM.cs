using Cryptogram.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.ViewModel
{
    internal class MyChatsVM
    {
        public string MyId { get; set; }
        public MyChats Owner;
        public MyChatsVM(MyChats owner, int myId)
        {
            MyId = myId.ToString();
            Owner = owner;

        }
    }
}
