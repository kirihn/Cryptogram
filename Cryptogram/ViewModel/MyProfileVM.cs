using Cryptogram.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cryptogram.ViewModel
{
    internal class MyProfileVM 
    {
        public string MyId { get; set; }
        public MyProfile Owner;
        public MyProfileVM(MyProfile owner ,int myId)
        {
            MyId = myId.ToString();
            Owner = owner;

        }

    }
}
